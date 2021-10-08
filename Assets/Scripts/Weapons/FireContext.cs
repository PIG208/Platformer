
public class FireContext
{
    public readonly Player Player;
    public readonly Entity[] SurroundingEnemies;

    public FireContext(Player player, Entity[] enemies)
    {
        Player = player;
        SurroundingEnemies = enemies;
    }
}