using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using TMPro;

namespace Samples.Purchasing.Core.BuyingSubscription
{
    public class BuyingSubscription : MonoBehaviour, IStoreListener
    {
        IStoreController m_StoreController;

        // Your subscription ID. It should match the id of your subscription in your store.
        public string subscriptionProductId = "com.drinkboozegame.cardgames.subscription";

        public TextMeshProUGUI isSubscribedText;

        public static bool subscriptionActive = false;

        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _unLock;

        public static event Action<bool> OnSubscribtionChange;


        public void SetSubscribtion(bool value)
        {
            subscriptionActive = value;
            OnSubscribtionChange?.Invoke(value);
        }


  

        void Start()
        {
            InitializePurchasing();
        }

        void CheckSubscription()
        {
            if (subscriptionActive)
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

        void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            // Add our purchasable product and indicate its type.
            builder.AddProduct(subscriptionProductId, ProductType.Subscription);

            UnityPurchasing.Initialize(this, builder);
        }

        public void BuySubscription()
        {
            m_StoreController.InitiatePurchase(subscriptionProductId);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            m_StoreController = controller;

            UpdateUI();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log($"In-App Purchasing initialize failed: {error}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            // Retrieve the purchased product
            var product = args.purchasedProduct;

            Debug.Log($"Purchase Complete - Product: {product.definition.id}");
            subscriptionActive = true;
            CheckSubscription();

            UpdateUI();

            // We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        bool IsSubscribedTo(Product subscription)
        {
            // If the product doesn't have a receipt, then it wasn't purchased and the user is therefore not subscribed.
            if (subscription.receipt == null)
            {
                subscriptionActive = false;
                CheckSubscription();
                return false;
            }

            //The intro_json parameter is optional and is only used for the App Store to get introductory information.
            var subscriptionManager = new SubscriptionManager(subscription, null);

            // The SubscriptionInfo contains all of the information about the subscription.
            // Find out more: https://docs.unity3d.com/Packages/com.unity.purchasing@3.1/manual/UnityIAPSubscriptionProducts.html
            var info = subscriptionManager.getSubscriptionInfo();

            return info.isSubscribed() == Result.True;
        }

        void UpdateUI()
        {
            var subscriptionProduct = m_StoreController.products.WithID(subscriptionProductId);

            try
            {
                var isSubscribed = IsSubscribedTo(subscriptionProduct);
                isSubscribedText.text = isSubscribed ? "You are subscribed" : "You are not subscribed";
            }
            catch (StoreSubscriptionInfoNotSupportedException)
            {
                var receipt = (Dictionary<string, object>)MiniJson.JsonDecode(subscriptionProduct.receipt);
                var store = receipt["Store"];
                /*isSubscribedText.text =
                    "Couldn't retrieve subscription information because your current store is not supported.\n" +
                    $"Your store: \"{store}\"\n\n" +
                    "You must use the App Store, Google Play Store or Amazon Store to be able to retrieve subscription information.\n\n" +
                    "For more information, see README.md";*/

               
            }
        }
    }
}
