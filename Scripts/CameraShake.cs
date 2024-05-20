using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform; // Kameranýn transform bileþeni

    // Titreþim parametreleri
    public float shakeDuration = 0.2f; // Titreþim süresi
    public float shakeMagnitude = 0.1f; // Titreþim büyüklüðü

    private float elapsed = 0f; // Geçen süre
    private Vector3 originalPos; // Kameranýn orijinal konumu

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
            // Kamerayý titreþim efektiyle hareket ettir
            cameraTransform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;

            // Geçen süreyi güncelle
            elapsed += Time.deltaTime;
        }
        else
        {
            // Titreþim süresi dolduðunda kamerayý orijinal konumuna geri getir
            elapsed = 0f;
            cameraTransform.localPosition = originalPos;
        }
    }
}
