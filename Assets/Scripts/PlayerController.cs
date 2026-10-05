using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _actualHealth;
    [SerializeField] private float movementSpeed = 4.5f;    
    [SerializeField] private float _jumpHeight = 10;  
    [SerializeField] private int _attackDamage = 10;

//Components
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private AudioSource _playerAudioSource;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;

//Inputs
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private Vector2 _moveInput;
    private InputAction _pauseAction;

//Colliders and Sensors
    [SerializeField] private Transform _groundSensor;
    [SerializeField] private Transform _attackHitBox;
    [SerializeField] private float _hitBoxRadius = 1f;
    [SerializeField] private float _sensorSize = 1;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _attackSound; 
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _deathSound;
    
  

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerAudioSource = GetComponent<AudioSource>();
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();


        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _attackAction = InputSystem.actions["Attack"];
        _pauseAction = InputSystem.actions["Pause"];
    }

    void Start()
    {
        _actualHealth = _maxHealth;
        GameManager.Instance._playerHealth = _actualHealth;
    }
    // Update is called once per frame
    void Update()
    {        
        if (_pauseAction.WasPressedThisFrame())
        {
            GameManager.Instance.Pause();
        }

        //Si se cumple la condición, return corta la función (dentro de update) anulando todos los inputs, a excepción del de pausa
        if(GameManager.Instance.IsPaused())
        {
            return; 
        }

        _moveInput = _moveAction.ReadValue<Vector2>();
        if(_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
            _animator.SetBool("IsRunning", true);
        }
        else if(_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }


        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        } 

        if (_attackAction.WasPressedThisFrame() && IsGrounded())
        {
            Attack();
        }


        _animator.SetBool("IsJumping", !IsGrounded());

        
    }
    void FixedUpdate()
    {
        _rigidBody2D.linearVelocity = new Vector2(_moveInput.x * movementSpeed, _rigidBody2D.linearVelocity.y);
 
    } 

    void Jump()
    {
        _rigidBody2D.AddForce(Vector2.up * Mathf.Sqrt(_jumpHeight * -2 * Physics2D.gravity.y),ForceMode2D.Impulse);         
        PlaySFX(_jumpSound, 0.7f);   
    }

    void Attack()
    {
        _animator.SetTrigger("IsAttacking");

        PlaySFX(_attackSound, 0.7f);

        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(_attackHitBox.position, _hitBoxRadius);

        foreach(Collider2D enemy in colliders2D)
        {
            if(enemy.gameObject.layer == 7)
            {
                Mimik enemyScript = enemy.GetComponent<Mimik>();
                enemyScript.TakeDamage(_attackDamage);
            }
        }
    }

    bool IsGrounded()
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(_groundSensor.position, _sensorSize);

        foreach (Collider2D item in colliders2D)
        {
            if(item.gameObject.layer == 6)
            {
                return true;
            }
        }
        return false; 
    }

    void PlaySFX(AudioClip clip, float volume = 1)
    {
        _playerAudioSource.PlayOneShot(clip, volume);
    }

    public void GainHealth(int heal)
    {
        _actualHealth += heal;

        if(_actualHealth >= _maxHealth)
        {
            _actualHealth = _maxHealth ;  
        }
        
    }

    public void TakeDamage(int damage)
    {
        _actualHealth -= damage;
        PlaySFX(_damageSound);
        GameManager.Instance._playerHealth = _actualHealth;
        GameManager.Instance.HealthBar(-0.1f);

        if(_actualHealth <= 0)
        {
            Death();
        }
    }

    void Death ()
    {
        SceneLoader.Instance.GameOver("GameOver");
        PlaySFX(_deathSound);
        _collider2D.enabled = false;
        _spriteRenderer.enabled = false;
        Destroy(gameObject, 0.5f);
    }

    void OnDrawGizmos()
    {
        //GroundSensor Gizmo
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_groundSensor.position, _sensorSize);

        //AttackHitbox Gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackHitBox.position, _hitBoxRadius);
    }
}

