using UnityEngine;
using TMPro;

public class LookAtYAxis : MonoBehaviour
{
    public Transform target; // Player transform

    public GameObject[] notePanelTexts; // Q ya basýnca çýkacak paneldeki yerel halkýn söyledikleri

    public GameObject[] NPCs; // Yerel halkýn GameObjectleri
    // 0 = Hamit
    // 1 = Utkucan
    // 2 = Alican
    // 3 = Behcet
    // 4 = Batu
    // 5 = Timur
    // 6 = Nese
    // 7 = Simay

    [SerializeField] private GameObject[] monsters; // Haritadaki canavarlar GameObjectleri

    [SerializeField] private GameObject bloodSplat1;

    [SerializeField] private AudioSource timurAS, playerAS;

    [SerializeField] private AudioClip writingSFX, screamSFX, crawlerRoarSFX, monsterKnowsSFX;

    [SerializeField] private TMP_Text txtSpokenFolks;
    [SerializeField] private TMP_Text txtOpenNotes;
    [SerializeField] private TMP_Text txtCrawlerSensed;

    public bool idle;
    public static bool metHamit, metUtkucan, metAlican, metBehcet, metBatu, metTimur, metNese, metSimay;
    private bool timurASPlayed;

    public static int spokenFolks;

    private void Start()
    {
        spokenFolks = PlayerPrefs.GetInt("spokenFolks"); // Yerel halktan kaç kiþiyle konuþtuðunun bilgisi
        txtSpokenFolks.text = "Spoken folks: " + "<color=yellow>" + spokenFolks + "/10</color>";

        if (PlayerPrefs.HasKey("metHamit")) // Hamit ile konuþtun mu
        {
            notePanelTexts[0].SetActive(true);
            NPCs[0].name = "Met Hamit";
        }
        if (PlayerPrefs.HasKey("metUtkucan")) // Utkucan ile konuþtun mu
        {
            notePanelTexts[1].SetActive(true);
            NPCs[1].name = "Met Utkucan";
        }
        if (PlayerPrefs.HasKey("metAlican")) // Alican ile konuþtun mu
        {
            notePanelTexts[2].SetActive(true);
            NPCs[2].name = "Met Alican";
        }
        if (PlayerPrefs.HasKey("metBehcet")) // Behcet ile konuþtun mu
        {
            notePanelTexts[3].SetActive(true);
            NPCs[3].name = "Met Behcet";
        }
        if (PlayerPrefs.HasKey("metBatu")) // Batu ile konuþtun mu
        {
            notePanelTexts[4].SetActive(true);
            NPCs[4].name = "Met Batu";
        }
        if (PlayerPrefs.HasKey("metTimur")) // Timur ile konuþtun mu
        {
            notePanelTexts[5].SetActive(true);
            NPCs[5].name = "Met Timur";
        }
        if (PlayerPrefs.HasKey("metNese")) // Nese ile konuþtun mu
        {
            notePanelTexts[6].SetActive(true);
            NPCs[6].name = "Met Nese";
        }
        if (PlayerPrefs.HasKey("metSimay")) // Simay ile konuþtun mu
        {
            notePanelTexts[7].SetActive(true);
            NPCs[7].name = "Met Simay";
        }

        if (PlayerPrefs.HasKey("timurDeath"))
        {
            Destroy(NPCs[5], 3f);
            bloodSplat1.SetActive(true);
        }

        txtOpenNotes.gameObject.SetActive(false);
        txtCrawlerSensed.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player")
        {
            if (idle) // Yerel halktan kimin yanýndaysan o playera doðru bakar
            {
                transform.LookAt(target);

                Vector3 euler = transform.rotation.eulerAngles;
                euler.x = 0;
                euler.z = 0;
                transform.rotation = Quaternion.Euler(euler);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) // Bütün PlayerPrefs kayýtlarýný siler -- HÝLE
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs silindi");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            if (!gameObject.name.Contains("Met")) // Karakterle konuþtuysan bir daha seninle konuþmaz
            {
                if (spokenFolks == 4) // Verilen deðer kadar kiþiyle konuþtuðunda canavar biraz güçlenir
                {
                    Invoke(nameof(PlayKnowsAudio), 5f);

                    for (int i = 0; i < monsters.Length; i++)
                    {
                        monsters[i].GetComponent<Patrol>().range += 10;
                        monsters[i].GetComponent<EnemyController>().walkSpeed += 1.5f;
                        monsters[i].GetComponent<EnemyController>().runSpeed += 2.2f;
                    }
                }

                spokenFolks++;
                PlayerPrefs.SetInt("spokenFolks", spokenFolks);

                GetComponent<AudioSource>().Play();
                playerAS.PlayOneShot(writingSFX, 0.4f);

                txtSpokenFolks.text = "Spoken folks: " + "<color=yellow>" + spokenFolks + "/10</color>";

                txtOpenNotes.gameObject.SetActive(true);
                txtOpenNotes.text = "You talked with <color=yellow>" + gameObject.name + "\n</color> [<color=orange> Q </color>] to open notes";

                txtOpenNotes.GetComponent<Animation>().Play("SubtitleAnim");

                Invoke(nameof(DeactivateTxtOpenNotes), 7f);
            }

            if (gameObject.name is "Hamit") // Yanýnda bulunduðun karakterin adý Hamit mi
            {
                notePanelTexts[0].SetActive(true);
                gameObject.name = "Met Hamit";

                metHamit = true;
                PlayerPrefs.SetInt("metHamit", 1);
            }
            if (gameObject.name is "Utkucan") // Yanýnda bulunduðun karakterin adý Utkucan mi
            {
                notePanelTexts[1].SetActive(true);
                gameObject.name = "Met Utkucan";

                metUtkucan = true;
                PlayerPrefs.SetInt("metUtkucan", 1);
            }
            if (gameObject.name is "Alican") // Yanýnda bulunduðun karakterin adý Alican mi
            {
                notePanelTexts[2].SetActive(true);
                gameObject.name = "Met Alican";

                metAlican = true;
                PlayerPrefs.SetInt("metAlican", 1);
            }
            if (gameObject.name is "Behcet") // Yanýnda bulunduðun karakterin adý Behcet mi
            {
                notePanelTexts[3].SetActive(true);
                gameObject.name = "Met Behcet";

                metBehcet = true;
                PlayerPrefs.SetInt("metBehcet", 1);
            }
            if (gameObject.name is "Batu") // Yanýnda bulunduðun karakterin adý Batu mi
            {
                notePanelTexts[4].SetActive(true);
                gameObject.name = "Met Batu";

                metBatu = true;
                PlayerPrefs.SetInt("metBatu", 1);
            }
            if (gameObject.name is "Timur") // Yanýnda bulunduðun karakterin adý Timur mi
            {
                notePanelTexts[5].SetActive(true);
                gameObject.name = "Met Timur";

                metTimur = true;
                PlayerPrefs.SetInt("metTimur", 1);
            }
            if (gameObject.name is "Nese") // Yanýnda bulunduðun karakterin adý Nese mi
            {
                notePanelTexts[6].SetActive(true);
                gameObject.name = "Met Nese";

                metNese = true;
                PlayerPrefs.SetInt("metNese", 1);
            }
            if (gameObject.name is "Simay") // Yanýnda bulunduðun karakterin adý Simay mi
            {
                notePanelTexts[7].SetActive(true);
                gameObject.name = "Met Simay";

                metSimay = true;
                PlayerPrefs.SetInt("metSimay", 1);
            }

            if (gameObject.name is "Met Batu" && metTimur) // Batunun yanýndaysan ve Timurla karþýlaþmýþsan
            {
                bloodSplat1.SetActive(true);
                Invoke(nameof(TimurDeath), 2f);
            }

            PlayerPrefs.Save();
        }
    }

    void TimurDeath() // Timur ölüm senaryosu
    {
        if (!timurASPlayed)
        {
            timurAS.volume = 1f;
            timurAS.minDistance = 90;
            timurAS.maxDistance = 110f;
            timurAS.PlayOneShot(screamSFX, 2f);

            timurASPlayed = true;
        }

        Destroy(NPCs[5], 3f);

        PlayerPrefs.SetInt("timurDeath", 1);
    }

    void DeactivateTxtOpenNotes()
    {
        txtOpenNotes.gameObject.SetActive(false);
    }

    void DeactivateTxtCrawlerSensed()
    {
        txtCrawlerSensed.gameObject.SetActive(false);
    }

    void PlayKnowsAudio()
    {
        txtCrawlerSensed.gameObject.SetActive(true);
        txtCrawlerSensed.GetComponent<Animation>().Play("SubtitleAnim");
        playerAS.PlayOneShot(monsterKnowsSFX, 2f);

        Invoke(nameof(DeactivateTxtCrawlerSensed), 7f);
    }
}