using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactUs : MonoBehaviour
{

    public void SendEmail()
    {
        Application.OpenURL("mailto:k.andruhovich@gmail.com");
    }

    public void Politics()
    {
        Application.OpenURL("https://docs.google.com/document/d/1PIpnf8eHpbeZnC-EeBTrWPEvOLgZ9gNa5n7ASMnpO9k/edit#heading=h.75yj1osgcmas");
    }

    public void Terms()
    {
        Application.OpenURL("https://docs.google.com/document/d/1TUFIDLAfCvoJQgAO3N1QydTupKEd5FISJhr0rEePo6c/edit#heading=h.fmbv20p88e81");
    }
}
