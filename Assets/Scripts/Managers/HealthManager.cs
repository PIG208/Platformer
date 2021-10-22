using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour
{

    public int Health { get; set; }
    public int MaxHealth;
    public Slider HealthBar;

    public event Action<HealthManager> Die;
    public event Action<HealthManager> Died;

    public AudioClip DeathSound;
    public AudioSource AudioSource;

    private bool isDead;

    private void Start()
    {
        Health = MaxHealth;
        UpdateHealthBar();
    }

    public void Damage(int damage)
    {
        IEnumerator Wait1()
        {
            
            if (AudioSource != null) AudioSource.PlayOneShot(DeathSound);
            yield return new WaitForSeconds(2f);
            
        }
        if (isDead) return;

        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        if (Health <= 0)
        {
            StartCoroutine(Wait1());
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
