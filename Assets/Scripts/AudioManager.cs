using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //variable SingleTone
    public static AudioManager Instance;

    private AudioSource _audioSource;
    
    [SerializeField] private AudioClip _soundtrack;

    void Awake()
    {
        //SingleTone
        if(Instance != null & Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void StartSoundtrack()
    {
        _audioSource.clip = _soundtrack; //determinamos que clip de audio vamos a reproducir
        _audioSource.Play();
    }

    public void PauseSoundtrack()
    {
        _audioSource.Pause();
    }
}
