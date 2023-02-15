using DG.Tweening;
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
    public GameObject notification;

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

        if (playersModel.playerDatas.Count < 2)
        {
            notification.transform.localScale = Vector3.zero;
            notification.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
        }
        else
        {
            notification.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InElastic);
        }
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

        if(playersModel.playerDatas.Count < 2)
        {
            notification.transform.localScale = Vector3.zero;
            notification.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
        } else
        {
            notification.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InElastic);
        }
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
