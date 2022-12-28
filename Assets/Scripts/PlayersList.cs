using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersList : MonoBehaviour
{
    [SerializeField] private GameObject[] _playersList;
    [SerializeField] private int _countPlayers;
    [SerializeField] private GameObject _addPlayersObject;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private GameObject _content;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _screenHeight;
    [SerializeField] private float _distance;


    public void AddPlayers()
    {
        _countPlayers++;
        ChangePosition();
    }

    private void Start()
    {
        _content.transform.position = new Vector3(_startPosition.transform.position.x, _startPosition.transform.position.y, _startPosition.transform.position.z);

        _screenHeight = Screen.height;

        if (_screenHeight >= 2960)
        {
            _distance = -5.697f;
        }

        if (_screenHeight >= 2732 && _screenHeight < 2960)
        {
            _distance = 0.55f;
        }

        if (_screenHeight >= 2560 && _screenHeight < 2732)
        {
            _distance = 0.7175f;
        }

        if (_screenHeight >= 2388 && _screenHeight < 2560)
        {
            _distance = 0.792f;
        }

        if (_screenHeight >= 2160 && _screenHeight < 2388)
        {
            _distance = 0.67f;
        }

        if (_screenHeight >= 1920 && _screenHeight < 2160)
        {
            _distance = 0.53f;
        }

    }

   public void ChangePosition()
    {
        if (_countPlayers ==1)
        {
            _playersList[0].SetActive(true);
        }

        if(_countPlayers == 2)
        {
            _playersList[1].SetActive(true);
        }

        if(_countPlayers == 3)
        {
            _playersList[2].SetActive(true);
        }

        if(_countPlayers == 4)
        {
            _playersList[3].SetActive(true);
            _scrollbar.value = _distance;
        }

        if(_countPlayers == 5)
        {
            _playersList[4].SetActive(true);
        }

        if(_countPlayers == 6)
        {
            _playersList[5].SetActive(true);
            _scrollbar.value = 0;
        }

        if(_countPlayers == 7)
        {
            _playersList[6].SetActive(true);
        }

        if(_countPlayers == 8)
        {
            _playersList[7].SetActive(true);
            _addPlayersObject.SetActive(false);
        }
    }
}
