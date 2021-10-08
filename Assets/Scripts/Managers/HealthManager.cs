using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public int Health { get; set; }
    public int MaxHealth;
    public Slider HealthBar;

    private void Start()
    {
        Health = MaxHealth;
        UpdateHealthBar();
    }

    public void Damage(int damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        UpdateHealthBar();
    }

    public void Heal(int heal)
    {
        this.Damage(-heal);
    }

    public void UpdateHealthBar()
    {
        if (HealthBar is null) return;
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = Health;
    }
}
