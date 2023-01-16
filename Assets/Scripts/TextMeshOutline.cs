using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshOutline : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Color32 color;

    void Start()
    {
        textMeshPro.outlineWidth = 0.2f;
        textMeshPro.outlineColor = color;
    }
}
