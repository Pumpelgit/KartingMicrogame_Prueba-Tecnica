using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuElementView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI display;

    public void SetText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        display.text = text;
    }
}
