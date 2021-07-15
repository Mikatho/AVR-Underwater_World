using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Verwaltet den Bereich, in dem Fische gespawnt werden und sich bewegen können
public class SpawnAreaManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 spawnArea = new Vector3(20f, 6f, 20f);

    [SerializeField]
    private Vector3 spawnAreaSigns = new Vector3(5f, 2f, 5f);

    public Vector3 getSpawnArea { get { return spawnArea; } }
    public Vector3 getSpawnAreaSigns { get { return spawnAreaSigns; } }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //Gibt random Position des Bereichs zurück
    public Vector3 RandomPosition() {
        Vector3 randomPosition = new Vector3(
            Random.Range(-getSpawnArea.x, getSpawnArea.x),
            Random.Range(-getSpawnArea.y, getSpawnArea.y),
            Random.Range(-getSpawnArea.z, getSpawnArea.z)
        );
        randomPosition = transform.TransformPoint(randomPosition * 0.5f);

        return randomPosition;
    }

    //Gibt random Position des Bereichs eines Schildes zurück
    public Vector3 RandomPositionSign() {
        Vector3 randomPosition = new Vector3(Random.Range(-getSpawnAreaSigns.x, getSpawnAreaSigns.x), -3, Random.Range(-getSpawnAreaSigns.z, getSpawnAreaSigns.z));
        randomPosition = transform.TransformPoint(randomPosition * 0.5f);

        return randomPosition;
    }

    //Stellt den Bereich grafisch da, falls dieser im Editor ausgewählt wird
    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawCube(transform.position, getSpawnArea);
    }
}
