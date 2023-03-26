using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour
{
    PlayerController Player;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
    }

    public void EquipSword()
    {
        Player.Weapon.UnequipWeapon();
        Player.Weapon.EquipWeapon(Resources.Load<WeaponConfig>("Test Sword"));
    }

    public void EquipSpear()
    {
        Player.Weapon.UnequipWeapon();
        Player.Weapon.EquipWeapon(Resources.Load<WeaponConfig>("Test Spear"));
    }

    public void EquipHeavy()
    {
        Player.Weapon.UnequipWeapon();
        Player.Weapon.EquipWeapon(Resources.Load<WeaponConfig>("Test Heavy"));
    }
}
