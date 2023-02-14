using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedDeck : MonoBehaviour, IPointerClickHandler
{
    public static List<int> selectedDeck = new List<int>();

    public GameObject deckContainer;
    public GameObject check;
    public GameObject unCheck;
    public GameObject lockCheck;
    public static event Action OnDeckChange; 

    public void UnlockDecks()
    { 
        for (int i = 0; i < deckContainer.transform.childCount; i++)
        {
            if (selectedDeck.Contains(i))
            {
                if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck)
                {
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(true);
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(false);
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck.SetActive(false);
                }

            }
            else
            {
                if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck)
                {
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(false);
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(true);
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck.SetActive(false);
                }
            }
        }
    }

    private void OnEnable()
    {
        if(selectedDeck.Count == 0)
        {
            selectedDeck.Add(0);
        }

        if (Subscription.subscriptionActive)
        {

            for (int i = 0; i < deckContainer.transform.childCount; i++)
            {
                if (selectedDeck.Contains(i))
                {
                    if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck)
                    {
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(true);
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(false);
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck.SetActive(false);
                    }

                }
                else
                {
                    if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck)
                    {
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(false);
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(true);
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck.SetActive(false);
                    }
                }
            }
        } else
        { 
            for (int i = 0; i < deckContainer.transform.childCount; i++)
            {
                if(i == 0)
                {
                    if (selectedDeck.Contains(i))
                    {
                        if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck)
                        {
                            deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(true);
                            deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(false);
                            deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck.SetActive(false);
                        }

                    }
                    else
                    {
                        if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck)
                        {
                            deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(false);
                            deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(true);
                            deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck.SetActive(false);
                        }
                    }
                } else
                {
                    if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck)
                    {
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(false);
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(false);
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck.SetActive(true);
                    }
                }
                
            }

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Subscription.subscriptionActive)
        {
            if (selectedDeck.Contains(transform.parent.GetSiblingIndex()))
            {
                if (selectedDeck.Count > 1)
                    selectedDeck.Remove(transform.parent.GetSiblingIndex());

            }
            else
            {
                selectedDeck.Add(transform.parent.GetSiblingIndex());
            }


            for (int i = 0; i < deckContainer.transform.childCount; i++)
            {
                if (selectedDeck.Contains(i))
                {
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(true);
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(false);
                    deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck.SetActive(false);
                }
                else
                {
                    if (deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>() && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check && deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck)
                    {
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().check.SetActive(false);
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().unCheck.SetActive(true);
                        deckContainer.transform.GetChild(i).GetChild(1).GetComponent<SelectedDeck>().lockCheck.SetActive(false);
                    }
                }
            }
            OnDeckChange?.Invoke();
        } else
        {
            if(transform.parent.GetSiblingIndex() > 0)
            {
                Subscription.instance.SubscribeWindow.SetActive(true);
            }
        }

        
    }
}
