using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscription : MonoBehaviour
{
    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _unLock;

    private bool _subscriptionActive;


    public void SubscriptionActive()
    {
        _subscriptionActive = true;
    }
}
