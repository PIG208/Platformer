using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Entity))]
public class RedShroomManager : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Canvas;

    Entity _entity;

    private void Start()
    {
        _entity = GetComponent<Entity>();
        HealthManager healthManager = GetComponent<HealthManager>();
        healthManager.Hurt += HandleHurt;
        healthManager.Die += HandleDie;
    }

    private void HandleHurt(HealthManager healthManager)
    {
        Canvas.SetActive(true);
        Portal.SetActive(false);

        AIManager ai = _entity.GetComponent<AIManager>();
        WeaponManager weaponManager = LevelManager.CurrentLevelManager.Player.Inventory.CurrentWeaponManager;
        HandleSwitchWeapon(weaponManager.Weapon, weaponManager);

        LevelManager.CurrentLevelManager.Player.Inventory.WeaponSwitched += HandleSwitchWeapon;
        ai.AlertDistance = 15f;

        _entity.Health.Hurt -= HandleHurt;
    }

    private void HandleDie(HealthManager healthManager)
    {
        Portal.SetActive(true);
    }

    private void HandleSwitchWeapon(BaseWeapon weapon, WeaponManager manager)
    {
        AIManager ai = GetComponent<AIManager>();
        _entity.Inventory.Weapons = new List<BaseWeapon>() { weapon };
        _entity.Inventory.SwitchWeapon(0);
        ai.AttackDistance = (_entity.Inventory.CurrentWeaponManager.Weapon.GetType() == typeof(Melee)) ? 2f : 6f;
    }
}