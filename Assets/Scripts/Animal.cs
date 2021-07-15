using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum Food {FoodAlge, FoodFisch, FoodGarnele};

public enum Type {Säugetier, Fisch, Reptil, Vogel, Amphibie, Wirbelloser};

public abstract class Animal : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    protected string commonName;
    [SerializeField]
    protected string scientificName;
    [SerializeField]
    protected Type type;
    [SerializeField]
    protected string size;
    [SerializeField]
    protected string weight;
    [SerializeField]
    protected string lifeSpan;
    [SerializeField]
    protected string appearance;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float turnSpeed;

    [SerializeField]
    protected Food food;

    private MissionManager missionManager;

    public Sprite avatar;

    public AudioSource chomp;

    public GameObject cameraToLookAt;

    public GameObject prefab;

    protected GameObject marker;

    protected GameObject nametag;

    protected GameObject visionCone;

    public GameObject collectionButton;
    protected string collectionButtonName;

    public string getCollectionButtonName { get { return collectionButtonName; } }

    protected SkinnedMeshRenderer[] skinnedRenderers;
    protected SkinnedMeshRenderer[] skinnedAnimalRenderers;
    protected Renderer[] renderers;

    private Quaternion horizontalRotation;

    public bool hasFoodTarget = false;
    public bool lookingUp = false;

    protected bool marked = false;
    protected bool onScreen = false;

    protected float startingY;

    public string getFood { get { return food.ToString(); } }

    public float getSpeed { get { return speed; } }
    public float getTurnSpeed { get { return turnSpeed; } }
    public float getStartingY { get { return startingY; } }

    public virtual void Awake() {
        
    }

    protected void Start() {
        missionManager = GameObject.Find("/Menus/GameMenu/GMenu/MissionsStuff").GetComponent<MissionManager>();

        visionCone = transform.Find("VisionCone").gameObject;
        visionCone.GetComponentInChildren<Renderer>().enabled = false;

        skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        //Tierkörper + Meshes muss an erster Stelle im Prefab sein
        skinnedAnimalRenderers = transform.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();
        renderers = GetComponentsInChildren<Renderer>();
        
        marker = transform.Find("Marker").gameObject;
        marker.SetActive(false);

        nametag = transform.Find("Nametag").gameObject;
        nametag.GetComponentInChildren<TMP_Text>().text = commonName;
        nametag.SetActive(false);

        chomp = GetComponent<AudioSource>();

        cameraToLookAt = GameObject.Find("AR Session Origin/AR Camera");

        startingY = transform.position.y;

        horizontalRotation = Quaternion.Euler(0, 0, 0);

        collectionButton = GameObject.Find("/Menus/GameMenu/GMenu/CollectionStuff/Collection/Background/" + collectionButtonName);

        collectionButton.GetComponent<Button>().interactable = false;
    }

    //Prüft, ob sich Fisch im Sichtfeld des Spielers befindet und blendet ihn dementsprechend ressourcensparend ein/aus
    protected void Update() {
        foreach (SkinnedMeshRenderer r in skinnedAnimalRenderers) {
            if (r.IsVisibleFrom(Camera.main)) {
                onScreen = true;
            } else {
                onScreen = false;
            }   
        }
        
        if (onScreen) {
            ShowFish(true);
        } else {
            ShowFish(false);
        }
    }

    protected void FixedUpdate() {
        NametagFollowCam();

        //Sorgt dafür, dass der Fisch sich horizontal gerade richtet
        if (lookingUp) {
            Quaternion horizontalRotation = Quaternion.Euler(0, transform.rotation.y, 0);
            float angleDif = Quaternion.Dot(transform.rotation, horizontalRotation);

            //Prüft, ob der Fisch bereits nahezu perfekt ausgerichtet ist
            if (angleDif > 0.9999 || angleDif < -0.9999) {
                lookingUp = false;
                startingY = transform.position.y;
            } else {
                transform.rotation = Quaternion.Slerp(transform.rotation, horizontalRotation, Time.deltaTime * 2);
            }
        }
    }

    //Blendet Fisch + Komponenten ein/aus
    protected void ShowFish(bool visible) {
        foreach (SkinnedMeshRenderer r in skinnedRenderers) {
            r.enabled = visible;
        }
        
        foreach (Renderer r in renderers) {
            r.enabled = visible;
        }

        //Sichtfeld des Fischs wird jederzeit ausgeblendet, da es nie gesehen werden soll
        visionCone.GetComponentInChildren<MeshRenderer>().enabled = false;

        visionCone.SetActive(visible);

        nametag.GetComponent<Canvas>().enabled = visible;
    }

    //Funktion lässt Namensschild der Fische stets lesbar zur Kamera rotieren
    protected void NametagFollowCam() {
        Vector3 vector = cameraToLookAt.transform.position - nametag.transform.position;
        
        vector.x = 0.0f;
        vector.z = 0.0f;

        nametag.transform.LookAt(cameraToLookAt.transform.position - vector); 
        nametag.transform.Rotate(0,180,0);
    }

    //Formatiert Eintrag der Collection
    public string[] FormatCollectionEntry() {
        string[] collectionEntries = new string[2];

        collectionEntries[0] = String.Format("Name: {0}\nWissenschaftl. Name {1}\nTierart: {2}", commonName, scientificName, type.ToString());

        collectionEntries[1] = String.Format("Nahrung: {0}\nGröße: {1}\nGewicht: {2}\nLebensdauer: {3}\nAussehen: {4}\nGeschwindigkeit: {5}",
                                food.ToString().Substring(4), size, weight, lifeSpan, appearance, speed.ToString() + "km/h");

        return collectionEntries;
    }

    //Prüft, ob Fisch mit anderen GameObjects kollidiert
    protected void OnCollisionEnter(Collision collision) {
        try {
            //Prüft für jeden Essenstyp im Enum, ob das kollidierte Objekt eine Art Essen ist
            foreach (string food in Enum.GetNames(typeof(Food))) {
                //Wenn das Objekt eine Art Essen des Enums ist wird es zerstört
                if (collision.gameObject.tag == food) {
                    Destroy(collision.gameObject);

                    AudioSource.PlayClipAtPoint(chomp.clip, collision.transform.position);
                
                    //Wenn das Objekt das favorisierte Essen des Fischs ist wird der Fisch markiert
                    if (collision.gameObject.tag == getFood) {
                        
                        //Erhöht den Counter der markierten Tiere, sofern das Tier nicht bereits markiert war
                        if (!marked) {
                            GameSceneManager.animalsMarked += 1;
                            
                            missionManager.MissionSolved(commonName);
                        }

                        marked = true;
                        Debug.Log(marked);
                        
                        marker.SetActive(true);
                        nametag.SetActive(true);

                        collectionButton.GetComponent<Button>().interactable = true;

                        hasFoodTarget = false;

                        //Setzt Variable, damit der Fisch sich nach der Bewegung zum Essen wieder horizontal gerade richtet
                        lookingUp = true;
                    }
                }
            }
        } catch (NullReferenceException e) {
            Debug.Log(e);
        }
    }
}
