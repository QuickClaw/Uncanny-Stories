using UnityEngine;

public class CageDoorScript : MonoBehaviour
{
    public BearScript BearScript;

    [SerializeField] private Animation cageDoorAnimation;
    [SerializeField] private GameObject crowbarIcon;

    private void Start()
    {
        if (PlayerPrefs.HasKey("cageDoorOpened"))
        {
            BearScript.isSleeping = false;

            cageDoorAnimation.Play();
            GetComponentInChildren<Collider>().enabled = false;
        }
        else
        {
            BearScript.isSleeping = true;
            GetComponentInChildren<Collider>().enabled = true;
        }
    }

    public void OpenCageDoor()
    {
        cageDoorAnimation.Play();
        GetComponentInChildren<Collider>().enabled = false;
        GetComponent<AudioSource>().Play();

        crowbarIcon.SetActive(false);

        BearScript.isSleeping = false;

        PlayerPrefs.SetInt("cageDoorOpened", 1);
        PlayerPrefs.SetInt("used Crowbar", 1);
    }
}