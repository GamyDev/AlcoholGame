using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnBoardingStars : MonoBehaviour
{
    public int delay; 
    public UnityEvent OnDisablePanel;

    

    private async void OnEnable()
    {
        await DelayDisable();   
    }

    private async UniTask DelayDisable()
    {
        await UniTask.Delay(delay);
        gameObject.SetActive(false);
        OnDisablePanel?.Invoke();
    }
}
