using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class OpenCard : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayersModel playersModel;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject imageCard;
    [SerializeField] private GameObject playerChoice; 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerTwo;
    [SerializeField] private GameObject playerAll;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private GameObject buttonTimer;
    [SerializeField] private GameObject buttonDone;
    [SerializeField] private Image backgroundCard;
    [SerializeField] private TMP_Text rules;
    [SerializeField] private GameObject rulesObj;

    private bool timerStarted;

    private static CancellationTokenSource _cancelToken;

    private int currentSeconds;

    public Image BackgroundCard => backgroundCard;

    public void AnimateOpenCard()
    {
        imageCard.transform.eulerAngles = new Vector3(0, 180, 0);
        text.gameObject.SetActive(false);
        playerChoice.SetActive(false);

        imageCard.transform.DORotate(new Vector3(0, 0, 0), 2).SetEase(Ease.OutElastic).OnUpdate(FlipAnimCallback);
    }

    public void SetCurrentPlayer(int index)
    {
        if (GameManager.currentQuestion[LocalizationManager.SelectedLanguage].players == "1")
        {
            GameManager.currentPlayer.Add(gameManager.tempPlayers[index]);
        }

        if (GameManager.currentQuestion[LocalizationManager.SelectedLanguage].players == "2")
        {
            GameManager.currentPlayer.Add(gameManager.tempPlayers[index * 2]);
            if (index * 2 + 1 >= gameManager.tempPlayers.Count)
            {
                GameManager.currentPlayer.Add(gameManager.tempPlayers[0]);
            }
            else
            {
                GameManager.currentPlayer.Add(gameManager.tempPlayers[index * 2 + 1]);
            }
        }
    }

    private void OnDisable()
    {
        LocalizationManager.OnLanguageChange -= LanguageChange;
    }

    private void OnEnable()
    {
        _cancelToken = new CancellationTokenSource();

        backgroundCard.sprite = gameManager.Decks.deckSettings[LocalizationManager.SelectedLanguage].deckSettings[GameManager.randomDeck].backgroundCard;
        rules.text = gameManager.Decks.deckSettings[LocalizationManager.SelectedLanguage].deckSettings[GameManager.randomDeck].deckDescription;

        LocalizationManager.OnLanguageChange += LanguageChange;
        AnimateOpenCard();
        
        if(!string.IsNullOrEmpty(GameManager.currentQuestion[LocalizationManager.SelectedLanguage].timer)) {
            currentSeconds = int.Parse(GameManager.currentQuestion[LocalizationManager.SelectedLanguage].timer);
            buttonTimer.SetActive(true);
            buttonDone.SetActive(false);
            timer.text = currentSeconds.ToString();
            timer.transform.GetChild(0).GetComponent<TMP_Text>().text = currentSeconds.ToString();
        } else
        {
            buttonDone.SetActive(true);
            buttonTimer.SetActive(false);
        }

        
        string question = GameManager.currentQuestion[LocalizationManager.SelectedLanguage].text;

        if (GameManager.currentQuestion[LocalizationManager.SelectedLanguage].players == "1") 
        {  
            if (GameManager.currentPlayerIndex != GameManager.previousPlayerIndex)
            { 
                question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
            } else
            {
                SetCurrentPlayer(GameManager.currentPlayerIndex);
                question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
            }
        }

        if(GameManager.currentQuestion[LocalizationManager.SelectedLanguage].players == "2")
        {
            if (GameManager.currentPlayer2Index != GameManager.previousPlayer2Index)
            {
                question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
                question = question.Replace("[player2]", $"<color=#DA2678>{GameManager.currentPlayer[1].name}</color>");
            } else
            {
                SetCurrentPlayer(GameManager.currentPlayer2Index);
                question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
                question = question.Replace("[player2]", $"<color=#DA2678>{GameManager.currentPlayer[1].name}</color>");
            }
        }

        text.text = question;

        if(GameManager.currentQuestion[LocalizationManager.SelectedLanguage].players == "A")
        {
            playerAll.SetActive(true);
            player.SetActive(false);
            playerTwo.SetActive(false);
        } 
        
        if(GameManager.currentQuestion[LocalizationManager.SelectedLanguage].players == "1")
        {
            playerAll.SetActive(false);
            player.SetActive(true);
            playerTwo.SetActive(false);
            if (GameManager.currentPlayerIndex != GameManager.previousPlayerIndex)
            {
                player.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;
                player.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;

                player.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[0].avatar];
            } else
            {
                Debug.LogError("Bug!!!!!");
                SetCurrentPlayer(GameManager.currentPlayerIndex);
                player.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;
                player.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;

                player.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[0].avatar];
            }
        }

        if (GameManager.currentQuestion[LocalizationManager.SelectedLanguage].players == "2")
        {
            playerAll.SetActive(false);
            player.SetActive(true);
            playerTwo.SetActive(true);

            if (GameManager.currentPlayer2Index != GameManager.previousPlayer2Index)
            {
                player.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;
                player.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;

                player.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[0].avatar];

                playerTwo.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[1].name;
                playerTwo.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[1].name;

                playerTwo.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[1].avatar];
            } else
            {
                Debug.LogError("Bug!!!!!");
                SetCurrentPlayer(GameManager.currentPlayer2Index);
                player.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;
                player.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[0].name;

                player.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[0].avatar];

                playerTwo.transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[1].name;
                playerTwo.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = GameManager.currentPlayer[1].name;

                playerTwo.GetComponent<Image>().sprite = playersModel.avatars[GameManager.currentPlayer[1].avatar];
            }
        }
    }

    private void LanguageChange()
    {
        if(gameObject.activeSelf)
        {
            string question = GameManager.currentQuestion[LocalizationManager.SelectedLanguage].text;

            if (GameManager.currentQuestion[LocalizationManager.SelectedLanguage].players == "1")
            {
                if (GameManager.currentPlayerIndex != GameManager.previousPlayerIndex)
                {
                    question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
                }
                else
                {
                    SetCurrentPlayer(GameManager.currentPlayerIndex);
                    question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
                }
            }

            if (GameManager.currentQuestion[LocalizationManager.SelectedLanguage].players == "2")
            {
                if (GameManager.currentPlayer2Index != GameManager.previousPlayer2Index)
                {
                    question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
                    question = question.Replace("[player2]", $"<color=#DA2678>{GameManager.currentPlayer[1].name}</color>");
                }
                else
                {
                    SetCurrentPlayer(GameManager.currentPlayer2Index);
                    question = question.Replace("[player]", $"<color=#DA2678>{GameManager.currentPlayer[0].name}</color>");
                    question = question.Replace("[player2]", $"<color=#DA2678>{GameManager.currentPlayer[1].name}</color>");
                }
            }

            text.text = question;
        }
    }

    private void FlipAnimCallback()
    {
        if (imageCard.transform.localEulerAngles.y >= 90 && imageCard.transform.localEulerAngles.y <= 270) {
            text.gameObject.SetActive(false);
            rulesObj.SetActive(false);
            playerChoice.SetActive(false);
        } else
        {
            text.gameObject.SetActive(true);
            playerChoice.SetActive(true);
            rulesObj.SetActive(true);
        }
    }

    public async void StartTimer()
    {
        if(timerStarted) {
            CancelTimer();
            buttonTimer.SetActive(false);
            buttonDone.SetActive(true);
            return;
        }

        await Timer();
    }

    async UniTask Timer()
    {
        timerStarted = true;
        while (currentSeconds > 0) {
            await UniTask.Delay(1000, false, 0, _cancelToken.Token);
            currentSeconds--;
            timer.text = currentSeconds.ToString();
            timer.transform.GetChild(0).GetComponent<TMP_Text>().text = currentSeconds.ToString();
        }

        timerStarted = false;
        buttonTimer.SetActive(false);
        buttonDone.SetActive(true);
    }

    public void CancelTimer()
    {
        _cancelToken?.Cancel();
        timerStarted = false;
    }
}
