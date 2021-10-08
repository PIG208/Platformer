using UnityEngine;

public class Entity : MonoBehaviour
{
    public MovementManager Movement { get => _movement; }
    public InventoryManager Inventory { get => _inventory; }
    public HealthManager Health { get => _health; }
    public Animator EntityAnimator;

    private MovementManager _movement;
    private InventoryManager _inventory;
    private HealthManager _health;

    protected void BindManagers()
    {
        _movement = GetComponent<MovementManager>();
        _inventory = GetComponent<InventoryManager>();
        _health = GetComponent<HealthManager>();
    }
}