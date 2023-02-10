using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckSettingsList
{
    public List<DeckSettings> deckSettings;
}

public class Decks : MonoBehaviour
{
    public List<DeckSettingsList> deckSettings;
}
