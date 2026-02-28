using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private bool hasPlayedSound;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip slash;
    public AudioClip deflect;
    public AudioClip sewage;
    public AudioClip police;
    public AudioClip rain;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource.volume = 0.5f;
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySlash()
    {
        sfxSource.PlayOneShot(slash);
    }

    public void PlayDeflect()
    {
        sfxSource.PlayOneShot(deflect);
    }

    public void PlaySewage()
    {
        musicSource.Pause();
        sfxSource.PlayOneShot(sewage);
    }

    public void PlayPolice()
    {
        sfxSource.PlayOneShot(police);
    }

    public void PlayRain()
    {
        sfxSource.PlayOneShot(rain);
    }

    public void PlaySFX()
    {
        sfxSource.Play();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }
}
