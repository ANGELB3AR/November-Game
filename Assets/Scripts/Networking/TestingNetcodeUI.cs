using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{
    private void Awake()
    {
        Show();
    }

    public void StartHost()
    {
        Debug.Log("STARTING HOST");
        NetworkManager.Singleton.StartHost();
        Hide();
    }

    public void StartClient()
    {
        Debug.Log("STARTING CLIENT");
        NetworkManager.Singleton.StartClient();
        Hide();
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
