using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacktipShark : Animal
{
    public override void Awake()
    {
        commonName = "Kleiner Schwarzspitzenhai";
        scientificName = "Carcharhinus limbatus";
        type = Type.Fisch;
        food = Food.FoodFisch;
        size = "bis zu 2,1m";
        weight = "25kg bis 30kg";
        lifeSpan = "15,5 Jahre";
        appearance = "Grau-Braun";
        speed = 22.7f;
        turnSpeed = 0.7f;
    
        collectionButtonName = "BlacktipSharkButton";
    }
}
