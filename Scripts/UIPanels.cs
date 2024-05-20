using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanels : MonoBehaviour
{
    public SortGridLayout SortGridLayoutScript;

    public GameObject localFolkPanel, pausePanel, startPanel;
    public GameObject player;

    [SerializeField] private AudioClip notePanelSFX, writingSFX;

    void Start()
    {
        localFolkPanel.SetActive(false);
        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        if (!PlayerPrefs.HasKey("gameStarted"))
        {
            startPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerLook>().enabled = false;
            player.GetComponent<PlayerFootsteps>().enabled = false;

            gameObject.GetComponent<AudioSource>().PlayOneShot(writingSFX);

            PlayerPrefs.SetInt("gameStarted", 1);
        }
        else
        {
            startPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerLook>().enabled = true;
            player.GetComponent<PlayerFootsteps>().enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !startPanel.activeInHierarchy)
            OpenLocalFolkPanel();

        if (Input.GetKeyDown(KeyCode.Escape) && !startPanel.activeInHierarchy)
            Pause();
    }

    void Pause()
    {
        if (!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            localFolkPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerLook>().enabled = false;
            player.GetComponent<PlayerFootsteps>().enabled = false;
            player.GetComponent<AudioSource>().Stop();

            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            localFolkPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerLook>().enabled = true;
            player.GetComponent<PlayerFootsteps>().enabled = true;

            Time.timeScale = 1f;
        }
    }

    void OpenLocalFolkPanel()
    {
        if (!pausePanel.activeInHierarchy)
        {
            GetComponent<AudioSource>().PlayOneShot(notePanelSFX);

            if (!localFolkPanel.activeInHierarchy)
            {
                localFolkPanel.SetActive(true);
                SortGridLayoutScript.SortGridElements();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                localFolkPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void Resume()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            localFolkPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerLook>().enabled = true;
            player.GetComponent<PlayerFootsteps>().enabled = true;

            Time.timeScale = 1f;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        startPanel.SetActive(false);

        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerLook>().enabled = true;
        player.GetComponent<PlayerFootsteps>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}