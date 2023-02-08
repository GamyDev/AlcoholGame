using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using DanielLochner.Assets.SimpleScrollSnap;
using TMPro;
using Cysharp.Threading.Tasks;
using System;

[System.Serializable]
public class DeckList
{
    public int deck;
    public List<Question> questions;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayersModel playersModel;
    [SerializeField] private Decks decks;
    [SerializeField] private string typeDeck;
    [SerializeField] private Button buttonSpin;
    [SerializeField] private GameObject spinner;
    [SerializeField] private GameObject centerCard;
    [SerializeField] private OpenCard openCard;
    [SerializeField] private GameObject rightCard;
    [SerializeField] private GameObject playerRoulette;
    [SerializeField] private GameObject playerTwoRoulette;

    [SerializeField] private TMP_Text titleDeck;
    [SerializeField] private TMP_Text titleDeckOutline;

    [SerializeField] private TMP_Text descriptionDeck;
    [SerializeField] private TMP_Text descriptionDeckOutline;

    [SerializeField] private Image deckImage;
    [SerializeField] private Image deckMiniImage;

    [SerializeField] private SimpleScrollSnap simpleScrollSnap;
    [SerializeField] private SimpleScrollSnap simpleScrollSnap2;
    [SerializeField] private StartPlayersScreen playersScreen;
    [SerializeField] private Animator gameAnimator;

    [SerializeField] private GameObject pool;

    private static Action OnComplete;

    public static bool reloadGame;

    private Vector3 centerCardPos;

    public static List<Player> currentPlayer;
    public static Question currentQuestion;

    
    public List<Player> tempPlayers;

    private ScrollRect scrollRect;
    private ScrollRect scrollRect2;
    public List<DeckList> deckLists;

    public static int previousPlayerIndex;
    public static int currentPlayerIndex;

    public static int previousPlayer2Index;
    public static int currentPlayer2Index;

    public static int randomDeck;

    public void GetRandomDeck()
    {
        if (!isQuestionsExists())
        {
            randomDeck = -1;
            return;
        } 
        randomDeck = SelectedDeck.selectedDeck[UnityEngine.Random.Range(0, SelectedDeck.selectedDeck.Count)];
        while(GetDeckListElement(randomDeck).questions.Count == 0)
        {
            randomDeck = SelectedDeck.selectedDeck[UnityEngine.Random.Range(0, SelectedDeck.selectedDeck.Count)];
        }
    }

    public DeckList GetDeckListElement(int deck)
    {
        for (int i = 0; i < deckLists.Count; i++)
        {
            if(deckLists[i].deck == deck)
            {
                return deckLists[i];
            }
        }

        return null;
    }

    public bool isQuestionsExists()
    {
        for (int i = 0; i < SelectedDeck.selectedDeck.Count; i++)
        {
            if(GetDeckListElement(SelectedDeck.selectedDeck[i]).questions.Count > 0)
            {
                return true;
            }
        }

        return false;
    }

    public void EnableSpinner()
    {

        simpleScrollSnap.gameObject.SetActive(false);
        simpleScrollSnap.gameObject.SetActive(true);

        simpleScrollSnap2.gameObject.SetActive(false);
        simpleScrollSnap2.gameObject.SetActive(true);

        if (currentQuestion.players == "1")
        {
            centerCard.SetActive(true);
            simpleScrollSnap.gameObject.SetActive(true);
            simpleScrollSnap2.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(true);
        }

        if (currentQuestion.players == "2")
        {
            centerCard.SetActive(true);
            simpleScrollSnap2.gameObject.SetActive(true);
            simpleScrollSnap.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(true);
        }

        if (currentQuestion.players == "A")
        {
            centerCard.SetActive(true);
            simpleScrollSnap.gameObject.SetActive(false);
            simpleScrollSnap2.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(false);
        }
    }

     

    public void ResetSpinners()
    { 
        scrollRect = simpleScrollSnap.GetComponent<ScrollRect>();
        scrollRect2 = simpleScrollSnap2.GetComponent<ScrollRect>();


        for (int j = 0; j < tempPlayers.Count; j++)
        { 
            simpleScrollSnap2.RemoveFromBack(); 
        }

        for (int j = 0; j < tempPlayers.Count; j++)
        {
            simpleScrollSnap.RemoveFromBack();
        }

        tempPlayers.Clear();
    }

    public void SetSettingsTwoUsers()
    {
        simpleScrollSnap2.InfiniteScrollingSpacing = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < playersModel.playerDatas.Count; j++)
            {
                tempPlayers.Add(playersModel.playerDatas[j]);
            }
        }


        for (int i = 0; i < Mathf.CeilToInt(tempPlayers.Count / 2); i++)
        {
            simpleScrollSnap2.AddToBack(playerTwoRoulette);
        }

        for (int i = 0; i < tempPlayers.Count; i++)
        {
            if (i % 2 == 0)
            {
                scrollRect2.content.GetChild(i / 2).GetChild(0).GetComponent<Image>().sprite = playersModel.avatars[tempPlayers[i].avatar];
                scrollRect2.content.GetChild(i / 2).GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i].name;
                scrollRect2.content.GetChild(i / 2).GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i].name;

                if (i + 1 < tempPlayers.Count)
                {
                    scrollRect2.content.GetChild(i / 2).GetChild(1).GetComponent<Image>().sprite = playersModel.avatars[tempPlayers[i + 1].avatar];
                    scrollRect2.content.GetChild(i / 2).GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i + 1].name;
                    scrollRect2.content.GetChild(i / 2).GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i + 1].name;
                }
            }

        }

        simpleScrollSnap2.OnPanelCentered += SelectedPlayer;
    }



    public void SetSettingsOneUser()
    {
        simpleScrollSnap.InfiniteScrollingSpacing = 0;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < playersModel.playerDatas.Count; j++)
            {
                tempPlayers.Add(playersModel.playerDatas[j]);
            }
        }


        for (int i = 0; i < tempPlayers.Count; i++)
        {
            simpleScrollSnap.AddToBack(playerRoulette);
        }

        for (int i = 0; i < tempPlayers.Count; i++)
        {
            scrollRect.content.GetChild(i).GetComponent<Image>().sprite = playersModel.avatars[tempPlayers[i].avatar];
            scrollRect.content.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i].name;
            scrollRect.content.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>().text = tempPlayers[i].name;
        }

        simpleScrollSnap.OnPanelCentered += SelectedPlayer;
    }

    private void OnDisable()
    {
        simpleScrollSnap.OnPanelCentered -= SelectedPlayer;
        simpleScrollSnap2.OnPanelCentered -= SelectedPlayer;
        SelectedDeck.OnDeckChange -= DeckChange;
        RemoveUserButtonHandler.UserRemove -= OnUserRemove;
        GamePlayerList.AddPlayerEvent -= OnAddPlayerEvent;
    }

    public void AnimDeck()
    {
        gameAnimator.SetBool("Fly", true);
        Invoke("EnableAllAnimation", 1.5f);
    }

    private void OnEnable()
    {
        RemoveUserButtonHandler.UserRemove += OnUserRemove;
        GamePlayerList.AddPlayerEvent += OnAddPlayerEvent;
        SelectedDeck.OnDeckChange += DeckChange;
        currentPlayer = new List<Player>();
        tempPlayers = new List<Player>();
        deckLists = new List<DeckList>();

        scrollRect = simpleScrollSnap.GetComponent<ScrollRect>();
        scrollRect2 = simpleScrollSnap2.GetComponent<ScrollRect>();

        centerCardPos = centerCard.transform.localPosition;

        SetQustions();

        GetRandomQuestion();

        AnimDeck();

        SetSettingsOneUser();

        SetSettingsTwoUsers();

        EnableSpinner();
    }

    private void OnAddPlayerEvent()
    {
        ResetSpinners();
        SetSettingsOneUser();
        SetSettingsTwoUsers();
    }

    private void OnUserRemove(int index)
    {
        ResetSpinners();
        SetSettingsOneUser();
        SetSettingsTwoUsers();
    }
    private void DeckChange()
    {
        SetQustions();
        GetRandomDeck();

        if (!openCard.gameObject.activeSelf) { 
            GetRandomQuestion();
            EnableSpinner();
            AnimDeck();
        }
    }

    public void EnableAllAnimation()
    {
        if (currentQuestion.players == "A")
        {
            gameAnimator.SetBool("Fly", false);
            centerCard.SetActive(false);
            simpleScrollSnap.gameObject.SetActive(false);
            simpleScrollSnap2.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(false);
            openCard.gameObject.SetActive(true);
            openCard.GetComponent<OpenCard>().AnimateOpenCard();
        }
    }


    private void SelectedPlayer(int index, int index2)
    { 

        Debug.Log($"Players count {currentQuestion.players}. Selected {index}");

        if (currentQuestion.players == "1")
        {
            currentPlayer.Add(tempPlayers[index]);

            currentPlayerIndex = index;
        }

        if (currentQuestion.players == "2")
        {
            currentPlayer2Index = index;

            currentPlayer.Add(tempPlayers[index * 2]);
            if (index * 2 + 1 >= tempPlayers.Count)
            {
                currentPlayer.Add(tempPlayers[0]);
            }
            else
            {
                currentPlayer.Add(tempPlayers[index * 2 + 1]);
            }
        }
    }

    public void DisableButton()
    {
        StartCoroutine(StartSpin());
    }

    IEnumerator StartSpin()
    {
        currentPlayer.Clear();
        if (currentQuestion.players == "1")
        {
            currentPlayerIndex = previousPlayerIndex;
        }

        if (currentQuestion.players == "2")
        {
            currentPlayer2Index = previousPlayer2Index;
        }


        buttonSpin.interactable = false;
        buttonSpin.transform.DOScale(Vector3.zero, 0.2f);
        yield return new WaitForSeconds(5f);
        buttonSpin.interactable = true;
        buttonSpin.transform.DOScale(Vector3.one, 0.2f);

        buttonSpin.gameObject.SetActive(false);
        spinner.gameObject.SetActive(false);
        simpleScrollSnap2.gameObject.SetActive(false);
        centerCard.gameObject.SetActive(false);

        openCard.gameObject.SetActive(false);
        openCard.gameObject.SetActive(true);
        gameAnimator.SetBool("Fly", false);
    }

    public void GetRandomQuestion()
    {
        if (!isQuestionsExists())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        GetRandomDeck();
        SetDeckInfo();

        int randomQuestion = UnityEngine.Random.Range(0, GetDeckListElement(randomDeck).questions.Count);
        currentQuestion = GetDeckListElement(randomDeck).questions[randomQuestion];
        Debug.Log($"{GetDeckListElement(randomDeck).questions[randomQuestion].text} - {GetDeckListElement(randomDeck).questions[randomQuestion].players}");
        GetDeckListElement(randomDeck).questions.RemoveAt(randomQuestion);
        Debug.Log($"{currentQuestion.text} - {currentQuestion.players}");
    }

    public void SetDeckInfo()
    {
        if(randomDeck != -1)
        {
            deckImage.sprite = decks.deckSettings[randomDeck].icon;
            deckMiniImage.sprite = decks.deckSettings[randomDeck].icon;

            titleDeck.text = decks.deckSettings[randomDeck].deckTitle;
            titleDeckOutline.text = decks.deckSettings[randomDeck].deckTitle;

            descriptionDeck.text = decks.deckSettings[randomDeck].deckDescription;
            descriptionDeckOutline.text = decks.deckSettings[randomDeck].deckDescription;
        }
    }

    public void SetQustions()
    { 
        deckLists.Clear();

        for (int i = 0; i < SelectedDeck.selectedDeck.Count; i++)
        {
            deckLists.Add(new DeckList()
            {
                deck = SelectedDeck.selectedDeck[i],
                questions = decks.deckSettings[SelectedDeck.selectedDeck[i]].questions
            });
        }
    }
}
