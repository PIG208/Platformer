using UnityEngine;

public class Entity : MonoBehaviour
{
    public MovementManager Movement { get => _movement; }
    public InventoryManager Inventory { get => _inventory; }

    private MovementManager _movement;
    private InventoryManager _inventory;

    protected void BindManagers()
    {
        _movement = GetComponent<MovementManager>();
        _inventory = GetComponent<InventoryManager>();
    }
}