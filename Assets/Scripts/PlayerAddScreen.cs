using DanielLochner.Assets.SimpleScrollSnap;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAddScreen : MonoBehaviour
{
    [SerializeField] private PlayersModel _playersModel;
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private GameObject checkOn;
    [SerializeField] private GameObject checkOff;
    [SerializeField] private GameObject _inputObject;
    [SerializeField] private GameObject _welcomObject;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private WelcomeScreen _welcomeScreen;
    [SerializeField] private PlayersList _playersList;
    [SerializeField] private GameObject _backInput;
    [SerializeField] private GameObject _back2Input;


    public void CheckInputField(string value)
    {
        checkOn.SetActive(!string.IsNullOrEmpty(value) && !ExistName(value));
        checkOff.SetActive(string.IsNullOrEmpty(value) || ExistName(value));
    }

    public bool ExistName(string name)
    {
        foreach (var item in _playersModel.playerDatas)
        {
            if(name == item.name)
            {
                return true;
            }
        }

        return false;
    }

    public void AddUser()
    {
        _playersModel.AddNewPlayer(new Player()
        {
            name = _name.text
        });
        _name.text = "";
    }

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(!string.IsNullOrEmpty(_inputObject.GetComponent<TMP_InputField>().text)) { 
                 _inputObject.SetActive(false);
                _welcomObject.SetActive(true);
                AddUser();
                _welcomeScreen.SetUser(); 
                _audioSource.Play();
                _backInput.SetActive(false);
                _back2Input.SetActive(false);
            }
        }
    }
}
