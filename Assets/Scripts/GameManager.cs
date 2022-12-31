using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayersModel playersModel;
    [SerializeField] private Decks decks;
    [SerializeField] private string typeDeck;
    [SerializeField] private Button buttonSpin;
    [SerializeField] private GameObject spinner;
    [SerializeField] private GameObject centerCard;
    [SerializeField] private OpenCard openCard;

    public static Player currentPlayer;
    public static Question currentQuestion;

    private List<Question> questions;

    private void OnEnable()
    {
        questions = new List<Question>();
        questions.Clear();
        questions = GetQustions();
    }

    public void DisableButton()
    {
        StartCoroutine(StartSpin());
    }

    IEnumerator StartSpin()
    {
        buttonSpin.interactable = false;
        yield return new WaitForSeconds(3f);
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
