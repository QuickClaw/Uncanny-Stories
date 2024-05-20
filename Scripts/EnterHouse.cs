using System.Collections;
using UnityEngine;

public class EnterHouse : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public static bool inHouse;

    public PlayerFootsteps PlayerFootsteps;

    [SerializeField] private AudioSource environmentAS;

    public AudioClip[] indoorFootstepSounds;
    public AudioClip[] outdoorFootstepSounds;

    //private void Awake()
    //{
    //    if (PlayerPrefs.HasKey("LastCharacterPosition" + "_x"))
    //        player.transform.position = new Vector3(PlayerPrefs.GetFloat("LastCharacterPosition" + "_x"), PlayerPrefs.GetFloat("LastCharacterPosition" + "_y"), PlayerPrefs.GetFloat("LastCharacterPosition" + "_z"));
    //}

    private void Start()
    {
        if (PlayerPrefs.HasKey("LastCharacterPosition" + "_x"))
            player.transform.position = new Vector3(PlayerPrefs.GetFloat("LastCharacterPosition" + "_x"), PlayerPrefs.GetFloat("LastCharacterPosition" + "_y"), PlayerPrefs.GetFloat("LastCharacterPosition" + "_z"));

        if (inHouse)
            PlayerFootsteps.footstepSounds = indoorFootstepSounds;
        else
            PlayerFootsteps.footstepSounds = outdoorFootstepSounds;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            SaveCharacterPosition();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player")
        {
            inHouse = true;
            PlayerFootsteps.footstepSounds = indoorFootstepSounds;
            environmentAS.volume = Mathf.Lerp(environmentAS.volume, 0.075f, Time.deltaTime * 5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "Player")
        {
            inHouse = false;
            PlayerFootsteps.footstepSounds = outdoorFootstepSounds;
            StartCoroutine(FadeOutOutsideAudio());
        }
    }

    IEnumerator FadeOutOutsideAudio()
    {
        float elapsedTime = 0f;
        float startVolume = environmentAS.volume;

        while (elapsedTime < 0.5f)
        {
            environmentAS.volume = Mathf.Lerp(startVolume, 0.4f, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void SaveCharacterPosition()
    {
        Vector3 currentPosition = player.transform.position;

        PlayerPrefs.SetFloat("LastCharacterPosition" + "_x", currentPosition.x);
        PlayerPrefs.SetFloat("LastCharacterPosition" + "_y", currentPosition.y);
        PlayerPrefs.SetFloat("LastCharacterPosition" + "_z", currentPosition.z);
    }
}