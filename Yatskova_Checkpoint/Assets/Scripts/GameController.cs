using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // References to specific objects
    public MusicController musicController;   // Music Player
    public PlayerController player;           // Player

    // List of possible Game States
    public enum GameState
    {
        StateMenu,
        StatePlay,
        StateWin,
        StateLose
    }

    // The game state we are currently in
    private GameState CurrentGameState;

    // Start is called before the first frame update
    void Start()
    {
        // Start game in menu state
        CurrentGameState = GameState.StateMenu;

        // Play menu music
        musicController.PlayMenuMusic();
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

        if (CurrentGameState == GameState.StateMenu)
        {

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
        // Set the new game state
        CurrentGameState = newGameState;

        // Do other desired behaviors here...

        // Transition to play game state
        if (newGameState == GameState.StatePlay)
        {
            musicController.PlayPlayMusic();
        }
    }
}
