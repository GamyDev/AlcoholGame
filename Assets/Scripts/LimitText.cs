using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LimitText : MonoBehaviour
{
    public int maxLength;
     

    private void OnDestroy()
    {
        LocalizationManager.OnLanguageChange -= LocalizationManager_OnLanguageChange;
    }

    private void Start()
    {
        LocalizationManager.OnLanguageChange += LocalizationManager_OnLanguageChange;
        Invoke("Truncate", 0.1f);
    }

    private void LocalizationManager_OnLanguageChange()
    { 
        Invoke("Truncate", 0.1f);
    }

    public void Truncate()
    {
        if (string.IsNullOrEmpty(GetComponent<TMP_Text>().text)) GetComponent<TMP_Text>().text = GetComponent<TMP_Text>().text;
        GetComponent<TMP_Text>().text = GetComponent<TMP_Text>().text.Length <= maxLength ? GetComponent<TMP_Text>().text : GetComponent<TMP_Text>().text.Substring(0, maxLength) + "...";
    }
}
