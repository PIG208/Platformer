public interface Modifiable<T> where T : BaseWeapon
{
    void RegisterModifier(IModifier<T> modifier);
}