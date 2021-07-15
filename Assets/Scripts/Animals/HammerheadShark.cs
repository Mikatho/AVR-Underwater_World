using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerheadShark : Animal
{
    public override void Awake()
    {
        commonName = "Hammerhai";
        scientificName = "Sphyrnidae";
        type = Type.Fisch;
        food = Food.FoodFisch;
        size = "4m bis 6m";
        weight = "230kg bis 450kg";
        lifeSpan = "20 bis 30 Jahre";
        appearance = "Grau-Weiß";
        speed = 40.2f;
        turnSpeed = 0.6f;

        collectionButtonName = "HammerheadSharkButton";
    }
}
