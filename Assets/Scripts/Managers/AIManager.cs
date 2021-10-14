using UnityEngine;

/// <summary>This will be used as the generic manager for AIs.
/// Enable AI for any movables by attaching this component to the prefab.</summary>
[RequireComponent(typeof(MovementManager))]
public class AIManager : MonoBehaviour
{
    MovementManager _movement;

    private void Start()
    {
        _movement = GetComponent<MovementManager>();
    }

    private void Update()
    {
        Vector2 direction = LevelManager.CurrentLevelManager.Player.transform.position - transform.position;
        _movement.Move(direction.normalized);
    }
}
