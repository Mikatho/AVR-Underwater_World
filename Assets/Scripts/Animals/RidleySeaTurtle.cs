using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidleySeaTurtle : Animal
{
    public override void Awake()
    {
        commonName = "Bastardschildkröte";
        scientificName = "Lepidochelys kempii";
        type = Type.Reptil;
        food = Food.FoodAlge;
        size = "61cm bis 67cm";
        weight = "45kg";
        lifeSpan = "50 Jahre";
        appearance = "Grau-Grün-Braun";
        speed = 9.8f;
        turnSpeed = 0.5f;

        collectionButtonName = "RidleySeaTurtleButton";
    }
}
