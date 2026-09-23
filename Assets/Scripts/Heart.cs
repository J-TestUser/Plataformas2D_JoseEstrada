using UnityEngine;

public class Heart : MonoBehaviour
{
    private AudioSource _hearthAudioSource;
    private CircleCollider2D _circleCollider2D;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _heal;

    [SerializeField] private AudioClip _healSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hearthAudioSource = GetComponent <AudioSource>();
        _circleCollider2D = GetComponent <CircleCollider2D>();
        _spriteRenderer = GetComponent <SpriteRenderer>();
        
    }
    void PlaySFX()
    {
        _hearthAudioSource.PlayOneShot(_healSound);
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerScript = collision.GetComponent<PlayerController>();
            playerScript.GainHealth(_heal);
            _spriteRenderer.enabled = false;
            _circleCollider2D.enabled = false;
            PlaySFX();
            Destroy(gameObject, 0.5f);
        }
    }

}
