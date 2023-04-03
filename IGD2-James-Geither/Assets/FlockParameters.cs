using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlockParameters : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public List<GameObject> spawnedObjects = new List<GameObject>();

    public Slider spawnSlider; //Done
    public Slider subgroupSlider; //Done
    public Slider compactnessSlider; //Done
    public Slider direcitonSlider;
    public Slider speedSlider; //Done
    public Slider quicknessGroupingSlider;

    private int numSpawned = 0;

    private void Update()
    {
        Spawn();
        Stats();
    }

    public void Stats()
    {
        foreach (var butterfly in spawnedObjects)
        {
            BoidMovement boidMovement = butterfly.GetComponent<BoidMovement>();
            boidMovement.cohesionWeight = compactnessSlider.value;
            boidMovement.speed = speedSlider.value;
            boidMovement.subgroupValue = subgroupSlider.value;
            boidMovement.directionValue = direcitonSlider.value;
        }
    }

    public void Spawn()
    {
        int numToSpawn = (int)spawnSlider.value;
        int diff = numToSpawn - numSpawned;
        if (diff > 0)
        {
            for (int i = 0; i < diff; i++)
            {
                GameObject newObject = Instantiate(prefabToSpawn);
                spawnedObjects.Add(newObject);
            }
        }
        else if (diff < 0)
        {
            diff = Mathf.Abs(diff);
            for (int i = 0; i < diff; i++)
            {
                if (spawnedObjects.Count > 0)
                {
                    GameObject objectToDelete = spawnedObjects[spawnedObjects.Count - 1];
                    spawnedObjects.RemoveAt(spawnedObjects.Count - 1);
                    Destroy(objectToDelete);
                }
            }
        }
        numSpawned = numToSpawn;
    }
}