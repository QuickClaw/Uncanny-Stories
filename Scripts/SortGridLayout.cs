using UnityEngine;
using UnityEngine.UI;

public class SortGridLayout : MonoBehaviour
{
    private void Start()
    {
        SortGridElements();
    }

    public void SortGridElements()
    {
        // Tüm child objelerin RectTransform'lerini al
        RectTransform[] children = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }

        // Aktif olanlarý en üste, aktif olmayanlarý en alta yerleþtir
        int activeIndex = 0;
        int inactiveIndex = children.Length - 1;
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].gameObject.activeSelf)
            {
                children[i].SetSiblingIndex(activeIndex);
                activeIndex++;
            }
            else
            {
                children[i].SetSiblingIndex(inactiveIndex);
                inactiveIndex--;
            }
        }
    }
}