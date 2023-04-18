using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // References to specific objects
    public MusicController musicController;   // Music Player
    public PlayerController player;           // Player
    public GameObject PauseMenu;              // The pause menu
    public GameObject WinText;                // The win text
    public GameObject LoseText;               // The lose text
    public GameObject MenuText;               // The menu text
    public CoverFade coverFade;               // Black cover on screen

    public AudioSource Announcer;             // The voice announcer
    public AudioClip AnnounceStart;           // I say "start"

    // List of possible Game States
    public enum GameState
    {
        StateMenu,
        StatePlay,
        StateWin,
        StateLose,
        StatePaused,
        StateReset
    }

    // The game state we are currently in
    private GameState CurrentGameState;
    // The game state we were previously in
    private GameState PrevGameState;

    // Start is called before the first frame update
    void Start()
    {
        // Start game in menu state
        CurrentGameState = GameState.StateMenu;

        // Randomly set one of the pick ups to be bomb & one to be treasure
        GameObject[] pickUps = GameObject.FindGameObjectsWithTag("Pick Up");
        int numPickUps = pickUps.Length;
        if (pickUps.Length > 0)
        {
            // Generate which pickup is bomb
            int bomb = Random.Range(0, numPickUps - 1);
            // Generate which pickup is treasure
            int treasure = 0;
            do
            {
                treasure = Random.Range(0, numPickUps - 1);
            } while (treasure == bomb);  // Ensure treasure is not bomb

            // Assign treasure and bomb
            pickUps[bomb].gameObject.GetComponent<CubeController>().Type(CubeController.CollectableType.Bomb);
            pickUps[treasure].gameObject.GetComponent<CubeController>().Type(CubeController.CollectableType.Treasure);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Pause game while in menu
        if (CurrentGameState == GameState.StateMenu)
        {
            Time.timeScale = 0.0f;
        }

        // Take input thru the "Game Controller" object

        // Player jumps
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentGameState == GameState.StateMenu)
            {
                SetGameState(GameState.StatePlay);
            }
            else
            {
                player.Jump();
            }
        }

        // Player Rolling
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        player.Move(direction);

        // Player presses pause button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If game is already paused, unpause game
            if (CurrentGameState == GameState.StatePaused)
            {
                ResumeGame();
            }
            // If game is not already paused, pause the game
            else
            {
                PauseGame();
            }
        }

        // Player presses reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetGameState(GameState.StateReset);
        }
    }

    // Gets the Current Game State
    GameState GetGameState()
    {
        return CurrentGameState;
    }

    // Sets the current game state
    public void SetGameState(GameState newGameState)
    {
        // Store previous game state
        PrevGameState = CurrentGameState;
        // Set the new game state
        CurrentGameState = newGameState;

        // Do other desired behaviors here...
        if (newGameState == GameState.StateMenu)
        {
            // Play the play music
            musicController.SwapMusic(MusicController.Music.MenuMusic);
            // Show menu text
            MenuText.SetActive(true);
            // Hide Win and Lose text
            WinText.SetActive(false);
            LoseText.SetActive(false);
        }


        // Transition to play game state
        else if (newGameState == GameState.StatePlay)
        {
            // Play the play music
            musicController.SwapMusic(MusicController.Music.PlayMusic);
            // Hide menu text
            MenuText.SetActive(false);
            // Unfreeze the game
            Time.timeScale = 1.0f;
            // Start player rolling sounds
            player.GetComponent<PlayerSpeedSoundController>().StartSounds();
            // Start all ticking sounds
            CubeController[] cubes = GameObject.FindObjectsOfType<CubeController>();
            foreach (CubeController cube in cubes)
            {
                if (cube.Type() == CubeController.CollectableType.Bomb)
                    cube.StartTickingSound();
            }

            if (PrevGameState == GameState.StateMenu)
            {
                // Do the voice over if going from menu
                Announcer.clip = AnnounceStart;
                Announcer.Play();
                // Duck the music for this voice over
                musicController.DuckMusic(Announcer.clip.length);
            }
        }

        else if (newGameState == GameState.StateWin)
        {
            // Play win music
            musicController.SwapMusic(MusicController.Music.WinMusic);
            // Display win text
            WinText.SetActive(true);
        }

        else if (newGameState == GameState.StateLose)
        {
            // Play lose music
            musicController.SwapMusic(MusicController.Music.LoseMusic);
            // Display lose text
            LoseText.SetActive(true);
        }

        else if (newGameState == GameState.StateReset)
        {
            // Fade out music
            musicController.SwapMusic(MusicController.Music.NoMusic);
            // Reset scene in 1 second
            Invoke("ResetGame", 1.0f);
            // Set timescale to normal
            Time.timeScale = 1.0f;
            // Fade in the black cover
            coverFade.FadeIn();
        }
    }

    // Pause the game
    void PauseGame()
    {
        SetGameState(GameState.StatePaused);
        // Show pause menu
        PauseMenu.SetActive(true);
        // Unfreeze time
        Time.timeScale = 0.0f;
        // Atenuate Music
        musicController.DampenMusic();
    }

    // Resume the game
    void ResumeGame()
    {
        SetGameState(PrevGameState);
        // Hide pause menu
        PauseMenu.SetActive(false);
        // Freeze time
        Time.timeScale = 1.0f;
        // Atenuate Music
        musicController.UnDampenMusic();
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
