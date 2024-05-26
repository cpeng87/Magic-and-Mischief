using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Repairable", menuName = "InteractableObjects/Repairable")]
public class RepairableData : ScriptableObject
{
    public string repairableName;
    public List<IngredientListing> ingredients = new List<IngredientListing>();
}
