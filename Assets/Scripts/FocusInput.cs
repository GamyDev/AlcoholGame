using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FocusInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void OnEnable()
    {
        Invoke("Fokus", 0.2f);
    }


    void Fokus()
    {
        inputField.Select();
        inputField.ActivateInputField();
    }
}
