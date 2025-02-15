using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Itemgenerator : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] Transform origin;
    [SerializeField] float originDeadZone;
    [SerializeField] float spawnMaxRadius;

    [SerializeField] int maxObjectsOnScene;
    [SerializeField] float spawnInterval;
    [SerializeField] SpawnableItems[] spawnableItems;

    [SerializeField] bool generationToggle;
    [SerializeField] float timeSinceLastgen = 0f;

    [SerializeField] List<GameObject> spawnedObjects;

    [SerializeField] float xRotationClamp;

    private void Update() {
        foreach (var item in spawnedObjects) {
            if (item.gameObject == null) {
                spawnedObjects.Remove(item);
                break;
            }
        }

        if (!generationToggle || maxObjectsOnScene <= spawnedObjects.Count) return;
        timeSinceLastgen += Time.deltaTime;

        if (timeSinceLastgen >= spawnInterval) {
            Vector3 position = new Vector3(
                GetRandomPosition(),
                -0.63f,
                GetRandomPosition());

            Vector3 rotation = new Vector3(
                Random.Range(-xRotationClamp, xRotationClamp),
                Random.Range(0,360),
                Random.Range(0,360));

            GameObject item = Instantiate(spawnableItems[Random.Range(0, spawnableItems.Count())].spawnable, transform);
            item.transform.position = position;

            Transform itemModel = item.transform.GetChild(0);
            item.transform.eulerAngles = new Vector3(0,rotation.y,0);
            itemModel.transform.localEulerAngles = new Vector3(rotation.x,0,rotation.z);
            spawnedObjects.Add(item);

            

            timeSinceLastgen = 0f;
            return;
        }
    }

    float GetRandomPosition() {
        float pos = Random.Range(1, 10) > 5 ?
            origin.position.x + Random.Range(originDeadZone, spawnMaxRadius) :
            origin.position.x - Random.Range(originDeadZone, spawnMaxRadius);
        return pos;
    }

}

[Serializable]
public class SpawnableItems {
    [SerializeField] public GameObject spawnable;
    [SerializeField] public float chance; //not implemented
}
