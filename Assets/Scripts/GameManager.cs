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
using System.Threading;

[System.Serializable]
public class DeckLangs
{
    public List<DeckList> deckLists;
}


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

    [SerializeField] private Image deckImage;
    [SerializeField] private Image deckMiniImage;

    [SerializeField] private SimpleScrollSnap simpleScrollSnap;
    [SerializeField] private SimpleScrollSnap simpleScrollSnap2;
    [SerializeField] private StartPlayersScreen playersScreen;
    [SerializeField] private Animator gameAnimator;
    [SerializeField] private AudioSource _spinnerSource;

    public static List<Player> currentPlayer;
    public static List<Question> currentQuestion;

    private static CancellationTokenSource _cancelSpinToken;


    public List<Player> tempPlayers;

    private ScrollRect scrollRect;
    private ScrollRect scrollRect2;
    public List<DeckLangs> deckLists;

    public static int previousPlayerIndex;
    public static int currentPlayerIndex;

    public static int previousPlayer2Index;
    public static int currentPlayer2Index;

    public static int randomDeck;

    public Decks Decks => decks;

    public void GetRandomDeck()
    {
        if (!isQuestionsExists())
        {
            randomDeck = -1;
            return;
        } 
        randomDeck = SelectedDeck.selectedDeck[UnityEngine.Random.Range(0, SelectedDeck.selectedDeck.Count)];

        while(GetDeckListElement(LocalizationManager.SelectedLanguage, randomDeck).questions.Count == 0)
        {
            randomDeck = SelectedDeck.selectedDeck[UnityEngine.Random.Range(0, SelectedDeck.selectedDeck.Count)];
        }
    }

    public DeckList GetDeckListElement(int lang, int deck)
    {
        for (int j = 0; j < deckLists[lang].deckLists.Count; j++)
        {
            if (deckLists[lang].deckLists[j].deck == deck)
            {
                return deckLists[lang].deckLists[j];
            }
        }
        return null;
    }

    public bool isQuestionsExists()
    {
        for (int i = 0; i < SelectedDeck.selectedDeck.Count; i++)
        {
            if(GetDeckListElement(LocalizationManager.SelectedLanguage, SelectedDeck.selectedDeck[i]).questions.Count > 0)
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

        if (currentQuestion[LocalizationManager.SelectedLanguage].players == "1")
        {
            centerCard.SetActive(true);
            simpleScrollSnap.gameObject.SetActive(true);
            simpleScrollSnap2.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(true);
        }

        if (currentQuestion[LocalizationManager.SelectedLanguage].players == "2")
        {
            centerCard.SetActive(true);
            simpleScrollSnap2.gameObject.SetActive(true);
            simpleScrollSnap.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(true);
        }

        if (currentQuestion[LocalizationManager.SelectedLanguage].players == "A")
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
        LocalizationManager.OnLanguageChange -= LanguageChange;
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
        LocalizationManager.OnLanguageChange += LanguageChange;
        RemoveUserButtonHandler.UserRemove += OnUserRemove;
        GamePlayerList.AddPlayerEvent += OnAddPlayerEvent;
        SelectedDeck.OnDeckChange += DeckChange;
        currentQuestion = new List<Question>();
        currentPlayer = new List<Player>();
        tempPlayers = new List<Player>();
        deckLists = new List<DeckLangs>();

        scrollRect = simpleScrollSnap.GetComponent<ScrollRect>();
        scrollRect2 = simpleScrollSnap2.GetComponent<ScrollRect>(); 

        SetQustions();

        GetRandomQuestion();

        AnimDeck();

        SetSettingsOneUser();

        SetSettingsTwoUsers();

        EnableSpinner();
    }

    private void LanguageChange()
    {
        SetDeckInfo();
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
        if (currentQuestion[LocalizationManager.SelectedLanguage].players == "A")
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

        Debug.Log($"Players count {currentQuestion[LocalizationManager.SelectedLanguage].players}. Selected {index}");

        if (currentQuestion[LocalizationManager.SelectedLanguage].players == "1")
        {
            currentPlayer.Add(tempPlayers[index]);

            currentPlayerIndex = index;
        }

        if (currentQuestion[LocalizationManager.SelectedLanguage].players == "2")
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

    public async void DisableButton()
    {
        await StartSpin();
    }

    public void CancelSpinner()
    {
        scrollRect.StopMovement();
        scrollRect2.StopMovement();
        
        _spinnerSource.Stop();

        FindObjectOfType<SlotMachine>(true).StopSpine();

        ResetSpinners();
        SetSettingsOneUser();
        SetSettingsTwoUsers();

        buttonSpin.interactable = true;
        buttonSpin.transform.DOScale(Vector3.one, 0.2f);

       _cancelSpinToken?.Cancel();
    }

    private async UniTask StartSpin()
    {
        _cancelSpinToken = new CancellationTokenSource();

        currentPlayer.Clear();
        if (currentQuestion[LocalizationManager.SelectedLanguage].players == "1")
        {
            currentPlayerIndex = previousPlayerIndex;
        }

        if (currentQuestion[LocalizationManager.SelectedLanguage].players == "2")
        {
            currentPlayer2Index = previousPlayer2Index;
        }


        buttonSpin.interactable = false;
        buttonSpin.transform.DOScale(Vector3.zero, 0.2f);
        await UniTask.Delay(5000, false, 0, _cancelSpinToken.Token);
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

        currentQuestion.Clear();

        GetRandomDeck();
        SetDeckInfo();

        int randomQuestion = UnityEngine.Random.Range(0, GetDeckListElement(0, randomDeck).questions.Count);
        for (int j = 0; j < deckLists.Count; j++)
        {
            currentQuestion.Add(GetDeckListElement(j, randomDeck).questions[randomQuestion]);
            GetDeckListElement(j, randomDeck).questions.RemoveAt(randomQuestion);
        }
    }

    public void SetDeckInfo()
    {
        if(randomDeck != -1)
        {
            deckImage.sprite = decks.deckSettings[LocalizationManager.SelectedLanguage].deckSettings[randomDeck].icon;
            deckMiniImage.sprite = decks.deckSettings[LocalizationManager.SelectedLanguage].deckSettings[randomDeck].icon;

            titleDeck.text = decks.deckSettings[LocalizationManager.SelectedLanguage].deckSettings[randomDeck].deckTitle;
            titleDeckOutline.text = decks.deckSettings[LocalizationManager.SelectedLanguage].deckSettings[randomDeck].deckTitle;

        }
    }

    public void SetQustions()
    { 
        deckLists.Clear();

        for (int i = 0; i < decks.deckSettings.Count; i++)
        {
            deckLists.Add(new DeckLangs()
            {
                deckLists = GetQuestionsByLang(i)
            });
        } 
    }

    public List<Question> GetQuestions(int lang, int index)
    {
        List<Question> questions = new List<Question>();
        for (int i = 0; i < decks.deckSettings[lang].deckSettings[index].questions.Count; i++)
        {
            questions.Add(decks.deckSettings[lang].deckSettings[index].questions[i]);
        }
        
        return questions;
    }

    public List<DeckList> GetQuestionsByLang(int lang)
    {
        List<DeckList> list = new List<DeckList>();
        for (int i = 0; i < decks.deckSettings[lang].deckSettings.Count; i++)
        {
             
            if (SelectedDeck.selectedDeck.Contains(i))
            {
                list.Add(new DeckList()
                {
                    deck = i,
                    questions = GetQuestions(lang, i)
                });
            }
            
        }

        return list;
    }
}
