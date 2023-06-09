using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource[] SourceArray;
    AudioSource Source;
    AudioSource FadeOutSource;

    public AudioClip MenuMusic;
    public AudioClip PlayMusic;
    public AudioClip WinMusic;
    public AudioClip LoseMusic;
    public AudioClip NoMusic;

    public enum Music
    {
        MenuMusic,
        PlayMusic,
        WinMusic,
        LoseMusic,
        NoMusic
    }

    [SerializeField]
    float crossFadeTime;  // How long it takes for a cross fade to complete

    float defaultVolume = 0.5f;
    bool isPaused;

    // Reference to object's low pass filter
    [SerializeField]
    private AudioLowPassFilter lowPassFilter;

    // Start is called before the first frame update
    void Start()
    {
        // Get references of audio sources
        SourceArray = GetComponents<AudioSource>();

        foreach (AudioSource source in SourceArray)
            source.volume = defaultVolume;

        Source = SourceArray[0];
        FadeOutSource = SourceArray[1];

        // Start by playing menu music
        Source.clip = MenuMusic;
        Source.volume = 0.0f;
        Source.Play();

        // Start game not paused
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Crossfade the music
        if (isPaused == false)
        {
            // Fade in new music
            Source.volume = Mathf.Clamp(Source.volume + (defaultVolume / crossFadeTime * Time.unscaledDeltaTime), 0.0f, defaultVolume);
            // Fade out old music
            FadeOutSource.volume = Mathf.Clamp(FadeOutSource.volume - (defaultVolume / crossFadeTime * Time.unscaledDeltaTime), 0.0f, defaultVolume);
        }
    }

    public void SwapMusic(Music newMusic)
    {
        // Load up the old music to be faded out
        FadeOutSource.clip = Source.clip;
        FadeOutSource.time = Source.time;
        FadeOutSource.Play();
        FadeOutSource.volume = defaultVolume;

        // Load up new music to be played
        if (newMusic == Music.MenuMusic)
            Source.clip = MenuMusic;
        if (newMusic == Music.PlayMusic)
            Source.clip = PlayMusic;
        if (newMusic == Music.WinMusic)
            Source.clip = WinMusic;
        if (newMusic == Music.LoseMusic)
            Source.clip = LoseMusic;
        if (newMusic == Music.NoMusic)
            Source.clip = NoMusic;
        // New music volume starts at 0, but will fade in
        Source.volume = 0.0f;
        // Start playing new music
        Source.Play();
    }

    public void DampenMusic()
    {
        Source.volume = 0.2f;
        lowPassFilter.cutoffFrequency = 600;
        isPaused = true;
    }

    public void UnDampenMusic()
    {
        Source.volume = defaultVolume;
        lowPassFilter.cutoffFrequency = 22000;
        isPaused = false;
    }

    // Dampen music for a set time
    public void DuckMusic(float time)
    {
        Source.volume = 0.2f;

        // Undo this in "time" seconds
        Invoke("UnDampenMusic", time);
    }


    public void UnDuckMusic()
    {
        Source.volume = defaultVolume;
    }

    // Fade out inactive music

    // Fade in active music
}
