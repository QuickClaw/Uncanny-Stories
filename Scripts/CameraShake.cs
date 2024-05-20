using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform; // Kameran�n transform bile�eni

    // Titre�im parametreleri
    public float shakeDuration = 0.2f; // Titre�im s�resi
    public float shakeMagnitude = 0.1f; // Titre�im b�y�kl���

    private float elapsed = 0f; // Ge�en s�re
    private Vector3 originalPos; // Kameran�n orijinal konumu

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent(typeof(Transform)) as Transform;
        }

        originalPos = cameraTransform.localPosition;
    }

    void Update()
    {
        if (elapsed < shakeDuration)
        {
            // Kameray� titre�im efektiyle hareket ettir
            cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;

            // Ge�en s�reyi g�ncelle
            elapsed += Time.deltaTime;
        }
        else
        {
            // Titre�im s�resi doldu�unda kameray� orijinal konumuna geri getir
            elapsed = 0f;
            cameraTransform.localPosition = originalPos;
        }
    }
}
