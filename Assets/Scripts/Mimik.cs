using UnityEngine;

public class Mimik : MonoBehaviour
{
    [SerializeField] private int _mimikMaxHP = 20;
    [SerializeField] private int _mimikActualHP;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mimikActualHP = _mimikMaxHP;
    }
    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController _playerDamage = collision.gameObject.GetComponent<PlayerController>();
            _animator.SetTrigger("IsAttacking");
            _playerDamage.TakeDamage(50);
        }       
    }

    public void TakeDamage(int damage)
    {
        _mimikActualHP -= damage;

        if(_mimikActualHP <= 0)
        {
            Die();
        }

    }

    

    void Die()
    {
        Destroy(gameObject);
    }
}
