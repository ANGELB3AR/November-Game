using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch(newState)
        {
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
    {
        Playing,
        Paused
    }
}
