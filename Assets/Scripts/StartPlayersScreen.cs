using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPlayersScreen : MonoBehaviour
{
    [SerializeField] private PlayersList _playersList;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _disabledNextButton;
    [SerializeField] private GameObject _buttonGrid;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private PlayersModel playersModel;
    [SerializeField] private GamePlayerList _gamePlayerList;
    

    public GameObject PlayerPrefab { get => _playerPrefab; }

    private void Start()
    {
        PlayersModel.OnPlayersAdd += OnPlayersAdd;
        PlayersModel.OnPlayersRemove += OnPlayersRemove;
        RemoveUserButtonHandler.UserRemove += OnUserRemove;
    }

    private void OnUserRemove(int index)
    {
         
        playersModel.playerDatas.RemoveAt(index);
        _gamePlayerList.RemovePlayer(index);
        _playersList.RemovePlayer(index);
        _playersList.ChangePosition(false);
        _nextButton.gameObject.SetActive(playersModel.playerDatas.Count > 1);
        _disabledNextButton.gameObject.SetActive(playersModel.playerDatas.Count < 2);
       
    }

    private void OnPlayersRemove(List<Player> players)
    {
        Debug.Log("Remove Player");
        _nextButton.gameObject.SetActive(players.Count > 1);
        _disabledNextButton.gameObject.SetActive(players.Count < 2);
    }

    private void OnPlayersAdd(List<Player> players)
    {
        _nextButton.gameObject.SetActive(players.Count > 1);
        _disabledNextButton.gameObject.SetActive(players.Count < 2);

    }

    public void RefreshUsers()
    {    
        GetLastVisibleObject().transform.GetChild(0).GetComponent<TMP_Text>().text = playersModel.GetLastUser().name;
        GetLastVisibleObject().transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = playersModel.GetLastUser().name;
        GetLastVisibleObject().GetComponent<Image>().sprite = playersModel.avatars[PlayersModel.playersModel.GetLastUser().avatar];
    }

    public void RefreshVisibles()
    {
        for (int i = 0; i < _buttonGrid.transform.childCount; i++)
        {
            if(!_buttonGrid.transform.GetChild(i).gameObject.activeSelf)
            {
                _buttonGrid.transform.GetChild(i).transform.SetAsLastSibling();
                break;
            }
        }
    }

    public GameObject GetLastVisibleObject()
    {
        GameObject lastVisible = null; 

        for (int i = 0; i < _buttonGrid.transform.childCount - 1; i++)
        {
            if(_buttonGrid.transform.GetChild(i).gameObject.activeSelf)
            {
                lastVisible = _buttonGrid.transform.GetChild(i).gameObject;
            }
        }

        return lastVisible;
    }
     
    private void OnDestroy()
    {
        PlayersModel.OnPlayersAdd -= OnPlayersAdd;
        PlayersModel.OnPlayersRemove -= OnPlayersRemove;
        RemoveUserButtonHandler.UserRemove -= OnUserRemove;
    }
}


