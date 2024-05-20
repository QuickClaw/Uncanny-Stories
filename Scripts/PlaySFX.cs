using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player" && !PlayerPrefs.HasKey("audioClipPlayed" + gameObject.name))
        {
            audioSource.PlayOneShot(audioClip, 1.5f);

            PlayerPrefs.SetInt("audioClipPlayed" + gameObject.name, 1);
        }
    }
}