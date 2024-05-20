using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource churchAS;
    public AudioClip audioClip;

    void Start()
    {
        if (churchAS != null && audioClip != null)
        {
            churchAS.clip = audioClip;
            InvokeRepeating(nameof(PlayClip), 0f, 120f); // 120 saniye = 2 dakika
        }
    }

    void PlayClip()
    {
        churchAS.Play();
    }
}