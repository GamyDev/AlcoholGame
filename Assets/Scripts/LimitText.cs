using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LimitText : MonoBehaviour
{
    public int maxLength;

    private void Start()
    {
        Truncate();
        LocalizationManager.OnLanguageChange += LocalizationManager_OnLanguageChange;
    }

    private void LocalizationManager_OnLanguageChange()
    {
        Truncate();
    }

    public void Truncate()
    {
        if (string.IsNullOrEmpty(GetComponent<TMP_Text>().text)) GetComponent<TMP_Text>().text = GetComponent<TMP_Text>().text;
        GetComponent<TMP_Text>().text = GetComponent<TMP_Text>().text.Length <= maxLength ? GetComponent<TMP_Text>().text : GetComponent<TMP_Text>().text.Substring(0, maxLength) + "...";
    }
}
