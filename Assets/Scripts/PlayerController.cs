using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float movementSpeed = 4.5f;    
    [SerializeField] private float _jumpHeight = 10;

//Components
    private Rigidbody2D _rigidBody2D;

//Inputs
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Vector2 _moveInput;

//Ground Sensor
    [SerializeField] private Transform _groundSensor;
    [SerializeField] private float _sensorSize = 1;
    [SerializeField] private LayerMask _groundLayer;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
    }
    // Update is called once per frame
    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        if(_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else if(_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }


        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        } 

        
    }
    void FixedUpdate()
    {
        _rigidBody2D.linearVelocity = new Vector2(_moveInput.x * movementSpeed, _rigidBody2D.linearVelocity.y);    
    } 

    void Jump()
    {
        _rigidBody2D.AddForce(Vector2.up * Mathf.Sqrt(_jumpHeight * -2 * Physics2D.gravity.y),ForceMode2D.Impulse); 
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_groundSensor.position, _sensorSize);
    }

}

