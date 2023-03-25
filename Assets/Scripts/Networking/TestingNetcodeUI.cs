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
        NetworkManager.Singleton.StartHost();
        Hide();
    }

    public void StartClient()
    {
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
