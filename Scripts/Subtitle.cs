using UnityEngine;
using TMPro;

public class Subtitle : MonoBehaviour
{
    public Camera mainCamera;

    public TMP_Text txtSubtitle;

    private void Start()
    {
        txtSubtitle.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject.tag is "SubtitleObject" || hitInfo.collider.gameObject.tag is "Subtitle" && !PlayerPrefs.HasKey(hitInfo.collider.gameObject.name + "isSeen")) // Objeye bakýnca altyazý oynatýlacak mý?
            {
                if (hitInfo.collider.gameObject.GetComponentInChildren<ObjectCollider>().inCollider)
                {
                    txtSubtitle.gameObject.SetActive(true);
                    txtSubtitle.text = hitInfo.collider.gameObject.GetComponent<SubtitleCheck>().subtitleString;
                    txtSubtitle.GetComponent<Animation>().Play("SubtitleAnim");

                    hitInfo.collider.gameObject.GetComponent<SubtitleCheck>().isSeen = true;
                    PlayerPrefs.SetInt(hitInfo.collider.gameObject + "isSeen", 1);

                    Invoke(nameof(DeactivateTxtSubtitle), 7f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "SubtitleCollider" && !PlayerPrefs.HasKey(other.gameObject.name + "isSeen"))
        {
            txtSubtitle.gameObject.SetActive(true);
            txtSubtitle.text = other.gameObject.GetComponent<SubtitleCheck>().subtitleString;
            txtSubtitle.GetComponent<Animation>().Play("SubtitleAnim");

            other.gameObject.GetComponent<SubtitleCheck>().isSeen = true;
            PlayerPrefs.SetInt(other.gameObject.name + "isSeen", 1);

            Invoke(nameof(DeactivateTxtSubtitle), 7f);
        }
    }

    void DeactivateTxtSubtitle()
    {
        txtSubtitle.gameObject.SetActive(false);
    }
}