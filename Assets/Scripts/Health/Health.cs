using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth == 0) { return; }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
    }

    public void Heal(float healAmount)
    {
        if (currentHealth == 0) { return; }

        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }
}
