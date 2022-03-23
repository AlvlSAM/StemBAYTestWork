using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseAI
{
    public abstract void Observation();
    public abstract void Move();
}
