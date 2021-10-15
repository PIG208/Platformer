using System.Collections.Generic;

public class FireContext
{
    public readonly Player Player;
    public readonly IEnumerable<Entity> SurroundingTargets;

    public FireContext(Player player, IEnumerable<Entity> targets)
    {
        Player = player;
        SurroundingTargets = targets;
    }
}