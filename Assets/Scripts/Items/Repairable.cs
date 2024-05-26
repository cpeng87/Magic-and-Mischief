using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{
    public RepairableData repairableData;
    public GameObject after;

    public void Repair()
    {
        Vector3 position = this.gameObject.transform.position;
        Quaternion rotation = this.gameObject.transform.rotation;
        GameObject newGameObject = Instantiate(after, position, rotation);
        Destroy(this.gameObject);
    }
}
