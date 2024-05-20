using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLifes : MonoBehaviour
{
    public GameObject gameOverPanel;

    public int life;

    public TMP_Text txtLife;

    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level 1");
    }
}