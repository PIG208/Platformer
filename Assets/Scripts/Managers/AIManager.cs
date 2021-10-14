using UnityEngine;

/// <summary>This will be used as the generic manager for AIs.
/// Enable AI for any movables by attaching this component to the prefab.</summary>
[RequireComponent(typeof(MovementManager))]
public class AIManager : MonoBehaviour
{
    MovementManager _movement;
    public float distance;

    private void Start()
    {
        _movement = GetComponent<MovementManager>();
    }

    private void Update()
    {   
        distance = Vector2.Distance(LevelManager.CurrentLevelManager.Player.transform.position, transform.position);
        if(distance<5){
            _movement.Speed = 4f;
            Vector2 direction = LevelManager.CurrentLevelManager.Player.transform.position - transform.position;
            _movement.Move(direction.normalized);
        }
        else{
            _movement.Speed = 0;
            Vector2 direction;
            direction.x = 0;
            direction.y = 0;
            _movement.Move(direction.normalized);
        }
        
    }
}
