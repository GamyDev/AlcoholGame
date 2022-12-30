using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayersModel : MonoBehaviour
{
    public List<Player> _playerDatas;

    public static Action<List<Player>> OnPlayersAdd;
    public static Action<List<Player>> OnPlayersRemove;

    public static PlayersModel playersModel;

    private void Awake()
    {
        playersModel = this;
        _playerDatas = new List<Player>();
    }

    public void AddNewPlayer(Player player)
    {
        if (player == null)
            throw new System.ArgumentNullException();

        _playerDatas.Add(player);

        OnPlayersAdd?.Invoke(_playerDatas); 
    }

    public Player GetLastUser()
    {
        if(_playerDatas.Count > 0)
            return _playerDatas[_playerDatas.Count - 1];

        return null;
    }

    public void RemoveUser(int index)
    {
        _playerDatas.RemoveAt(_playerDatas.Count - 1 - index);
        OnPlayersRemove?.Invoke(_playerDatas);
    }
}


[System.Serializable]
public class Player
{
    public string name;
}