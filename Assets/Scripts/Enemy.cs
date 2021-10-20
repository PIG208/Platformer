using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(AIManager))]
public class Enemy : Entity
{
    public override Group Group => Group.Enemy;

    private void Start()
    {
        LevelManager.CurrentLevelManager.Enemies.Add(this);
        GetComponent<HealthManager>().Die += HandleDie;
    }

    private void HandleDie(HealthManager hm)
    {
        LevelManager.CurrentLevelManager.Enemies.Remove(this);
        EntityAnimator.Play("Die");
        Destroy(gameObject, 0.5f);
    }
}
