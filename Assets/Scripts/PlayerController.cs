using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float movementSpeed = 4.5f;    
    [SerializeField] private float _jumpForce = 7;
    private Rigidbody2D _rigidBody2D;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Vector2 _moveInput;


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


        if (_jumpAction.WasPressedThisFrame())
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
        _rigidBody2D.AddForce(Vector2.up * _jumpForce,ForceMode2D.Impulse);
    }


}

