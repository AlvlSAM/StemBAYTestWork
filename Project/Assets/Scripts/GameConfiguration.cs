using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "New GameConfiguration")]
public class GameConfiguration : ScriptableObject
{
    public List<int> CountBombs;
}
