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
    [SerializeField] private Animator animator;

    private float timeSlide = 0.7f;
    private bool startSlide;

    public string RuWelcome;
    public string EnWelcome;

    

    public void SetUser()
    {
        if (LocalizationManager.SelectedLanguage == 0)
        {
            _titleName.text = $"{EnWelcome}  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
            _subTitleName.GetComponent<TMP_Text>().text = $"{EnWelcome}  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
        }
        if (LocalizationManager.SelectedLanguage == 1)
        {
            _titleName.text = $"{RuWelcome}  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
            _subTitleName.GetComponent<TMP_Text>().text = $"{RuWelcome}  <color=#FFE973>{_playersModel.GetLastUser().name}</color>";
        }

        _name.text = _playersModel.GetLastUser().name;
        _subName.GetComponent<TMP_Text>().text = _playersModel.GetLastUser().name; 


        startSlide = true; 
         
    }

    public void ActivateUser()
    {
        _playersList.ChangePosition();
        _playersList.playerList.AddPlayer();
        _startPlayersScreen.RefreshUsers();
    }

    private void LateUpdate()
    {
        if (timeSlide > 0 && startSlide)
        {
            timeSlide -= Time.deltaTime;
        }
        else
        {
            if (timeSlide <= 0)
            {
                SetAvatar();
            }
        }

    }

    private void SetAvatar()
    { 
        _avatar.sprite = _playersModel.avatars[_playersModel.GetLastUser().avatar];
    }
    public void EnableAvatar()
    {
        timeSlide = 0.7f;
        startSlide = false; 
        animator.Rebind();
        animator.Update(0); 
    }
}
