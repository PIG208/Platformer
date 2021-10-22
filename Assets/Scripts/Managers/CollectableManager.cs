using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollectableManager : MonoBehaviour
{
    public bool IsExtension;
    public BaseWeapon Weapon;
    public ExtensionRegistry Extension;
    public string Name;
    public string Description;

    private void OnTriggerEnter2D(Collider2D other)
    {
        InventoryManager inventory = other.GetComponent<InventoryManager>();
        Debug.Log("other");
        Debug.Log(inventory);
        if (inventory != null && inventory.PickupEnabled && !inventory.SurroundingCollectables.Contains(this))
        {
            inventory.AddCanPickup(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InventoryManager inventory = other.GetComponent<InventoryManager>();
        if (inventory != null && inventory.PickupEnabled && inventory.SurroundingCollectables.Contains(this))
        {
            inventory.RemoveCanPickup(this);
        }
    }

    private static CollectableManager prepareSpawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // Prepare a collectable GameObject containing the item prefab
        GameObject collectableObject = Instantiate(Resources.Load<GameObject>(Constants.CollectablePrefab), position, rotation);
        Instantiate(prefab, collectableObject.transform);
        return collectableObject.GetComponent<CollectableManager>();
    }

    public static CollectableManager Spawn(BaseWeapon weapon, Vector3 position, Quaternion rotation)
    {
        // Spawn a collectable weapon at the given position
        CollectableManager collectableManager = prepareSpawn(weapon.WeaponPrefab, position, rotation);
        collectableManager.IsExtension = false;
        collectableManager.Weapon = weapon;
        collectableManager.Name = weapon.Name;
        collectableManager.Description = weapon.Description;

        return collectableManager;
    }

    public static CollectableManager Spawn(ExtensionRegistry extensionRegistry, GameObject modifierPrefab, Vector3 position, Quaternion rotation)
    {
        // Spawn a collectable extension at the given position
        CollectableManager collectableManager = prepareSpawn(modifierPrefab, position, rotation);
        collectableManager.IsExtension = true;
        collectableManager.Extension = extensionRegistry;
        collectableManager.Name = extensionRegistry.ExtensionId();
        collectableManager.Description = extensionRegistry.Description();

        return collectableManager;
    }
}