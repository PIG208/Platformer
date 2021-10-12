public interface IModifier<T> where T : BaseWeapon
{
    void Register(T weapon);
}