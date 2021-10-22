using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public MovementManager Movement { get => _movement; }
    public InventoryManager Inventory { get => _inventory; }
    public HealthManager Health { get => _health; }
    public Animator EntityAnimator;
    public virtual Group Group => Group.Netural;
    public virtual bool IsPlayer => false;

    private MovementManager _movement;
    private InventoryManager _inventory;
    private HealthManager _health;

    protected void BindManagers()
    {
        _movement = GetComponent<MovementManager>();
        _inventory = GetComponent<InventoryManager>();
        _health = GetComponent<HealthManager>();
    }

    public void Fire()
    {
        Inventory.CurrentWeaponManager.Fire(new FireContext(this, LevelManager.CurrentLevelManager.FindSurroundingTargets(this)));
    }
}