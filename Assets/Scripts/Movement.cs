using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Steuert Movement der Fische
public class Movement : MonoBehaviour
{
    private Animal animal;
    private SpawnAreaManager spawnAreaManager;

    private Vector3 waypoint;
    private Vector3 randomWaypoint;
    private Vector3 foodWaypoint;

    public Vector3 setFoodWaypoint { set { foodWaypoint = value; } }

    private Vector3 velocity;

    private float animalspeed;

    void Start()
    {
        animal = GetComponent<Animal>();
        spawnAreaManager = transform.parent.GetComponentInParent<SpawnAreaManager>();

        StartCoroutine(RandomWaypoint());

        animalspeed = animal.getSpeed/15;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move() {
        //Prüft, ob Fisch als Zielpunkt bereits sein Essen hat
        if (animal.hasFoodTarget) {
            //Mathematische Berechnung, damit der Fisch sich zum Essen dreht und dahin schwimmt
            waypoint = Vector3.SmoothDamp(waypoint, foodWaypoint, ref velocity, 3.0f);
            Vector3 newDir = Vector3.RotateTowards(transform.forward, foodWaypoint - transform.position, Time.deltaTime * animal.getTurnSpeed * 1.25f, 0);
            transform.rotation = Quaternion.LookRotation(newDir);

            transform.position = Vector3.MoveTowards(transform.position, foodWaypoint, Time.deltaTime * animalspeed);
        } else {
            //Prüft, ob der Fisch sich horizontal gerade richtet
            if (animal.lookingUp) {
                //Wenn er es tut schwimmt er nur geradeaus, damit seine Rotation keine Bugs hervorruft
                transform.Translate(Vector3.forward * Time.deltaTime * animalspeed);
            } else {
                //Mathematische Berechnung, damit der Fisch natürlich schwimmt
                waypoint = Vector3.SmoothDamp(waypoint, randomWaypoint, ref velocity, 3.0f);
                waypoint = new Vector3(waypoint.x, animal.getStartingY, waypoint.y);
                Vector3 newDir = Vector3.RotateTowards(transform.forward, waypoint - transform.position, Time.deltaTime * animal.getTurnSpeed, 0);
                transform.rotation = Quaternion.LookRotation(newDir);
                transform.Translate(Vector3.forward * Time.deltaTime * animalspeed);
            }
        }
    }

    //Coroutine setzt alle paar Sekunden einen neuen Wegpunkt/ein neues Ziel des Fischs
    IEnumerator RandomWaypoint() {
        while (true)
        {
            randomWaypoint = spawnAreaManager.RandomPosition();
            yield return new WaitForSeconds(5.0f);
        }
    }
}
