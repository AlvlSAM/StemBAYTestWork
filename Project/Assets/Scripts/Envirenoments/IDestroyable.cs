using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyable
{
    abstract void DoDamage(float damage);
    abstract void Destroy();
}
