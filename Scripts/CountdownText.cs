using UnityEngine;
using TMPro;

public class CountdownText : MonoBehaviour
{
    public TMP_Text countdownText;
    public int currentCountdownValue;

    void OnEnable()
    {
        currentCountdownValue = 7; // Reset the countdown value to 7 when the script is enabled
        UpdateCountdownText();
        InvokeRepeating("CountdownTick", 1f, 1f);
    }

    void CountdownTick()
    {
        currentCountdownValue--;
        UpdateCountdownText();
        if (currentCountdownValue <= 0)
        {
            CancelInvoke("CountdownTick");
        }
    }

    void UpdateCountdownText()
    {
        countdownText.text = "Respawn in " + currentCountdownValue.ToString();
    }
}
