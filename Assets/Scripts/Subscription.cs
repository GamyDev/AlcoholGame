using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Subscription : MonoBehaviour
{
    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _unLock;

    private bool _subscriptionActive;


    private void OnEnable()
    {
        CheckSubscription();
    }

    public void SubscriptionActive()
    {
        _subscriptionActive = true;
        CheckSubscription();
    }

    void CheckSubscription()
    {
        if (_subscriptionActive)
        {
            _unLock.SetActive(true);
            _lock.SetActive(false);
        }
        else
        {
            _unLock.SetActive(false);
            _lock.SetActive(true);
        }
    }


}
