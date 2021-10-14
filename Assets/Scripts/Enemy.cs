using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIManager))]
public class Enemy : Entity
{
    public override Group Group { get => Group.FriendlyToEnemy; }

}
