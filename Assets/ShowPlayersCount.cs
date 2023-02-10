using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPlayersCount : MonoBehaviour
{
    public PlayersModel model;
    public TMP_Text text;
    public TMP_Text outline;

    private void OnEnable()
    {
        text.text = model.playerDatas.Count.ToString();
        outline.text = model.playerDatas.Count.ToString();
    }
}
