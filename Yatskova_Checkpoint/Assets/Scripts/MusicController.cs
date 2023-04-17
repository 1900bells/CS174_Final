using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip MenuMusic;
    public AudioClip PlayMusic;
    public AudioClip WinMusic;
    public AudioClip LoseMusic;

    float defaultVolume = 0.5f;

    // Reference to object's low pass filter
    [SerializeField]
    private AudioLowPassFilter lowPassFilter;

    // Start is called before the first frame update
    void Start()
    {
        Source.volume = defaultVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayMenuMusic()
    {
        Source.clip = MenuMusic;
        Source.Play();
    }

    // Play the "Play" state music
    public void PlayPlayMusic()
    {
        Source.clip = PlayMusic;
        Source.Play();
    }

    public void DampenMusic()
    {
        Source.volume = 0.2f;
        lowPassFilter.cutoffFrequency = 600;
    }

    public void UnDampenMusic()
    {
        Source.volume = defaultVolume;
        lowPassFilter.cutoffFrequency = 22000;
    }

    // Fade out inactive music

    // Fade in active music
}
