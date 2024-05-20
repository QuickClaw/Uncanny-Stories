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

    // Credits panelini a�ar, kapat�r.
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

    // Oyuncuyu Steam'deki geli�tirici sayfama y�nlendirir.
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    #endregion

    #region Quit

    // Oyundan ��kar.
    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    #region Yes

    // B�t�n ilerlemeyi siler ve 1. b�l�mden oyuna ba�lar.
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level 1");
    }
    #endregion
    */
}