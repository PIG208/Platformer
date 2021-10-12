using System;

[Flags]
public enum Group
{
    HostileToAll = 0,
    FriendlyToPlayer = 1,
    FriendlyToEnemy = 2,
}