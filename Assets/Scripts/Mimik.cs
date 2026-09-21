using UnityEngine;

public class Mimik : MonoBehaviour
{
    [SerializeField] private int _mimikMaxHP = 20;
    [SerializeField] private int _mimikActualHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mimikActualHP = _mimikMaxHP;
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
