using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScale : MonoBehaviour
{
    [SerializeField] private float aspectRatio;
    [SerializeField] private GameObject panelGame;
   // [SerializeField] private Vector2 scale;



    void Start()
    {
        aspectRatio = (float)Screen.height / Screen.width;
    

        if (aspectRatio <= 1.522849)
        {
            panelGame.transform.localScale = new Vector2(transform.localScale.x - 0.05f, transform.localScale.y - 0.05f);
        }

        else if (aspectRatio <= 1.43165)
        {
            panelGame.transform.localScale = new Vector2(transform.localScale.x - 0.07f, transform.localScale.y - 0.07f);
        }

        else if (aspectRatio <= 1.33333)
        {
            panelGame.transform.localScale = new Vector2(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f);
        }
    }


}
