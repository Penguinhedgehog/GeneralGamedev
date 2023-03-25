using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Note: must be connected to a switch/trigger")]
    [SerializeField] GameObject spawnObject;
    [Tooltip("Set to true to continously spawn enemies as per the spawn interval")]
    [SerializeField] bool continousSpawner = false;
    [SerializeField] float spawnInterval = 5f;

    [SerializeField] bool isActive;
    [SerializeField] bool startActive;


    private void Start() {
        if(startActive) {
            SwitchActive();
        }
    }


    public void SwitchActive() {
        isActive = !isActive;
        if(isActive) {
            StartCoroutine(SpawningInterval());
        }
    }

    private IEnumerator SpawningInterval() {
        Instantiate(spawnObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnInterval);
        if(continousSpawner) {
            StartCoroutine(SpawningInterval());
        }
    }


}
