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

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayersModel playersModel;
    [SerializeField] private Decks decks;
    [SerializeField] private string typeDeck;
    [SerializeField] private Button buttonSpin;
    [SerializeField] private GameObject spinner;
    [SerializeField] private GameObject centerCard;
    [SerializeField] private OpenCard openCard;
    [SerializeField] private VFX vfx;
    [SerializeField] private GameObject rightCard;
    [SerializeField] private GameObject playerRoulette;
    [SerializeField] private GameObject playerTwoRoulette;

    [SerializeField] private SimpleScrollSnap simpleScrollSnap;
    [SerializeField] private SimpleScrollSnap simpleScrollSnap2;
    [SerializeField] private StartPlayersScreen playersScreen;

    public static bool reloadGame;
    
    private Vector3 centerCardPos;

    public static List<Player> currentPlayer;
    public static Question currentQuestion;

    private List<Question> questions;
    private List<Player> tempPlayers;

    private ScrollRect scrollRect;
    private ScrollRect scrollRect2;

    public void EnableSpinner()
    {
        if(currentQuestion.players == "1")
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
            centerCard.SetActive(false);
            simpleScrollSnap.gameObject.SetActive(false);
            simpleScrollSnap2.gameObject.SetActive(false);
            buttonSpin.gameObject.SetActive(false);
            openCard.gameObject.SetActive(true);
        }
    }

    public void BackToDecks()
    {
        reloadGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
     
    public void ResetSpinners()
    {

        scrollRect = simpleScrollSnap.GetComponent<ScrollRect>();
        scrollRect2 = simpleScrollSnap2.GetComponent<ScrollRect>();
        for (int i = 0; i < scrollRect2.content.transform.childCount; i++)
        {
            simpleScrollSnap2.Remove(i);
        }


        for (int i = 0; i < scrollRect.content.transform.childCount; i++)
        {
            simpleScrollSnap.Remove(i);
        }
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
            if(i % 2 == 0)
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

        simpleScrollSnap2.OnPanelCentered.AddListener((index, index2) => { SelectedPlayer(index, index2); });
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

        simpleScrollSnap.OnPanelCentered.AddListener((index, index2) => { SelectedPlayer(index, index2); });
    }

     

    private void Start()
    {
        currentPlayer = new List<Player>();
        tempPlayers = new List<Player>(); 

        scrollRect = simpleScrollSnap.GetComponent<ScrollRect>();
        scrollRect2 = simpleScrollSnap2.GetComponent<ScrollRect>();

        centerCardPos = centerCard.transform.localPosition;

        centerCard.transform.localPosition = rightCard.transform.localPosition;
        centerCard.transform.localScale = Vector3.zero;

        centerCard.transform.DOLocalMove(centerCardPos, 2).SetEase(Ease.InExpo);
        centerCard.transform.DOScale(Vector3.one, 3).SetEase(Ease.OutExpo);

        questions = new List<Question>();
        questions.Clear();
        questions = GetQustions();

        GetRandomQuestion();
        EnableSpinner();

        
        SetSettingsOneUser(); 
        
        SetSettingsTwoUsers();
        

        if(currentQuestion.players == "A")
        {
            openCard.gameObject.SetActive(true);
        }
    }

    private void SelectedPlayer(int index, int index2)
    {
        currentPlayer.Clear();
        Debug.Log($"Players count {currentQuestion.players}. Selected {index}");

        if(currentQuestion.players == "1") {  
            currentPlayer.Add(tempPlayers[index]);
        }

        if(currentQuestion.players == "2")
        {
            currentPlayer.Add(tempPlayers[index * 2]);
            if(index * 2 + 1 >= tempPlayers.Count)
            {
                currentPlayer.Add(tempPlayers[0]);
            } else
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
        buttonSpin.interactable = false;
        yield return new WaitForSeconds(5f);
       
        vfx.PlayVFX();
        yield return new WaitForSeconds(1f);
        buttonSpin.interactable = true;

        buttonSpin.gameObject.SetActive(false);
        spinner.gameObject.SetActive(false);
        simpleScrollSnap2.gameObject.SetActive(false);
        centerCard.gameObject.SetActive(false);

        openCard.gameObject.SetActive(false);
        openCard.gameObject.SetActive(true);
    }

    public void GetRandomQuestion()
    {
        if (questions.Count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        currentQuestion = questions[Random.Range(0, questions.Count)];
        questions.RemoveAt(Random.Range(0, questions.Count));
    }

    public List<Question> GetQustions()
    {
        List<Question> questions = new List<Question>();

        if (File.Exists(Application.persistentDataPath + "/Data/" + decks.deckSettings[0].name + ".csv"))
        {
            var cards = QuestionsParser.ReadCsv(decks.deckSettings[0].name);
            foreach (var item in cards)
            {
                questions.Add(new Question()
                {
                    name = "",
                    text = item.text,
                    players = item.count,
                    timer = item.timer
                });
            }
            typeDeck = "CsvFile";
            return questions;
        }

        typeDeck = "ScriptableObject";

        foreach (var item in decks.deckSettings[0].questions)
        {
            questions.Add(item);
        }

        return questions;
    }
}
