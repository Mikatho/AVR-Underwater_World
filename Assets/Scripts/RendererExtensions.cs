using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://wiki.unity3d.com/index.php?title=IsVisibleFrom

//Klasse prüft, ob das Objekt von der gegebenen Kamera gesehen wird
public static class RendererExtensions
{
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera) {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
