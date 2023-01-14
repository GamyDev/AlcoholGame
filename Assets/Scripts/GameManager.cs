using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using DanielLochner.Assets.SimpleScrollSnap;
using TMPro;

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

    private SimpleScrollSnap simpleScrollSnap;
    private StartPlayersScreen playersScreen;
    
    private Vector3 centerCardPos;

    public static Player currentPlayer;
    public static Question currentQuestion;

    private List<Question> questions;

      
    private void OnEnable()
    {

        simpleScrollSnap = FindObjectOfType<SimpleScrollSnap>();
        playersScreen = FindObjectOfType<StartPlayersScreen>();

        var scrollRect = simpleScrollSnap.GetComponent<ScrollRect>();

        centerCardPos = centerCard.transform.localPosition;

        centerCard.transform.localPosition = rightCard.transform.localPosition;
        centerCard.transform.localScale = Vector3.zero;

        centerCard.transform.DOLocalMove(centerCardPos, 2).SetEase(Ease.InExpo);
        centerCard.transform.DOScale(Vector3.one, 3).SetEase(Ease.OutExpo);

        questions = new List<Question>();
        questions.Clear();
        questions = GetQustions();


        //������� spacing � �����
        simpleScrollSnap.InfiniteScrollingSpacing = 0;
         

        for (int i = 0; i < playersModel.playerDatas.Count; i++)
        {
            simpleScrollSnap.AddToBack(playersScreen.PlayerPrefab);
        }

        for (int i = 0; i < playersModel.playerDatas.Count; i++)
        {
            scrollRect.content.GetChild(i).GetComponent<Image>().sprite = playersModel.avatars[playersModel.playerDatas[i].avatar];
            scrollRect.content.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = playersModel.playerDatas[i].name;
        }

        simpleScrollSnap.OnPanelCentered.AddListener((index, index2) => { SelectedPlayer(index, index2); });
    }

    private void SelectedPlayer(int index, int index2)
    {
        Debug.Log(index);
        currentPlayer = playersModel.playerDatas[index];
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
        centerCard.gameObject.SetActive(false);

        openCard.gameObject.SetActive(false);
        openCard.gameObject.SetActive(true);
    }

    public void GetRandomUser()
    {
        currentPlayer = playersModel.playerDatas[Random.Range(0, playersModel.playerDatas.Count)];
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
                    name = item.title,
                    text = item.text
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
