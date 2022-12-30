using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAddScreen : MonoBehaviour
{
    [SerializeField] private PlayersModel _playersModel;
    [SerializeField] private TMP_InputField _name;
    
    public void AddUser()
    {
        _playersModel.AddNewPlayer(new Player()
        {
            name = _name.text
        });
        _name.text = "";
    }
}
