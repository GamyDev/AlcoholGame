using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersList : MonoBehaviour
{
    [SerializeField] private GameObject[] _playersList;
    [SerializeField] private int _countPlayers;
    [SerializeField] private GameObject _addPlayersObject;


    public void AddPlayers()
    {
        _countPlayers++;
    }

     
    

    void Update()
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
        }

        if(_countPlayers == 5)
        {
            _playersList[4].SetActive(true);
        }

        if(_countPlayers == 6)
        {
            _playersList[5].SetActive(true);
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
