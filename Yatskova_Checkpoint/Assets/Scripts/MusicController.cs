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

    // Start is called before the first frame update
    void Start()
    {
        
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
}
