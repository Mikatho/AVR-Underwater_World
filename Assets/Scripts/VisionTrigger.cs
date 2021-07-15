using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Verwaltet Sichtbereichs der Fische
public class VisionTrigger : MonoBehaviour
{
    private Animal animal;

    private Movement movement;

    void Start()
    {
        animal = GetComponentInParent<Animal>();
        
        movement = GetComponentInParent<Movement>();
    }

    void Update()
    {
        
    }

    //Setzt gefundenes Essen als neuen Wegpunkt des Fischs
    void FoundFood(Vector3 foodWaypoint) {
        animal.hasFoodTarget = true;
        
        movement.setFoodWaypoint = foodWaypoint;
    }

    //Wird getriggert, wenn ein GameObject in das Sichtfeld vom Fisch ist
    void OnTriggerEnter(Collider collider) {
        try {
            //Prüft, ob das GameObject das Essen des Fischs ist
            if (collider.gameObject.tag == animal.getFood) {
                FoundFood(collider.transform.position);
            }
        } catch (NullReferenceException e) {
            Debug.Log(e);
        }
    }
}
