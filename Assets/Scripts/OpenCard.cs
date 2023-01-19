using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OpenCard : MonoBehaviour
{
    [SerializeField] private PlayersModel playersModel;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private GameObject buttonTimer;
    [SerializeField] private GameObject buttonDone;

    private int currentSeconds;

    private void OnEnable()
    {
        transform.DOShakeScale(1, 1).SetEase(Ease.OutBounce);
        

        if(!string.IsNullOrEmpty(GameManager.currentQuestion.timer)) {
            currentSeconds = int.Parse(GameManager.currentQuestion.timer);
            buttonTimer.SetActive(true);
            buttonDone.SetActive(false);
            timer.text = currentSeconds.ToString();
            timer.transform.GetChild(0).GetComponent<TMP_Text>().text = currentSeconds.ToString();
        } else
        {
            buttonDone.SetActive(true);
            buttonTimer.SetActive(false);
        }

        title.text = GameManager.currentQuestion.name;
        text.text = GameManager.currentQuestion.text;

        player.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer.name;
        player.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer.name;

        player.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer.avatar];
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        buttonTimer.GetComponent<Button>().interactable = false;

        while (currentSeconds > 0) { 
            yield return new WaitForSeconds(1f);
            currentSeconds--;
            timer.text = currentSeconds.ToString();
            timer.transform.GetChild(0).GetComponent<TMP_Text>().text = currentSeconds.ToString();
        }

        buttonTimer.GetComponent<Button>().interactable = true;
        buttonTimer.SetActive(false);
        buttonDone.SetActive(true);
        yield return null;
    }
}
