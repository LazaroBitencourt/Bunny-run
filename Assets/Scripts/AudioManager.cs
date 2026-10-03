using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip click;
    public AudioClip gameOver;
    public AudioClip jump;
    public AudioClip MusicLoop;

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource.clip = MusicLoop;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.loop = false;
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void JumpSom()
    {
        SFXSource.PlayOneShot(jump);
    }

    public void ClickSom()
    {
        SFXSource.PlayOneShot(click);
    }
}
