using System.Collections.Generic;
using UnityEngine;

/// <summary>Each level needs to have a LevelManager, and only one LevelManager can exist in each level</summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager CurrentLevelManager { get; private set; }
    public Player Player;
    public List<Enemy> Enemies = new List<Enemy>();


    private void Awake()
    {
        CurrentLevelManager = this;
    }

    public IEnumerable<Entity> FindSurroundingTargets(Entity entity, float MaxDistance = 10f)
    {
        if (entity.IsPlayer)
        {
            return Enemies.FindAll(enemy => Vector2.Distance(enemy.transform.position, entity.transform.position) < MaxDistance);
        }
        else
        {
            return (Vector2.Distance(entity.transform.position, Player.transform.position) < MaxDistance)
                    ? new Player[] { Player } : new Entity[] { };
        }
    }
}
