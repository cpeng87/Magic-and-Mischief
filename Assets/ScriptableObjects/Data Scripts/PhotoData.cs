using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Photo", menuName = "NPC/Photo")]
public class PhotoData : ScriptableObject
{
    public Sprite photo;
    public string description;
}