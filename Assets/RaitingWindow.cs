using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaitingWindow : MonoBehaviour
{
    public GameObject starContainer;
    public GameObject resultStarContainer;
    public UnityEvent OnStarsLessThree;
    public UnityEvent OnStarsMoreThree;
    public string url;
    public int currentRaiting; 

    private void OnEnable()
    {
        RaitingStar.selectedStar += RaitingStar_selectedStar;
    }

    private void OnDisable()
    {
        RaitingStar.selectedStar -= RaitingStar_selectedStar;
    }

    public void Rate()
    {
        if (currentRaiting > 3)
        {
            OnStarsMoreThree?.Invoke();
           // Application.OpenURL(url);
        }
        else
        {
            OnStarsLessThree?.Invoke();
        }
    }

    private void RaitingStar_selectedStar(int index)
    {
        currentRaiting = index + 1;

        for (int i = 0; i < starContainer.transform.childCount; i++)
        {
            if(i <= index)
            {
                starContainer.transform.GetChild(i).GetComponent<RaitingStar>().activeStar.SetActive(true);
                starContainer.transform.GetChild(i).GetComponent<RaitingStar>().disableStar.SetActive(false);
            } else
            {
                starContainer.transform.GetChild(i).GetComponent<RaitingStar>().activeStar.SetActive(false);
                starContainer.transform.GetChild(i).GetComponent<RaitingStar>().disableStar.SetActive(true);
            }
        }

        for (int i = 0; i < resultStarContainer.transform.childCount; i++)
        {
            if (i <= index)
            {
                resultStarContainer.transform.GetChild(i).GetComponent<RaitingStar>().activeStar.SetActive(true);
                resultStarContainer.transform.GetChild(i).GetComponent<RaitingStar>().disableStar.SetActive(false);
            }
            else
            {
                resultStarContainer.transform.GetChild(i).GetComponent<RaitingStar>().activeStar.SetActive(false);
                resultStarContainer.transform.GetChild(i).GetComponent<RaitingStar>().disableStar.SetActive(true);
            }
        }
    } 
}
