using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseAI
{
    internal enum Control
    {
        Up,
        Right,
        Down,
        Left
    }
    public abstract void Observation();
    public abstract void Move();
    public abstract void GetDamage();
}
