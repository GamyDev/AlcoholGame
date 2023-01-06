using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAddScreen : MonoBehaviour
{
    [SerializeField] private PlayersModel _playersModel;
    [SerializeField] private TMP_InputField _name;


    public void AddUser()
    {
        _playersModel.AddNewPlayer(new Player()
        {
            name = _name.text
        });
        _name.text = "";
    }

    public void OnSelectUserField()
    {
        _name.transform.DOLocalMoveY(_name.transform.localPosition.y * -1, 1).SetEase(Ease.OutBounce);
    }

    public void OnDeselectUserField()
    {
        _name.transform.DOLocalMoveY(_name.transform.localPosition.y * -1, 1).SetEase(Ease.OutBounce);
    }
}
