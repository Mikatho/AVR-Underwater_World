using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteShark : Animal
{
    public override void Awake()
    {
        commonName = "Weißer Hai";
        scientificName = "Carcharodon carcharias";
        type = Type.Fisch;
        food = Food.FoodFisch;
        size = "4,5m bis 6m";
        weight = "680kg bis 1800kg";
        lifeSpan = "70 Jahre";
        appearance = "Grau-Weiß";
        speed = 40.1f;
        turnSpeed = 0.6f;

        collectionButtonName = "WhiteSharkButton";
    }
}
