using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Subscription : MonoBehaviour
{
    [SerializeField] private GameObject _lock;
    [SerializeField] private GameObject _unLock;
    [SerializeField] private GameObject _subscribeWindow;

    public static bool subscriptionActive = false;

    public static Subscription instance;

    public GameObject SubscribeWindow => _subscribeWindow;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        CheckSubscription();
    }

    public void Subscribe()
    {
        FindObjectOfType<SelectedDeck>().UnlockDecks();
        subscriptionActive = true;
    }

    public void SubscriptionActive()
    {
        //    subscriptionActive = true;
        //    CheckSubscription();

    }

    void CheckSubscription()
    {
        //if (subscriptionActive)
        //{
        //    _unLock.SetActive(true);
        //    _lock.SetActive(false);
        //}
        //else
        //{
        //    _unLock.SetActive(false);
        //    _lock.SetActive(true);
        //}
    }


}
