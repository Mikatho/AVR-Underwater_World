using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public enum Level {Tutorial, Level1};

//Enthält Funktionen der Buttons des UI in der StartScene
public class StartSceneManager : MonoBehaviour
{
    public GameObject controls;

    public AudioSource audioSource;
    public AudioClip tutorialAmbiente;
    public AudioClip level1Ambiente;
    
    public TMP_Text levelname;

    public Image background;

    public Sprite tutorialSprite;
    public Sprite level1Sprite;

    private Level selectedLevel;

    private static bool controlsVisible = false;

    void Start()
    {
        AudioListener.pause = false;
        
        controls.SetActive(false);
        selectedLevel = Level.Tutorial;

        Time.timeScale = 1f;
        audioSource.clip = tutorialAmbiente;
        audioSource.Play();
    }

    void Update()
    {
        
    }

    //Startet die Levelszene, welche vorher ausgewählt wurde
    public void StartGame() {
        GameSceneManager.alreadyFinished = false;
        GameSceneManager.finished = false;
        Time.timeScale = 1f;

        switch (selectedLevel) {
            case Level.Tutorial:
                SceneManager.LoadScene(1);
                break;
            case Level.Level1:
                SceneManager.LoadScene(2);
                break;
            default:
                Debug.Log("No level selected.");
                break;
        }

        selectedLevel = Level.Tutorial;
    }

    //Setzt Hintergrund und Infos passend zum vom User ausgewählten Level
    public void SelectLevel() {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;

        switch (selectedButton.name) {
            case "TutorialButton":
                selectedLevel = Level.Tutorial;
                levelname.text = "Golf von Mexiko (Tutorial)";
                background.sprite = tutorialSprite;
                audioSource.clip = tutorialAmbiente;
                audioSource.Play();
                break;
            case "Level1Button":
                selectedLevel = Level.Level1;
                levelname.text = "Pazifischer Ozean";
                background.sprite = level1Sprite;
                audioSource.clip = level1Ambiente;
                audioSource.Play();
                break;
            default:
                Debug.Log("Level does not exist.");
                break;
        }
    }

    //Steuerungen werden angezeigt
    public void Controls() {
        controlsVisible = !controlsVisible;

        controls.SetActive(controlsVisible);
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
