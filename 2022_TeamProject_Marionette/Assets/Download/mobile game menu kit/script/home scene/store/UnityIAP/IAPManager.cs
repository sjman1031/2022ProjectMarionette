#if UNITY_PURCHASING
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;


public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager THIS;

    store_button currentStoreButton;//it will call the code that give the stuff if the purchase is ok
    public void SetCurrentStoreButton(store_button thisButton)
    {
        currentStoreButton = thisButton;
    }

    StoreManager storeItemsManager;

    IStoreController controller;
    IExtensionProvider extensions;

    game_master my_game_master;

    [System.Serializable]
    public class IAPItem
    {
        public ProductType productType;//consumable or not consumable
        public string productID;//this id must be the same of the item in your IOS/Android store

        //this will call the code needed when the item is purchased
        public StoreItem myStoreItem;
        public int storeContainerSlot;
        public int storeItemSlot;


    }
    [Tooltip("This list is public ONLY to allow you to check if all IAP items are loaded with the same productID and price that you use in your IOS/Google store. Leave it EMPTY: it will auto-fill when the app start")]
    public List<IAPItem> IAPItems;

    private void Awake()
    {
        THIS = this;
        storeItemsManager = GetComponent<StoreManager>();
        my_game_master = GetComponent<game_master>();

    }

    // Use this for initialization
    void Start () {
        FindItems();
        InitializePurchasing();
    }

    void FindItems()
    {
        IAPItems = new List<IAPItem>();

        for (int c = 0; c < storeItemsManager.storeContainers.Length; c++)//look in each container
        {
            if (!storeItemsManager.storeContainers[c].storeTab)//ignore the cointainer don't showed in the store
                continue;

            //seach item that require real money
            for (int i = 0; i < storeItemsManager.storeContainers[c].myItems.Length; i++)
            {
                if (storeItemsManager.storeContainers[c].myItems[i].priceCurrency != StoreItem.PriceCurrency.RealMoney)
                    continue;

                for (int l = 0; l < storeItemsManager.storeContainers[c].myItems[i].myItem.Length; l++)
                {
                    for (int profile = 0; profile < my_game_master.number_of_save_profile_slot_avaibles; profile++)
                    {
                        IAPItem tempItem = new IAPItem();
                        tempItem.productType = (ProductType)storeItemsManager.storeContainers[c].myItems[i].productType;
                        tempItem.myStoreItem = storeItemsManager.storeContainers[c].myItems[i];
                        tempItem.storeContainerSlot = c;
                        tempItem.storeItemSlot = i;


                        tempItem.productID = GetProfileID(profile) + storeItemsManager.storeContainers[c].myItems[i].myItem[l].productID;

                        IAPItems.Add(tempItem);
                    }
                }
            }
        }
    }


    string GetProfileID(int profile)
    {
        string profileId = "";
        if (my_game_master.number_of_save_profile_slot_avaibles > 1)
            profileId = "p" + profile + "_";

        return profileId;
    }


    void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Use your own products
        for (int c = 0; c < storeItemsManager.storeContainers.Length; c++)//look in each container
        {
            if (!storeItemsManager.storeContainers[c].storeTab)//ignore the cointainer don't showed in the store
                continue;

            //seach item that require real money
            for (int i = 0; i < IAPItems.Count; i++)
            {
                builder.AddProduct(IAPItems[i].productID, IAPItems[i].productType);
            }
        }


        UnityPurchasing.Initialize(this, builder);
    }


    bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return controller != null && extensions != null;
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("IAP: Initialized successfully");
        this.controller = controller;
        this.extensions = extensions;
    }


    /// <summary>
    /// Called when Unity IAP encounters an unrecoverable initialization error.
    ///
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void BuyProductID(string productId)
    {

        productId = GetProfileID(my_game_master.current_profile_selected) + productId;

        if (IsInitialized())
        {
            Product product = controller.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                controller.InitiatePurchase(product);
            }
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        bool validPurchase = true; // Presume valid for platforms with no R.V.

        // Unity IAP's validation logic is only included on these platforms.
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
         // Prepare the validator with the secrets we prepared in the Editor
         // obfuscation window.
         var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),AppleTangle.Data(), Application.identifier);
 
         try
         {
             // On Google Play, result has a single product ID.
             // On Apple stores, receipts contain multiple products.
             var result = validator.Validate(e.purchasedProduct.receipt);
             // For informational purposes, we list the receipt(s)
             Debug.Log("Receipt is valid. Contents:");
             foreach (IPurchaseReceipt productReceipt in result)
             {
                             if (productReceipt.productID != e.purchasedProduct.definition.id)
             {
                 Debug.Log("Invalid receipt data");
                 validPurchase = false;
             }
             }
         }
         catch (IAPSecurityException)
         {
             Debug.Log("Invalid receipt, not unlocking content");
             validPurchase = false;
#if UNITY_EDITOR
             validPurchase = true;
#endif
         }
#endif

        //apply the purchasing in case if the transaction is valid
        if (validPurchase)
        {
            currentStoreButton.Give_the_stuff();
            currentStoreButton = null;
            /*
            for (int i = 0; i < IAPItems.Count; i++)
            {
                if (IAPItems[i].productID == e.purchasedProduct.definition.id)
                {
                    IAPItems[i].myStoreItem.ExecuteThisCodeWhenPurchased(my_game_master);
                    break;
                }
            }*/

        }

        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product p, PurchaseFailureReason r)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", p.definition.storeSpecificId, r));
    }

    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer
                || Application.platform == RuntimePlatform.OSXPlayer
                || Application.platform == RuntimePlatform.tvOS)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = extensions.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions(OnTransactionsRestored);
        }
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    void OnTransactionsRestored(bool success)
    {
        Debug.Log("Transactions restored " + success.ToString());
    }

}
#endif