using UnityEngine;

/// <summary>This will be used as the generic manager for AIs.
/// Enable AI for any movables by attaching this component to the prefab.</summary>
[RequireComponent(typeof(Entity))]
public class AIManager : MonoBehaviour
{
    Entity _entity;
    public float distance;
    public float AlertDistance = 10f;
    public float AttackDistance = 2f;
    public float AttackInterval = 1f;

    private bool disabled = false;
    private float _attackTimeout = 0;

    private void Start()
    {
        _entity = GetComponent<Entity>();
        GetComponent<HealthManager>().Die += HandleDie;
    }

    private void HandleDie(HealthManager healthManager)
    {
        disabled = true;
        _entity.Movement.Move(Vector2.zero);
    }

    private void Update()
    {
        if (disabled) return;
        if (_attackTimeout > 0) _attackTimeout -= Time.deltaTime;
        distance = Vector2.Distance(LevelManager.CurrentLevelManager.Player.transform.position, transform.position);
        if (distance < AlertDistance)
        {
            if (distance > AttackDistance)
            {
                Vector2 direction = LevelManager.CurrentLevelManager.Player.transform.position - transform.position;
                _entity.Movement.Move(direction.normalized);
            }
            else
            {
                if (_attackTimeout <= 0)
                {
                    _entity.Fire();
                    _attackTimeout = AttackInterval;
                }
                _entity.Movement.Move(Vector2.zero);
            }
        }
        else
        {
            _entity.Movement.Move(Vector2.zero);
        }

    }
}
