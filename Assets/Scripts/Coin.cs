using UnityEngine;

public class Coin : MonoBehaviour
{

    private AudioSource _coinAudioSource;

    [SerializeField] private AudioClip _coinAudioClip;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;

    void Awake()
    {
        _coinAudioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
    }
    
    void PlaySFX()
    {
        _coinAudioSource.PlayOneShot(_coinAudioClip);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"));
        {
            GameManager.Instance.AddCoin();
            PlaySFX();
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
