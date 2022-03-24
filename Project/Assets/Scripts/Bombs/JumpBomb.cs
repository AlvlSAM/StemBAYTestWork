using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBomb : FreezeBomb
{
    internal override void SetType()
    {
        bombType = BombType.Jumper;
    }
}
