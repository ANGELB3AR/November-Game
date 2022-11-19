using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action HealthUpdated;

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
        HealthUpdated?.Invoke();
    }

    public void Heal(float healAmount)
    {
        if (currentHealth == 0) { return; }

        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        HealthUpdated?.Invoke();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
