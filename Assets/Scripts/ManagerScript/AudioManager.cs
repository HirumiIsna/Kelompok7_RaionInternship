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
    [Header("Music")]
    public AudioClip background1;
    public AudioClip background2;
    public AudioClip background3;

    [Header("Sound Effect")]
    [Header("Character")]
    public AudioClip slash;
    public AudioClip deflect;
    public AudioClip pickup;
    public AudioClip bushes;

    [Header("Monster")]
    public AudioClip slimeDeath;

    [Header("Misc")]
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
        
    }

    public void PlayMusic()
    {
        musicSource.volume = 0.5f;
        musicSource.clip = background1;
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

    public void PlayPickup()
    {
        sfxSource.PlayOneShot(pickup);
    }

    public void PlayBush()
    {
        sfxSource.PlayOneShot(bushes);
    }

    public void PlaySlimeDeath()
    {
        sfxSource.PlayOneShot(slimeDeath);
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
