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
    [SerializeField] private GameObject _buttonGrid;
    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        PlayersModel.OnPlayersAdd += OnPlayersAdd;
        PlayersModel.OnPlayersRemove += OnPlayersRemove;
    }

    private void OnPlayersRemove(List<Player> players)
    {
        _nextButton.interactable = players.Count > 1;
    }

    private void OnPlayersAdd(List<Player> players)
    {
        _playersList.AddPlayers();
        _nextButton.interactable = players.Count > 1;

    }

    public void RefreshUsers()
    {
        GameObject player = Instantiate(_playerPrefab);
        player.transform.SetParent(_buttonGrid.transform);
        player.transform.SetAsFirstSibling();
        player.transform.localScale = Vector3.one;
        player.transform.GetChild(0).GetComponent<TMP_Text>().text = PlayersModel.playersModel.GetLastUser().name;
    }
     
    private void OnDestroy()
    {
        PlayersModel.OnPlayersAdd -= OnPlayersAdd;
        PlayersModel.OnPlayersRemove -= OnPlayersRemove;
    }
}


