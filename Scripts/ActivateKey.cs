using UnityEngine;

public class ActivateKey : MonoBehaviour
{
    [SerializeField] private GameObject key;

    private void Start()
    {
        if (PlayerPrefs.HasKey("keyActive"))
            key.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player" && PlayerPrefs.HasKey("met Buse"))
        {
            key.SetActive(true);

            PlayerPrefs.SetInt("keyActive", 1);
        }
    }
}