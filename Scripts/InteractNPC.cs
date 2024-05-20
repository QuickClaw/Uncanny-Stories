using UnityEngine;
using TMPro;

public class InteractNPC : MonoBehaviour
{
    public Transform player;

    [SerializeField] private GameObject notePanelImage;

    [SerializeField] private AudioSource playerAS;
    [SerializeField] private AudioClip NPCTalkSFX, writingSFX, itKnowsSFX;

    [SerializeField] private TMP_Text txtItKnows, txtSpokenFolks, txtOpenNotes;

    public bool idle;

    public static int spokenFolks;

    void Start()
    {
        if (PlayerPrefs.HasKey("met " + gameObject.name)) // Bu NPC ile konustun mu
            notePanelImage.SetActive(true);

        spokenFolks = PlayerPrefs.GetInt("spokenFolks");
        txtSpokenFolks.text = "Spoken folks: " + "<color=yellow>" + spokenFolks + "/10</color>";

        txtItKnows.gameObject.SetActive(false);
        txtOpenNotes.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) // Bütün PlayerPrefs kayýtlarýný siler -- HÝLE
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs silindi");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player" && idle)
        {
            transform.LookAt(player);

            Vector3 euler = transform.rotation.eulerAngles;
            euler.x = 0;
            euler.z = 0;
            transform.rotation = Quaternion.Euler(euler);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player" && !PlayerPrefs.HasKey("met " + gameObject.name))
        {
            PlayerPrefs.SetInt("met " + gameObject.name, 1);

            notePanelImage.SetActive(true);

            spokenFolks++;
            PlayerPrefs.SetInt("spokenFolks", spokenFolks);

            GetComponent<AudioSource>().PlayOneShot(NPCTalkSFX);
            playerAS.PlayOneShot(writingSFX, 0.4f);

            txtSpokenFolks.text = "Spoken folks: " + "<color=yellow>" + spokenFolks + "/10</color>";

            txtOpenNotes.gameObject.SetActive(true);
            txtOpenNotes.text = "You talked with <color=yellow>" + gameObject.name + "\n</color> [<color=orange> Q </color>] to open notes";

            txtOpenNotes.GetComponent<Animation>().Play("SubtitleAnim");

            Invoke(nameof(DeactivateTxtOpenNotes), 7f);
        }
    }

    void DeactivateTxtOpenNotes()
    {
        txtOpenNotes.gameObject.SetActive(false);
    }

    void PlayAudio_ItKnows()
    {
        txtItKnows.gameObject.SetActive(true);
        txtItKnows.GetComponent<Animation>().Play("SubtitleAnim");
        playerAS.PlayOneShot(itKnowsSFX, 2f);

        Invoke(nameof(DeactivateTxtItKnows), 7f);
    }

    void DeactivateTxtItKnows()
    {
        txtItKnows.gameObject.SetActive(false);
    }
}