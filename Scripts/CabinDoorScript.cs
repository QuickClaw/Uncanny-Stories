using UnityEngine;

public class CabinDoorScript : MonoBehaviour
{
    [SerializeField] private Animation cabinDoorAnimation;
    [SerializeField] private GameObject keyIcon;

    private void Start()
    {
        if (PlayerPrefs.HasKey("cabinDoorOpened"))
        {
            cabinDoorAnimation.Play();
            GetComponentInChildren<Collider>().enabled = false;
        }
    }

    public void OpenCabinDoor()
    {
        cabinDoorAnimation.Play();
        GetComponentInChildren<Collider>().enabled = false;
        GetComponent<AudioSource>().Play();

        keyIcon.SetActive(false);

        PlayerPrefs.SetInt("cabinDoorOpened", 1);
        PlayerPrefs.SetInt("used Key", 1);
    }
}