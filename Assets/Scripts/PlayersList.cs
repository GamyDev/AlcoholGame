using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersList : MonoBehaviour
{
    [SerializeField] private List<GameObject> _playersList; 
    [SerializeField] private GameObject _addPlayersObject;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _moveObject;
    [SerializeField] private GameObject _topTextObject;
    [SerializeField] private GameObject _topBlueObject;
    [SerializeField] private Transform[] _positionContent;
    [SerializeField] private Transform[] _positionMoveObject;
    [SerializeField] private float _screenHeight;
    [SerializeField] private float _distanceScroll;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject player;
    public GamePlayerList playerList;

    public void RemovePlayer(int index)
    {
        for (int i = 0; i < _content.transform.childCount; i++)
        {
            if (i == index)
                Destroy(_content.transform.GetChild(i).gameObject);
        }
        _playersList.RemoveAt(index);
    }

    private void Start()
    {
        _playersList = new List<GameObject>();

        _content.transform.position = new Vector3(_positionContent[0].transform.position.x, _positionContent[0].transform.position.y, _positionContent[0].transform.position.z);

        _screenHeight = Screen.height;

        if (_screenHeight >= 2532 && _screenHeight < 2960)
        {
            _distanceScroll = 0.9126536f;
        }

        if (_screenHeight >= 2436 && _screenHeight < 2532)
        {
            _distanceScroll = 0.9027101f;
        }

        if (_screenHeight >= 2340 && _screenHeight < 2436)
        {
            _distanceScroll = 0.9086636f; //
        }

        if (_screenHeight >= 2160 && _screenHeight < 2340)
        {
            _distanceScroll = 0.5328956f; //
        }

        if (_screenHeight >= 1920 && _screenHeight < 2160)
        {
            _distanceScroll = 0.6948383f; //
        }

        if (_screenHeight >= 1792 && _screenHeight < 1920)
        {
            _distanceScroll = 0.8960785f; //
        }

        if (_screenHeight >= 1334 && _screenHeight < 1792)
        {
            _distanceScroll = 0.6730404f; //
        }

        if (_screenHeight >= 1136 && _screenHeight < 1334)
        {
            _distanceScroll = 0.6748586f; //
        }

    }


    public void ChangePosition(bool spawn = true)
    {
        _addPlayersObject.SetActive(true);
        GameObject playerPrefab = null;
        if (spawn)
        {
             playerPrefab = Instantiate(player);
            _playersList.Add(playerPrefab);
            playerPrefab.transform.SetParent(_content.transform);
            playerPrefab.transform.SetSiblingIndex(_content.transform.childCount - 2);
        }
        

        if (_playersList.Count == 1)
        {
            if(spawn)
                playerPrefab.name = "Player1";
            
        }

        if(_playersList.Count == 2)
        {
            if (spawn)
                playerPrefab.name = "Player2";

            _content.transform.position = new Vector3(_positionContent[1].transform.position.x, _positionContent[1].transform.position.y, _positionContent[1].transform.position.z);
            _moveObject.transform.position = new Vector3(_positionMoveObject[0].transform.position.x, _positionMoveObject[0].transform.position.y, _positionMoveObject[0].transform.position.z);
        }

        if(_playersList.Count == 3)
        {
            if (spawn)
                playerPrefab.name = "Player3";
        }

        if(_playersList.Count == 4)
        {
            if (spawn)
                playerPrefab.name = "Player4";

            _content.transform.position = new Vector3(_positionContent[2].transform.position.x, _positionContent[2].transform.position.y, _positionContent[2].transform.position.z);
            _moveObject.transform.position = new Vector3(_positionMoveObject[1].transform.position.x, _positionMoveObject[1].transform.position.y, _positionMoveObject[1].transform.position.z);

            _topTextObject.SetActive(false);
            _topBlueObject.SetActive(true);
            _scrollRect.enabled = true;
            _scrollbar.value = _distanceScroll;
        }

        if(_playersList.Count == 5)
        {
            if (spawn)
                playerPrefab.name = "Player5";
        }

        if(_playersList.Count == 6)
        {
            if (spawn)
                playerPrefab.name = "Player6";

            _scrollbar.value = 0;
        }

        if(_playersList.Count == 7)
        {
            if (spawn)
                playerPrefab.name = "Player7";
        }

        if(_playersList.Count == 8)
        {
            if (spawn)
                playerPrefab.name = "Player8";

            _addPlayersObject.SetActive(false);
        }
    }
}
