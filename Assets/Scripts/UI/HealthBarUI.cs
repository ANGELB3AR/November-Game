using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
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

    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    void UpdateHealthBar()
    {
        healthSlider.value = healthComponent.GetCurrentHealth();
    }
}
