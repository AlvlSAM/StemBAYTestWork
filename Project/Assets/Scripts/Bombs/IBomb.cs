using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BombDetonate(BombType bombType, BombDetonate unSubEvent);

public enum BombType
{
    Freeze,
    Destroyer,
    Jumper
}
public interface IBomb
{
    public abstract void Detonate();
}
