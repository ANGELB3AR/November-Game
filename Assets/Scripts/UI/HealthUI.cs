using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    Health healthComponent;

    void Awake()
    {
        healthComponent = GetComponentInParent<Health>();
    }

    void OnEnable()
    {
        healthComponent.HealthUpdated += UpdateHealthBar;
    }

    void Start()
    {
        healthSlider.maxValue = healthComponent.GetMaxHealth();
        healthSlider.value = healthComponent.GetMaxHealth();
    }

    void UpdateHealthBar()
    {
        healthSlider.value = healthComponent.GetCurrentHealth();
    }
}
