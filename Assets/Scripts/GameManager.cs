using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    [SerializeField] InputReader InputReader;

    void Start()
    {
        InputReader.PauseEvent += OnPause;
    }

    void OnDestroy()
    {
        InputReader.PauseEvent -= OnPause;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch(newState)
        {
            case GameState.Playing:
                HandleGamePlaying();
                break;
            case GameState.Paused:
                HandleGamePaused();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
    {
        Playing,
        Paused
    }

    void HandleGamePlaying()
    {
        Time.timeScale = 1f;
    }

    void HandleGamePaused()
    {
        Time.timeScale = 0f;
    }

    void OnPause()
    {
        if (State == GameState.Playing)
        {
            UpdateGameState(GameState.Paused);
        }
        else
        {
            UpdateGameState(GameState.Playing);
        }
    }
}
