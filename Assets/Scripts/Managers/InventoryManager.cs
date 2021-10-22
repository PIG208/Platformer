using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform EquipementPosition;
    public WeaponManager CurrentWeaponManager;
    public List<BaseWeapon> Weapons;
    public List<ExtensionRegistry> Extensions;
    public float SwitchInterval = Constants.SwitchWeaponInterval;
    [Tooltip("The weapons this inventory manager will start with")]
    public InventoryPreset Preset = InventoryPreset.Empty;

    [System.NonSerialized]
    public List<CollectableManager> SurroundingCollectables = new List<CollectableManager>();
    public Text PickupText;
    public bool PickupEnabled = false;

    private int _currentWeaponIndex = -1;
    private float _lastSwitch;

    public event Action<BaseWeapon, WeaponManager> WeaponSwitched;

    private void Start()
    {
        Weapons = Preset.GetItems();

        NextWeapon();
    }

    private void Update()
    {
        if (_lastSwitch > 0) _lastSwitch -= Time.deltaTime;
    }

    private void UpdateUI()
    {
        if (SurroundingCollectables.Count > 0)
        {
            PickupText.enabled = true;
            if (SurroundingCollectables[0].IsExtension)
            {
                PickupText.text = $"[E] Install {SurroundingCollectables[0].Name} to {CurrentWeaponManager.Name}";
            }
            else
            {
                PickupText.text = $"[E] Pickup {SurroundingCollectables[0].Name}";
            }
        }
        else
        {
            PickupText.enabled = false;
        }
    }

    public void AddCanPickup(CollectableManager collectableManager)
    {
        SurroundingCollectables.Add(collectableManager);
        UpdateUI();
    }

    public void RemoveCanPickup(CollectableManager collectableManager)
    {
        SurroundingCollectables.Remove(collectableManager);
        UpdateUI();
    }

    /// <summary>Will attempt to pick up the first surrounding collectables</summary>
    public void TryPickup()
    {
        if (SurroundingCollectables.Count == 0) return;

        CollectableManager target = SurroundingCollectables[0];
        SurroundingCollectables.RemoveAt(0);
        if (target.IsExtension)
        {
            if (target.Extension == ExtensionRegistry.None) throw new InvalidOperationException("Cannot pickup a null extension");
            Extensions.Add(target.Extension);
            if (target.Extension.IsGun())
            {
                ((Gun)CurrentWeaponManager.Weapon).RegisterModifier(target.Extension.Modifier<Gun>());
            }
            else if (target.Extension.IsMelee())
            {
                ((Melee)CurrentWeaponManager.Weapon).RegisterModifier(target.Extension.Modifier<Melee>());
            }
            else
            {
                CurrentWeaponManager.Weapon.RegisterModifier(target.Extension.Modifier<BaseWeapon>());
            }
        }
        else
        {
            if (target.Weapon == null) throw new InvalidOperationException("Cannot pickup a null weapon");
            Weapons.Add(target.Weapon);
            SwitchWeapon(Weapons.Count - 1);
        }
        Destroy(target.gameObject);
        UpdateUI();
    }

    public void SwitchWeapon(int index)
    {
        if (_lastSwitch > 0 || Weapons.Count == 0) return;
        _lastSwitch = SwitchInterval;

        if (CurrentWeaponManager != null) Destroy(CurrentWeaponManager.gameObject);

        _currentWeaponIndex = index % Weapons.Count;
        if (_currentWeaponIndex < 0) _currentWeaponIndex = Weapons.Count + _currentWeaponIndex;

        CurrentWeaponManager = Weapons[_currentWeaponIndex].Spawn(EquipementPosition);

        if (WeaponSwitched != null) WeaponSwitched(Weapons[_currentWeaponIndex], CurrentWeaponManager);
    }

    public void NextWeapon() => SwitchWeapon(_currentWeaponIndex + 1);
    public void PrevWeapon() => SwitchWeapon(_currentWeaponIndex - 1);
}
