using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // References to specific objects
    public MusicController musicController;   // Music Player
    public PlayerController player;           // Player
    public GameObject PauseMenu;              // The pause menu

    // List of possible Game States
    public enum GameState
    {
        StateMenu,
        StatePlay,
        StateWin,
        StateLose,
        StatePaused
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

        // Play menu music
        musicController.PlayMenuMusic();

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
        // Take input thru the "Game Controller" object

        // Player jumps
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
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
    }

    // Gets the Current Game State
    GameState GetGameState()
    {
        return CurrentGameState;
    }

    // Sets the current game state
    void SetGameState(GameState newGameState)
    {
        // Store previous game state
        PrevGameState = CurrentGameState;
        // Set the new game state
        CurrentGameState = newGameState;

        // Do other desired behaviors here...

        // Transition to play game state
        if (newGameState == GameState.StatePlay)
        {
            musicController.PlayPlayMusic();
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
}
