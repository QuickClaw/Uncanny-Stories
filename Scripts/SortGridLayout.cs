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
        // T�m child objelerin RectTransform'lerini al
        RectTransform[] children = new RectTransform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }

        // Aktif olanlar� en �ste, aktif olmayanlar� en alta yerle�tir
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