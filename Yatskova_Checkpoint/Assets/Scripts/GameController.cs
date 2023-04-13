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
