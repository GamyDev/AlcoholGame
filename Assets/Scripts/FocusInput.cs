using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FocusInput : MonoBehaviour
{
    private void OnEnable()
    { 
        GetComponent<TMP_InputField>().Select();
        GetComponent<TMP_InputField>().ActivateInputField();
    }
}
