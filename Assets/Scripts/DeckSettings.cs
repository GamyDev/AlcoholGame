using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Decks")]
public class DeckSettings : ScriptableObject
{
    public Sprite icon;
    public List<Question> questions;
    public bool isOpen;
}

[System.Serializable]
public class Question
{
    public string name;
    [TextArea(3, 5)]
    public string text;
    public string players;
    public string timer;
}
