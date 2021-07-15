using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlenoseDolphin : Animal
{
    public override void Awake()
    {
        commonName = "Großer Tümmler";
        scientificName = "Tursiops truncatus";
        type = Type.Säugetier;
        food = Food.FoodGarnele;
        size = "3m bis 4m";
        weight = "500kg bis 640kg";
        lifeSpan = "45 - 50 Jahre";
        appearance = "Grau-Weiß";
        speed = 34.9f;
        turnSpeed = 0.6f;

        collectionButtonName = "BottlenoseDolphinButton";
    }
}
