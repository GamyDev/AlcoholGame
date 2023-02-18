using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactUs : MonoBehaviour
{

    public void SendEmail()
    {
        Application.OpenURL("mailto:k.andruhovich@gmail.com");
    }
}
