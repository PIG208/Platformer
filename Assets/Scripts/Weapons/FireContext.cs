using System.Collections.Generic;

public class FireContext
{
    public readonly Entity Player;
    public readonly IEnumerable<Entity> SurroundingTargets;

    public FireContext(Entity player, IEnumerable<Entity> targets)
    {
        Player = player;
        SurroundingTargets = targets;
    }
}