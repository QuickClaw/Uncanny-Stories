using UnityEngine;

public class ActivateObjects : MonoBehaviour
{
    public float activateDelay;

    void Start()
    {
        Invoke(nameof(ActivateGameobjectsScript), activateDelay);
    }

    void ActivateGameobjectsScript()
    {
        gameObject.GetComponent<TextAnimator>().enabled = true;
    }
}