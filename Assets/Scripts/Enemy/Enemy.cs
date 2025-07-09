using UnityEngine;

[RequireComponent(typeof(Patroller))]
public class Enemy : Creature
{
    protected override void Awake()
    {
        base.Awake();
    }
}