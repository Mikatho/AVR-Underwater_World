using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawnt Schilder mit Essensinformationen der Tiere zu Start des Programms
public class FoodSignSpawner : MonoBehaviour
{
    //Anzahl der Schilder inklusive Prefabs kann im Inspector in Unity gesetzt werden
    [SerializeField]
    private List<GameObject> signPrefabs = new List<GameObject>();

    private List<GameObject> spawnedSigns = new List<GameObject>();

    public SpawnAreaManager spawnAreaManager;

    public GameObject cameraToLookAt;

    void Start()
    {
        SpawnSigns();
    }

    void Update()
    {
        
    }

    //Spawnt gesetzte Anzahl der Schilder mit unterschiedlichen Positionen und mit Rotation Richtung Spawnpunkt des Spielers
    private void SpawnSigns() {
        for (int i=0; i<signPrefabs.Count; i++) {
            Quaternion defaultRot = Quaternion.Euler(0, 0, 0);

            Vector3 randomPos = FindRandomPosition();

            GameObject tempSpawn;
            tempSpawn = Instantiate(signPrefabs[i], randomPos, defaultRot);
            tempSpawn.transform.parent = gameObject.transform;

            Vector3 vector = cameraToLookAt.transform.position - randomPos;
        
            vector.x = 0;
            vector.z = 0;

            tempSpawn.transform.LookAt(cameraToLookAt.transform.position - vector); 
            tempSpawn.transform.Rotate(0,180,0);
            
            spawnedSigns.Add(tempSpawn);
        }
    }

    //Findet passende random Position für Schild
    private Vector3 FindRandomPosition() {
        Vector3 randomPosition = spawnAreaManager.RandomPositionSign();

        //Sorgt dafür, dass Schilder nicht zu nah aneinander spawnen
        foreach(GameObject sign in spawnedSigns) {
            float distance = Vector3.Distance(sign.transform.position, randomPosition);
            if (distance < 1) {
                FindRandomPosition();
            }
        }
        return randomPosition;
    }
}
