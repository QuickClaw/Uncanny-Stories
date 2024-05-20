using UnityEngine;

public class CutCableScript : MonoBehaviour
{
    [SerializeField] private GameObject knifeIcon;
    [SerializeField] private GameObject[] monstersToSpawn;

    [SerializeField] private AudioSource monsterAS;
    [SerializeField] private AudioClip monsterSpawnSFX;

    void Start()
    {
        if (PlayerPrefs.HasKey("cableCutted"))
        {
            GetComponentInChildren<Collider>().enabled = false;
            GetComponent<LineRenderer>().enabled = false;
        }
    }

    public void CutCable()
    {
        GetComponentInChildren<Collider>().enabled = false;
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<AudioSource>().Play();

        knifeIcon.SetActive(false);

        for (int i = 0; i < monstersToSpawn.Length; i++)
            monstersToSpawn[i].SetActive(true);

        monsterAS.PlayOneShot(monsterSpawnSFX);       

        PlayerPrefs.SetInt("cableCutted", 1);
        PlayerPrefs.SetInt("used Knife", 1);
    }
}