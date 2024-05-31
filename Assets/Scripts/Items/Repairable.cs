using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{
    public RepairableData repairableData;
    public GameObject after;

    private void Start()
    {
        bool isRepaired = PlayerPrefs.GetInt(this.gameObject.name, 0) == 1;
        if (isRepaired)
        {
            Repair();
        }
    }

    public void Repair()
    {
        PlayerPrefs.SetInt(this.gameObject.name, 1); // Using 0 to represent false
        PlayerPrefs.Save();

        Vector3 position = this.gameObject.transform.position;
        Quaternion rotation = this.gameObject.transform.rotation;
        GameObject newGameObject = Instantiate(after, position, rotation);
        Destroy(this.gameObject);
    }
}
