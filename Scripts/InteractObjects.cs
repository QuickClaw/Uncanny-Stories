using UnityEngine;
using TMPro;

public class InteractObjects : MonoBehaviour
{
    private Outline Outline = null;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animation crosshairAnimation;
    [SerializeField] private TMP_Text txtInteract, txtNeedAnItem;
    [SerializeField] private GameObject objectHolder;
    [SerializeField] private GameObject bottomOfPlayer;
    [SerializeField] private GameObject flashlightHand, flashlightGround;
    [SerializeField] private GameObject keyIconInteract, knifeIconInteract, crowbarIconInteract;
    public static GameObject objectInHand;

    [SerializeField] private AudioClip flashlightPickupSFX;
    [SerializeField] private AudioClip keyPickupSFX;
    [SerializeField] private AudioClip doorDontOpenSFX;

    public static bool mouseOnObject;
    private bool isObjectPickable;

    public static string stringInteract;

    public static bool flashlightFound;

    void Start()
    {
        if (PlayerPrefs.HasKey("flashlightFound"))
            PickupFlashlight();

        mouseOnObject = false;
        txtInteract.gameObject.SetActive(false);
        txtNeedAnItem.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject.GetComponentInChildren<ObjectCollider>().inCollider)
            {
                Outline = hitInfo.collider.gameObject.GetComponent<Outline>();

                if (hitInfo.collider.gameObject.tag is "Pickable") // Obje yerden alýnabilir bir obje mi?
                {
                    isObjectPickable = true;
                    stringInteract = "pickup the";
                }

                if (hitInfo.collider.gameObject.tag is "Light") // Obje ýþýk kaynaðý mý?
                {
                    isObjectPickable = false;

                    if (hitInfo.collider.gameObject.GetComponent<OpenLamp>().isLightOn)
                        stringInteract = "close the";
                    else
                        stringInteract = "open the";
                }

                if (hitInfo.collider.gameObject.name.Contains("Door")) // Obje kapý mý?
                {
                    isObjectPickable = false;
                    stringInteract = "open the";

                    if (hitInfo.collider.gameObject.name is "Cabin Door")
                        keyIconInteract.SetActive(true);
                    if (hitInfo.collider.gameObject.name is "Cage Door")
                        crowbarIconInteract.SetActive(true);
                }

                if (hitInfo.collider.gameObject.name is ("Cable")) // Obje kablo mu?
                {
                    isObjectPickable = false;
                    stringInteract = "cut the";
                    knifeIconInteract.SetActive(true);
                }

                if (hitInfo.collider.gameObject.name != null && hitInfo.collider.gameObject.tag != "Subtitle" && hitInfo.collider.gameObject.tag != "SubtitleObject")
                {
                    if (Outline != null)
                        Outline.enabled = true;

                    mouseOnObject = true;
                    crosshairAnimation.Play("Crosshair_Interact");
                    txtInteract.gameObject.SetActive(true);
                    txtInteract.text = "<size=55>[ E ]</size> to " + stringInteract + " " + "<color=green>" + hitInfo.collider.gameObject.name + "</color>";
                }

                if (Input.GetKeyDown(KeyCode.E)) // Mouse objedeyken E tuþuna basýlýrsa koþullara göre çalýþacak methodlar
                {
                    if (isObjectPickable && hitInfo.collider.gameObject.name is "Flashlight")
                        PickupFlashlight();

                    if (hitInfo.collider.gameObject.GetComponent<ItemPickupGrid>() != null)
                        hitInfo.collider.gameObject.GetComponent<ItemPickupGrid>().PickupItem();

                    if (hitInfo.collider.gameObject.name is "Lamp")
                        hitInfo.collider.gameObject.GetComponent<OpenLamp>().OpenCloseLamp();

                    if (hitInfo.collider.gameObject.name is "Cabin Door" && PlayerPrefs.HasKey("found Key"))
                        hitInfo.collider.gameObject.GetComponent<CabinDoorScript>().OpenCabinDoor();
                    if (hitInfo.collider.gameObject.name is "Cabin Door" && !PlayerPrefs.HasKey("found Key"))
                    {
                        txtNeedAnItem.gameObject.SetActive(true);
                        txtNeedAnItem.gameObject.GetComponent<Animation>().Play();
                        txtNeedAnItem.text = "I need a <color=yellow>Key</color>";
                        GetComponent<AudioSource>().PlayOneShot(doorDontOpenSFX);
                    }

                    if (hitInfo.collider.gameObject.name is "Cage Door" && PlayerPrefs.HasKey("found Crowbar"))
                        hitInfo.collider.gameObject.GetComponent<CageDoorScript>().OpenCageDoor();
                    if (hitInfo.collider.gameObject.name is "Cage Door" && !PlayerPrefs.HasKey("found Crowbar"))
                    {
                        txtNeedAnItem.gameObject.SetActive(true);
                        txtNeedAnItem.gameObject.GetComponent<Animation>().Play();
                        txtNeedAnItem.text = "I need a <color=yellow>Crowbar</color>";
                        GetComponent<AudioSource>().PlayOneShot(doorDontOpenSFX);
                    }

                    if (hitInfo.collider.gameObject.name is "Cable" && PlayerPrefs.HasKey("found Knife"))
                        hitInfo.collider.gameObject.GetComponent<CutCableScript>().CutCable();
                    if (hitInfo.collider.gameObject.name is "Cable" && !PlayerPrefs.HasKey("found Knife"))
                    {
                        txtNeedAnItem.gameObject.SetActive(true);
                        txtNeedAnItem.gameObject.GetComponent<Animation>().Play();
                        txtNeedAnItem.text = "I need a <color=yellow>Knife</color>";
                    }

                    if (hitInfo.collider.gameObject.name is "Door")
                        hitInfo.collider.gameObject.GetComponent<DoorScript.Door>().OpenDoor();
                }
            }
            else
            {
                if (Outline != null)
                    Outline.enabled = false;

                mouseOnObject = false;
                crosshairAnimation.Play("Crosshair_Idle");
                txtInteract.gameObject.SetActive(false);
                keyIconInteract.SetActive(false);
                knifeIconInteract.SetActive(false);
                crowbarIconInteract.SetActive(false);
            }
        }
        else
        {
            if (Outline != null)
                Outline.enabled = false;

            mouseOnObject = false;
            crosshairAnimation.Play("Crosshair_Idle");
            txtInteract.gameObject.SetActive(false);
            keyIconInteract.SetActive(false);
            knifeIconInteract.SetActive(false);
            crowbarIconInteract.SetActive(false);
        }
    }

    private void PickupFlashlight()
    {
        flashlightHand.SetActive(true);
        flashlightGround.SetActive(false);

        flashlightFound = true;

        GetComponent<AudioSource>().PlayOneShot(flashlightPickupSFX, 2f);

        PlayerPrefs.SetInt("flashlightFound", 1);
        PlayerPrefs.Save();
    }

    private void DeactivateNeedAnItem()
    {
        txtNeedAnItem.gameObject.SetActive(false);
    }
}