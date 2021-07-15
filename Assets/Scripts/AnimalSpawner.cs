using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawnt Tiere zu Start des Programms
public class AnimalSpawner : MonoBehaviour
{
    //Anzahl der Tiere inklusive Prefabs kann im Inspector in Unity gesetzt werden
    [SerializeField]
    private List<Animal> animals = new List<Animal>();

    public SpawnAreaManager spawnAreaManager;

    void Start()
    {
        SpawnAnimals();
    }

    void Update()
    {
        
    }

    //Spawnt gesetzte Anzahl der Fische mit unterschiedlichen Positionen und y-Rotationen
    private void SpawnAnimals() {
        for (int i=0; i<animals.Count; i++) {
            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            //Prüft noch einmal, ob Tier enen Prefab zum kreieren enthält
            if (animals[i].prefab != null) {
                GameObject tempSpawn;
                tempSpawn = Instantiate(animals[i].prefab, spawnAreaManager.RandomPosition(), randomRotation);
                //Setzt alle Tiere in ein Empty GameObject "AnimalContainer"
                tempSpawn.transform.parent = GameObject.Find("AnimalContainer").transform;
                tempSpawn.AddComponent<Movement>();
            }
        }
    }
}
