using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleData", menuName = "NPC/IdleData")]
public class IdleData : ActivityData
{
    public bool isAnimated;
    public string animationName;
    public Vector3 position;
    public Vector2 direction;
}
