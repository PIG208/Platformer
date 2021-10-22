using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public int Health { get; set; }
    public int MaxHealth;
    public Slider HealthBar;

    public event Action<HealthManager> Die;
    public event Action<HealthManager> Died;

    private bool isDead;

    private void Start()
    {
        Health = MaxHealth;
        UpdateHealthBar();
    }

    public void Damage(int damage)
    {
        if (isDead) return;

        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        if (Health <= 0)
        {
            Die?.Invoke(this);
            isDead = true;
            Died?.Invoke(this);
        };
        UpdateHealthBar();
    }

    public void Heal(int heal)
    {
        this.Damage(-heal);
    }

    public void Respawn()
    {
        Health = MaxHealth;
        isDead = false;
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (HealthBar is null) return;
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = Health;
    }
}
