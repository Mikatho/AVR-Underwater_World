using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;
using TMPro;

//Enthält Funktionen der Buttons des UI in der GameScene
public class GameSceneManager : MonoBehaviour
{
    public MissionManager missionManager;
    
    public GameObject gameMenu;
    public GameObject pauseMenu;
    public GameObject endMenu;

    public GameObject missionsIcon;
    public GameObject missionsText;
    
    public GameObject collectionIcon;
    public GameObject collection;
    
    public GameObject foodbox;
    public GameObject foodIcons;

    public GameObject buildbox;
    public GameObject buildIcons;

    public GameObject buildCoral;
    public GameObject buildStone;
    public GameObject buildTree;

    public GameObject animalContainer;

    public GameObject pauseButton;

    public GameObject controls;

    public TMP_Text description1;
    public TMP_Text description2;
    public Image avatar;

    public ARPlaneManager planeManager;

    public ARPlacementInteractable placementInteractable;

    private static bool foodVisible = false;
    private static bool missionsVisible = false;
    private static bool collectionVisible = false;
    private static bool controlsVisible = false;

    public static bool foodFishSelected = false;
    public static bool foodShrimpSelected = false;
    public static bool foodAlgaSelected = false;

    public static bool paused = false;
    public static bool finished = false;
    public static bool alreadyFinished = false;

    public static int animalsMarked = 0;

    //UI-Elemente, welche nicht sofort gezeigt werden sollen werden ausgeblendet
    void Start()
    {
        pauseMenu.SetActive(false);

        endMenu.SetActive(false);

        foodIcons.SetActive(false);

        missionsText.SetActive(false);

        collection.SetActive(false);

        buildIcons.SetActive(false);

        controls.SetActive(false);

        planeManager.enabled = false;
        placementInteractable.enabled = false;

        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //Kontrolliert, ob alle Missionen erfüllt wurden
        if (finished) {
            EndGame();
        }
    }

    //Symbole des Essens werden ein-/ausgeblendet
    public void ManageFoodSymbols() {
        foodVisible = !foodVisible;

        buildIcons.SetActive(false);

        foodIcons.SetActive(foodVisible);

        planeManager.enabled = false;
        placementInteractable.enabled = false;

        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(false);
        }

        foodFishSelected = false;
        foodShrimpSelected = false;
        foodAlgaSelected = false;
    }

    //Missionsfeld wird ein-/ausgeblendet
    public void ManageMissions() {
        missionsVisible = !missionsVisible;

        foodIcons.SetActive(false);
        buildIcons.SetActive(false);

        planeManager.enabled = false;
        placementInteractable.enabled = false;

        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(false);
        }

        missionsText.SetActive(missionsVisible);
    }

    //Fischkollektion wird ein-/ausgeblendet
    public void ManageCollection() {
        collectionVisible = !collectionVisible;

        foodIcons.SetActive(false);
        foodVisible = false;
        buildIcons.SetActive(false);

        //Verhindert, dass andere Buttons gedrückt werden können sobald die Collection geöffnet ist
        missionsIcon.GetComponent<Button>().interactable = !collectionVisible;
        foodbox.GetComponent<Button>().interactable = !collectionVisible;
        buildbox.GetComponent<Button>().interactable = !collectionVisible;
        
        planeManager.enabled = false;
        placementInteractable.enabled = false;

        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(false);
        }

        collection.SetActive(collectionVisible);
    }

    //Setzt Variablen so, dass nur das Essen "Fisch" ausgewählt ist
    public void ManageFoodFish() {
        foodFishSelected = true;
        foodShrimpSelected = false;
        foodAlgaSelected = false;
    }

    //Setzt Variablen so, dass nur das Essen "Shrimp" ausgewählt ist
    public void ManageFoodShrimp() {
        foodFishSelected = false;
        foodShrimpSelected = true;
        foodAlgaSelected = false;
    }

    //Setzt Variablen so, dass nur das Essen "Alga" ausgewählt ist
    public void ManageFoodAlga() {
        foodFishSelected = false;
        foodShrimpSelected = false;
        foodAlgaSelected = true;
    }

    //Blendet Elemente zum Platzieren auf AR Planes ein/aus
    //Blendet Planes der AR Plane Detection und dazugehörige Funktionen ein/aus
    public void ManageBuildMode() {
        bool buildModeActivated = !planeManager.enabled;

        foodIcons.SetActive(false);
        foodVisible = false;

        buildIcons.SetActive(buildModeActivated);

        planeManager.enabled = buildModeActivated;
        placementInteractable.enabled = buildModeActivated;

        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(buildModeActivated);
        }
    }

    //Setzt Variablen so, dass nur das Bauelement ausgewählt ist und setzt es als Prefab zum platzieren
    public void BuildCoral() {
        placementInteractable.placementPrefab = buildCoral;
    }

    //Setzt Variablen so, dass nur das Bauelement ausgewählt ist und setzt es als Prefab zum platzieren
    public void BuildStone() {
        placementInteractable.placementPrefab = buildStone;
    }

    //Setzt Variablen so, dass nur das Bauelement ausgewählt ist und setzt es als Prefab zum platzieren
    public void BuildTree() {
        placementInteractable.placementPrefab = buildTree;
    }

    //Methode wird ausgeführt nachdem ein GameObject per AR Placement Interactable platziert wurde
    public void BuildingElementPlaced(ARPlacementInteractable placement, GameObject placedObject) {
        placementInteractable.placementPrefab = null;

        if (placedObject.tag == "Stone") {
            missionManager.MissionSolved("StonePlaced");
        }
    }

    //Setzt Collection-Eintrag des Fisches
    public void SelectCollection() {
        Animal[] animals = animalContainer.GetComponentsInChildren<Animal>();

        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

        Debug.Log("Step1");

        if (selectedButton != null) {
            Debug.Log("Step2");
            foreach(Animal a in animals) {
                Debug.Log("Step3");
                if (selectedButton.name == a.getCollectionButtonName) {
                    Debug.Log("Step4");
                    
                    description1.fontSize = 26;
                    description1.text = a.FormatCollectionEntry()[0];
                    description2.text = a.FormatCollectionEntry()[1];
                    avatar.sprite = a.avatar;
                    avatar.color = new Color(255, 255, 255, 255);
                }
            }
        }
    }

    //Steuert, dass nichts anderes gedrückt werden kann, wenn Pause- oder End-Menu gezeigt werden
    //Legt Variablen auf Default-Value, damit keine Dinge im Pause-/End-Menu platziert werden können
    private void PauseInGameUI(bool pause) {
        AudioListener.pause = pause;

        missionsIcon.GetComponent<Button>().interactable = !pause;
        collectionIcon.GetComponent<Button>().interactable = !pause;
        foodbox.GetComponent<Button>().interactable = !pause;
        buildbox.GetComponent<Button>().interactable = !pause;
        pauseButton.GetComponent<Button>().interactable = !pause;

        foodIcons.SetActive(false);
        buildIcons.SetActive(false);

        collection.SetActive(false);
        missionsText.SetActive(false);

        foodVisible = false;
        missionsVisible = false;
        collectionVisible = false;

        foodAlgaSelected = false;
        foodFishSelected = false;
        foodShrimpSelected = false;

        planeManager.enabled = false;
        placementInteractable.enabled = false;

        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(false);
        }
    }

    //Spiel wird pausiert
    public void PauseGame() {
        paused = true;
        if (paused) {
            Time.timeScale = 0f;
            PauseInGameUI(true);
            pauseMenu.SetActive(true);
        }
    }

    //Spiel wird nach einem Pause-Bildschirm weitergeführt
    public void Resume() {
        finished = false;
        paused = false;
        if (!paused) {
            PauseInGameUI(false);
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            endMenu.SetActive(false);
        }
    }

    //Level wird geneustartet
    public void Restart() {
        finished = false;
        alreadyFinished = false;
        PauseInGameUI(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Steuerungen werden angezeigt
    public void Controls() {
        controlsVisible = !controlsVisible;

        controls.SetActive(controlsVisible);
    }

    //Ziel wurde erreicht und End-Screen wird angezeigt
    public void EndGame() {
        Time.timeScale = 0f;
        PauseInGameUI(true);
        endMenu.SetActive(true);
    }

    //Methode um ins Hauptmenu zu gelangen
    public void BackToMenu() {
        finished = false;
        alreadyFinished = false;
        SceneManager.LoadScene(0);
    }
}
