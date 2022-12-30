using System.IO;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(QuestionsParser))]
public class QuestionsParserEditor : Editor
{
    private QuestionsParser importQuestions;

    private void OnEnable()
    {
        importQuestions = (QuestionsParser)target;    
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Update Data"))
        { 
            foreach (var item in importQuestions.QuestionData)
            {
                importQuestions.DownloadCsv(item.nameDeck, item.codeLink, OnComplete);
            }
        }
    }

    private void OnComplete(string deck, string data)
    { 
        if(string.IsNullOrEmpty(data)) { 
            Debug.LogError($"{deck} is empty or not downloaded succesfully!");
            return;
        }

        File.WriteAllText(Application.persistentDataPath + "/Data/" + deck + ".csv", data);
        Debug.Log($"{deck} downloaded succesfully! {QuestionsParser.ReadCsv(deck).Count} questions loaded!");
    }
}
