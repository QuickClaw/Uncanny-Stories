using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject btnStart;

    void Start()
    {
        Invoke(nameof(ActivateBtnStart), 12f);
    }

    void ActivateBtnStart()
    {
        btnStart.SetActive(true);
    }
}