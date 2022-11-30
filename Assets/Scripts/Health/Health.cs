using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnHealthUpdated;
    public event Action OnDamageReceived;
    public event Action OnDeath;

    [SerializeField] float maxHealth = 100f;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(float damage)
    {
        if (currentHealth == 0) { return; }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnHealthUpdated?.Invoke();
        OnDamageReceived?.Invoke();

        if (currentHealth == 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float healAmount)
    {
        if (currentHealth == 0) { return; }

        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        OnHealthUpdated?.Invoke();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
