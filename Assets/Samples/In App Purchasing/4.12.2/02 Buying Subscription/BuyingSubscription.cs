using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;


namespace Samples.Purchasing.Core.BuyingSubscription
{
    public class BuyingSubscription : MonoBehaviour, IDetailedStoreListener
    {
        IStoreController m_StoreController;

        // Your subscription ID. It should match the id of your subscription in your store.
        public string subscriptionProductId = "com.mycompany.mygame.my_vip_pass_subscription";

        public Text isSubscribedText;

        void Start()
        {
            InitializePurchasing();
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
            if (m_StoreController != null)
            {
                m_StoreController.InitiatePurchase(subscriptionProductId);
            }
            else
            {
                Debug.LogError("StoreController is not initialized.");
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            m_StoreController = controller;

            UpdateUI();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailed(error, null);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

            if (!string.IsNullOrEmpty(message))
            {
                errorMessage += $" More details: {message}";
            }

            Debug.LogError(errorMessage);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            // Retrieve the purchased product
            var product = args.purchasedProduct;

            Debug.Log($"Purchase Complete - Product: {product.definition.id}");

            UpdateUI();

            // We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogError($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.LogError($"Purchase failed - Product: '{product.definition.id}', " +
                $"Purchase failure reason: {failureDescription.reason}, " +
                $"Purchase failure details: {failureDescription.message}");
        }

        bool IsSubscribedTo(Product subscription)
        {
            // If the product doesn't have a receipt, then it wasn't purchased and the user is therefore not subscribed.
            if (subscription == null || string.IsNullOrEmpty(subscription.receipt))
            {
                return false;
            }

            try
            {
                //The intro_json parameter is optional and is only used for the App Store to get introductory information.
                var subscriptionManager = new SubscriptionManager(subscription, null);

                // The SubscriptionInfo contains all of the information about the subscription.
                var info = subscriptionManager.getSubscriptionInfo();

                return info.isSubscribed() == Result.True;
            }
            catch (Exception e)
            {
                Debug.LogError($"Error checking subscription status: {e.Message}");
                return false;
            }
        }

        void UpdateUI()
        {
            if (m_StoreController == null)
            {
                Debug.LogError("StoreController is not initialized.");
                return;
            }

            var subscriptionProduct = m_StoreController.products.WithID(subscriptionProductId);

            try
            {
                bool isSubscribed = IsSubscribedTo(subscriptionProduct);
                isSubscribedText.text = isSubscribed ? "You are subscribed" : "You are not subscribed";

                if (isSubscribed)
                {
                    FindObjectOfType<SelectedDeck>(true)?.UnlockDecks();
                }
            }
            catch (StoreSubscriptionInfoNotSupportedException)
            {
                var receipt = MiniJson.JsonDecode(subscriptionProduct.receipt) as Dictionary<string, object>;
                if (receipt != null && receipt.TryGetValue("Store", out var store))
                {
                    isSubscribedText.text =
                        "Couldn't retrieve subscription information because your current store is not supported.\n" +
                        $"Your store: \"{store}\"\n\n" +
                        "You must use the App Store, Google Play Store or Amazon Store to be able to retrieve subscription information.\n\n" +
                        "For more information, see README.md";
                }
                else
                {
                    isSubscribedText.text = "Subscription info not available.";
                }
            }
        }
    }
}
