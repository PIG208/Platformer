using UnityEngine;

public abstract class WeaponManager : MonoBehaviour
{
    public string Name;
    public Rarity Rarity;
    public float Power;
    public GameObject[] Slots;
    public Animator WeaponAnimator;

    public BaseWeapon Weapon;

    public abstract void Fire(FireContext fireContext);
}
