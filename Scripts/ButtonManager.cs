using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject mainPanel;

    [SerializeField] private AudioSource musicManager;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    /*
    #region Credits

    // Credits panelini açar, kapatýr.
    public void OpenCloseCreditsPanel()
    {
        if (creditsPanelOpened == false)
        {
            creditsPanel.SetActive(true);
            creditsPanelOpened = true;
        }
        else
        {
            creditsPanel.SetActive(false);
            creditsPanelOpened = false;
        }
    }

    #endregion

    #region My Other Games

    // Oyuncuyu Steam'deki geliþtirici sayfama yönlendirir.
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    #endregion

    #region Quit

    // Oyundan çýkar.
    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    #region Yes

    // Bütün ilerlemeyi siler ve 1. bölümden oyuna baþlar.
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level 1");
    }
    #endregion
    */
}