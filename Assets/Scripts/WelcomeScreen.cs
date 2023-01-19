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
    [SerializeField] private PlayersList _playersList;
    [SerializeField] private StartPlayersScreen _startPlayersScreen;


    public void SetUser()
    {
        _name.text = _playersModel.GetLastUser().name;
        _name.transform.GetChild(0).GetComponent<TMP_Text>().text = _playersModel.GetLastUser().name;
        _titleName.text = _playersModel.GetLastUser().name;
        _titleName.transform.GetChild(0).GetComponent<TMP_Text>().text = _playersModel.GetLastUser().name;
        _avatar.sprite = _playersModel.avatars[_playersModel.GetLastUser().avatar];

        _playersList.AddPlayers();
        _playersList.ChangePosition();
        _startPlayersScreen.RefreshUsers();
    }
}
