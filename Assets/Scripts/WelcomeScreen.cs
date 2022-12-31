using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField] private PlayersModel _playersModel;
    [SerializeField] private Image _avatar;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _titleName;


    public void SetUser()
    {
        _name.text = _playersModel.GetLastUser().name;
        _titleName.text = _playersModel.GetLastUser().name;
        _avatar.sprite = _playersModel.avatars[_playersModel.GetLastUser().avatar];
    }
}
