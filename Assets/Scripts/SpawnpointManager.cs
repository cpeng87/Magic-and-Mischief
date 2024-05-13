using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointManager : MonoBehaviour
{
    private Dictionary<string, Transform> nameToSpawnpoint = 
        new Dictionary<string, Transform>();
    // Start is called before the first frame update
    public void OrganizeSpawnpoint()
    {
        // Loop through each child of the parent GameObject
        foreach (Transform spawnpoint in this.transform)
        {
            string name = spawnpoint.gameObject.name;
            if (!nameToSpawnpoint.ContainsKey(name))
            {
                nameToSpawnpoint.Add(name, spawnpoint);
            }
        }
    }
    public bool CheckContainsKey(string key)
    {
        if (key == null)
        {
            return false;
        }
        return nameToSpawnpoint.ContainsKey(key);
    }
    public Vector3 GetPositionByName(string name)
    {
        return nameToSpawnpoint[name].position;
    }
}
