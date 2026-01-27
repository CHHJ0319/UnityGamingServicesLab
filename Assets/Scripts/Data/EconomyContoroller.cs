using System;
using System.Collections.Generic;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;

public class EconomyContoroller : MonoBehaviour
{
    private void Awake()
    {
        DataManager.SetEconomyContoroller(this);
    }

    public async void GetPlayerCredits()
    {
        try
        {
            GetBalancesOptions options = new GetBalancesOptions
            {
                ItemsPerFetch = 5
            };

            GetBalancesResult getBalancesResult = await EconomyService.Instance.PlayerBalances.GetBalancesAsync(options);
            List<PlayerBalance> firstFiveBalances = getBalancesResult.Balances;

            if (getBalancesResult.HasNext)
            {
                getBalancesResult = await getBalancesResult.GetNextAsync(options.ItemsPerFetch);
                List<PlayerBalance> nextFiveBalances = getBalancesResult.Balances;
            }

            int credits = (int)getBalancesResult.Balances[0].Balance;
            UIEvents.UpdateCredits(credits);

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async void GetInventoryItems()
    {
        try
        {
            await EconomyService.Instance.Configuration.SyncConfigurationAsync();

            UIEvents.ResetInventory();

            GetInventoryResult inventoryResult = await EconomyService.Instance.PlayerInventory.GetInventoryAsync();
            List<PlayersInventoryItem> firstFiveItems = inventoryResult.PlayersInventoryItems;

            foreach (var item in inventoryResult.PlayersInventoryItems)
            {
                UIManager.AddInvetoryItem(item);
            }

        }
        catch (Exception e)
        {
            Debug.LogException(e);

        }

    }

    public async void VirtualPurchase()
    {
        try
        {
            string purchaseID = "PURCHASE_SALAMANDER_SCALE_WITH_CREDITS";
            MakeVirtualPurchaseResult purchaseResult = await EconomyService.Instance.Purchases.MakeVirtualPurchaseAsync(purchaseID);

        }
        catch (Exception e)
        {
            Debug.LogException(e);

        }

    }
}

