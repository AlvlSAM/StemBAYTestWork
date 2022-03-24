using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBomb : FreezeBomb
{
    internal override void SetType()
    {
        bombType = BombType.Destroyer;
    }
}
