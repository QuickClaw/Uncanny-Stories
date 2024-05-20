using UnityEngine;

public class ItemPickupGrid : MonoBehaviour
{
    public GameObject item;
    public GameObject itemIcon;

    public GameObject player;

    private void Start()
    {
        if (PlayerPrefs.HasKey("found " + gameObject.name) && !PlayerPrefs.HasKey("used " + gameObject.name))
        {
            item.GetComponent<MeshRenderer>().enabled = false;
            item.GetComponent<Collider>().enabled = false;
            item.GetComponentInChildren<Collider>().enabled = false;

            itemIcon.SetActive(true);
        }

        if (PlayerPrefs.HasKey("found " + gameObject.name) && PlayerPrefs.HasKey("used" + gameObject.name))
        {
            item.GetComponent<MeshRenderer>().enabled = false;
            item.GetComponent<Collider>().enabled = false;
            item.GetComponentInChildren<Collider>().enabled = false;

            itemIcon.SetActive(false);
        }
    }

    public void PickupItem()
    {
        item.GetComponent<MeshRenderer>().enabled = false;
        item.GetComponent<Collider>().enabled = false;
        item.GetComponentInChildren<Collider>().enabled = false;

        itemIcon.SetActive(true);

        GetComponent<AudioSource>().Play();

        //SaveCharacterPosition();

        PlayerPrefs.SetInt("found " + gameObject.name, 1);
    }

    void SaveCharacterPosition()
    {
        Vector3 currentPosition = player.transform.position;

        PlayerPrefs.SetFloat("LastCharacterPosition" + "_x", currentPosition.x);
        PlayerPrefs.SetFloat("LastCharacterPosition" + "_y", currentPosition.y);
        PlayerPrefs.SetFloat("LastCharacterPosition" + "_z", currentPosition.z);
    }
}