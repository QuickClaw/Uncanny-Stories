using UnityEngine;
using TMPro;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
    public TMP_Text textObject;
    [TextArea(25, 25)]
    public string targetText;
    public float letterDelay = 0.2f;

    private void Start()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        for (int i = 0; i < targetText.Length; i++)
        {
            textObject.text = targetText.Substring(0, i + 1);

            yield return new WaitForSeconds(letterDelay);
        }
    }
}