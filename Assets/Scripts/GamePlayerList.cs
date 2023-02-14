using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayerList : MonoBehaviour
{
    public GameObject content;
    public GameObject playerPrefab;

    public PlayersModel playersModel;

    public GameObject AddPlayerButton;

    public static event Action AddPlayerEvent;
    [SerializeField] private GameObject _backButton;



    public void AddPlayer()
    {
        GameObject player = Instantiate(playerPrefab);
        player.transform.SetParent(content.transform);
        player.transform.SetSiblingIndex(content.transform.childCount - 2);
        player.GetComponent<RectTransform>().localScale = Vector3.one;
        RefreshUsers();

        if (content.transform.childCount - 1 == 8)
            AddPlayerButton.SetActive(false);

        AddPlayerEvent?.Invoke();

        _backButton.SetActive(playersModel.playerDatas.Count > 1);
    }

    public void RefreshUsers()
    {
        GetLastVisibleObject().transform.GetChild(0).GetComponent<TMP_Text>().text = playersModel.GetLastUser().name;
        GetLastVisibleObject().transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = playersModel.GetLastUser().name;
        GetLastVisibleObject().GetComponent<Image>().sprite = playersModel.avatars[PlayersModel.playersModel.GetLastUser().avatar];
    }

    public void RemovePlayer(int index)
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            if(i == index)
                Destroy(content.transform.GetChild(i).gameObject);
        }
         
        AddPlayerButton.SetActive(true);
        _backButton.SetActive(playersModel.playerDatas.Count > 1);
    }

    public GameObject GetLastVisibleObject()
    {
        GameObject lastVisible = null;

        for (int i = 0; i < content.transform.childCount - 1; i++)
        {
            if (content.transform.GetChild(i).gameObject.activeSelf)
            {
                lastVisible = content.transform.GetChild(i).gameObject;
            }
        }

        return lastVisible;
    }
}
