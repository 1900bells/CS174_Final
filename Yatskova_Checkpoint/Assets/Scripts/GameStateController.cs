using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    // Reference to music controller
    public MusicController musicController;

    public enum GameState
    {
        StateMenu,
        StatePlay,
        StateWin,
        StateLose
    }

    private GameState CurrentGameState;

    // Start is called before the first frame update
    void Start()
    {
        // Start in menu state
        CurrentGameState = GameState.StateMenu;

        // Play menu music
        musicController.PlayMenuMusic();
    }

    // Update is called once per frame
    void Update()
    {
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
        CurrentGameState = newGameState;

        // Transition to play game state
        if (newGameState == GameState.StatePlay)
        {
            musicController.PlayPlayMusic();
        }
    }
}
