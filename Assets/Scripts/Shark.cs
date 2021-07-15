using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Animal
{
    // Start is called before the first frame update
    public override void Awake()
    {
        commonName = "Shark";
        scientificName = "Carcharhinus limbatus";
        type = Type.Fisch;
        food = Food.FoodFisch;
        size = "bis zu 2,1m";
        weight = "25kg bis 30kg";
        lifeSpan = "15,5 Jahre";
        appearance = "Grau-Braun";
        speed = 22.7f;
        turnSpeed = 0.4f;

        collectionButtonName = "WhiteSharkButton";
    }
}
