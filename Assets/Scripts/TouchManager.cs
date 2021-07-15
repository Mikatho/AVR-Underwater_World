using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Kümmert sich um jegliche Touch-Inputs/Maus-Inputs
public class TouchManager : MonoBehaviour
{
    public GameObject fishFood;
    
    public GameObject shrimpFood;
    
    public GameObject algaFood;

    private float spawnDistance = 3;

    void Start()
    {

    }

    void FixedUpdate()
    {
        //Prüft, ob ein Touch-Input registriert wurde
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {

                //Prüft, ob der Touch-Input auf ein UI-Element ausgeführt wurde
                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
                    SpawnFood(touch.position);
                }
            }
        }

//Wenn Anwendung im Unity Editor ausgeführt wird kann der Touch-Input mit der Maus simuliert und getestet werden
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {

            if (!EventSystem.current.IsPointerOverGameObject()) {
                SpawnFood(Input.mousePosition);
            }
        }
#endif
    }

    //Spawnt Futter, je nachdem welches Futter vorher in der UI ausgewählt wurde
    //Spawnt nichts, falls kein Futter vorher ausgewählt wurde
    private void SpawnFood(Vector3 position) {
        GameObject clone;
        GameObject prefab;

        position.z = spawnDistance;
        position = Camera.main.ScreenToWorldPoint(position);

        if (GameSceneManager.foodFishSelected) {
            prefab = fishFood;
        } else if (GameSceneManager.foodShrimpSelected) {
            prefab = shrimpFood;
        } else if (GameSceneManager.foodAlgaSelected) {
            prefab = algaFood;
        } else {
            return;
        }

        clone = Instantiate(prefab, position, prefab.transform.rotation);
        clone.transform.parent = GameObject.Find("Sea/FoodContainer").transform;

        GameSceneManager.foodFishSelected = false;
        GameSceneManager.foodShrimpSelected = false;
        GameSceneManager.foodAlgaSelected = false;
    }
}
