using UnityEngine;

public class OpenLamp : MonoBehaviour
{
    public Light pointLight;
    public bool isLightOn = false;

    private void Start()
    {
        pointLight.enabled = isLightOn;
    }

    public void OpenCloseLamp()
    {
        GetComponent<AudioSource>().Play();

        isLightOn = !isLightOn;

        if (isLightOn)
            pointLight.enabled = true;
        else
            pointLight.enabled = false;
    }
}