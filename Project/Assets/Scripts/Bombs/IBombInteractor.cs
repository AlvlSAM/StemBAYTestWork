using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBombInteractor
{
    public abstract void Interact(BombType bombType, BombDetonate bombDetonate);
}
