using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSeaTurtle : Animal
{
    public override void Awake()
    {
        commonName = "Grüne Meeresschildkröte";
        scientificName = "Chelonia mydas";
        type = Type.Reptil;
        food = Food.FoodAlge;
        size = "1m bis 1,5m";
        weight = "250kg bis 315kg";
        lifeSpan = "80 Jahre";
        appearance = "Grün-Braun";
        speed = 9.8f;
        turnSpeed = 0.5f;

        collectionButtonName = "GreenSeaTurtleButton";
    }
}
