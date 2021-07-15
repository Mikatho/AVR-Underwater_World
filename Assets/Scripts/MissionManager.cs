using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

//Kümmert sich um die Missionen der Level
public class MissionManager : MonoBehaviour
{
    public GameObject[] missions;

    private int solvedMissionsCounter;

    void Start()
    {
        foreach (GameObject mission in missions) {
            mission.transform.Find("Checkbox").gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //Beendet das Spiel, sobald alle Missionen des Levels erfüllt wurden 
        if (missions.Length == solvedMissionsCounter && !GameSceneManager.alreadyFinished) {
            GameSceneManager.finished = true;
            GameSceneManager.alreadyFinished = true;
        }
    }

    //Prüft, welche Szene/Level gerade läuft und bearbeitet je nachdem die Missionen
    public void MissionSolved(string solvedMission) {
        switch (SceneManager.GetActiveScene().buildIndex) {
            case 1:
                MissionsInTutorial(solvedMission);
                break;
            case 2:
                MissionsInLevel1(solvedMission);
                break;
            default:
                Debug.Log("Scene is not a Level.");
                break;
        }
    }

    //Prüft, welche Mission gerade im Tutorial erfüllt wurde
    private void MissionsInTutorial(string solvedMission) {
        int missionsIndex;

        switch (solvedMission) {
            case "Kleiner Schwarzspitzenhai":
                missionsIndex = 0;
                break;
            case "Bastardschildkröte":
                missionsIndex = 1;
                break;
            case "StonePlaced":
                missionsIndex = 2;
                break;
            default:
                Debug.Log("Animal does not exist in this Level.");
                return;
        }
        missions[missionsIndex].transform.Find("Checkbox").gameObject.SetActive(true);
        solvedMissionsCounter += 1;
    }

    //Prüft, welche Mission gerade in Level1 erfüllt wurde
    private void MissionsInLevel1(string solvedMission) {
        int missionsIndex;
        switch (solvedMission) {
            case "Großer Tümmler":
                missionsIndex = 0;
                break;
            case "Weißer Hai":
                missionsIndex = 1;
                break;
            case "Grüne Meeresschildkröte":
                missionsIndex = 2;
                break;
            case "Hammerhai":
                missionsIndex = 3;
                break;
            default:
                Debug.Log("Animal does not exist in this Level.");
                return;
        }
        missions[missionsIndex].transform.Find("Checkbox").gameObject.SetActive(true);
        solvedMissionsCounter += 1;
    }
}
