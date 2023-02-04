using System;
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
    [SerializeField] private TMP_Text _subName;
    [SerializeField] private TMP_Text _titleName;
    [SerializeField] private TMP_Text _subTitleName;
    [SerializeField] private PlayersList _playersList;
    [SerializeField] private StartPlayersScreen _startPlayersScreen;

    private void OnEnable()
    {
        LocalizationManager.OnLanguageChange += ChangeLanguage;
    }

    private void ChangeLanguage()
    {
        if(LocalizationManager.SelectedLanguage == 0)
        {
            _titleName.text = $"Welcome,  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
            _subTitleName.GetComponent<TMP_Text>().text = $"Welcome,  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
        }
        if(LocalizationManager.SelectedLanguage == 1)
        {
            _titleName.text = $"Добро пожаловать,  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
            _subTitleName.GetComponent<TMP_Text>().text = $"Добро пожаловать,  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
        }
    }

    private void OnDisable()
    {
        LocalizationManager.OnLanguageChange -= ChangeLanguage;
    }

    public void SetUser()
    {
        _name.text = _playersModel.GetLastUser().name;
        _subName.GetComponent<TMP_Text>().text = _playersModel.GetLastUser().name;
        _titleName.text = $"Welcome,  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
        _subTitleName.GetComponent<TMP_Text>().text = $"Welcome,  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
        _avatar.sprite = _playersModel.avatars[_playersModel.GetLastUser().avatar];

        _playersList.AddPlayers();
        _playersList.ChangePosition();
        _startPlayersScreen.RefreshUsers();
    }
}
