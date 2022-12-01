using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    GameObject DebugMenuCanvas;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;

        DebugMenuCanvas = FindObjectOfType<DebugMenu>(true).gameObject;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        DebugMenuCanvas.SetActive(state == GameManager.GameState.Paused);
    }
}
