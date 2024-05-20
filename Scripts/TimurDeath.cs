using UnityEngine;

public class TimurDeath : MonoBehaviour
{
    [SerializeField] private GameObject timur;
    [SerializeField] private GameObject blood;

    [SerializeField] private AudioSource timurAS;
    [SerializeField] private AudioClip timurScream;

    void Start()
    {
        if (PlayerPrefs.HasKey("timur dead"))
        {
            blood.SetActive(true);

            Destroy(timur);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player" && PlayerPrefs.HasKey("met Timur"))
        {
            Invoke(nameof(TimurDying), 3.5f);
        }
    }

    void TimurDying()
    {
        blood.SetActive(true);
        timurAS.PlayOneShot(timurScream);

        Destroy(timur);

        PlayerPrefs.SetInt("timur dead", 1);
    }
}