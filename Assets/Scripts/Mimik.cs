using UnityEngine;

public class Mimik : MonoBehaviour
{
    private AudioSource _mimikAudioSource;
    [SerializeField] private float _mimikMaxHP = 20;
    [SerializeField] private float _mimikActualHP;
    [SerializeField] private AudioClip _attackSound;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _mimikAudioSource = GetComponent<AudioSource>();
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
            PlaySFX(_attackSound, 0.7f);
            _animator.SetTrigger("IsAttacking");
            _playerDamage.TakeDamage(0.3f);
        }     
    }

    void PlaySFX(AudioClip clip, float volume = 1)
    {
        _mimikAudioSource.PlayOneShot(clip, volume);
    }

    public void TakeDamage(float damage)
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
