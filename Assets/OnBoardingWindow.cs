using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoardingWindow : MonoBehaviour
{
    public GameObject onBoardingWindow;

    private void Start()
    {
        if(PlayerPrefs.HasKey("OnBoarding"))
        {
            onBoardingWindow.SetActive(false);
        }
    }

    public void ShowedOnBoarding()
    {
        PlayerPrefs.SetInt("OnBoarding", 1);
    }
}
