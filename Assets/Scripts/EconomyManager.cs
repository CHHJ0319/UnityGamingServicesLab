using System;
using System.Collections.Generic;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnLoginSuccess += GetPlayerCredits;
        GameEvents.OnLoginSuccess += GetInventoryItems;
    }

    private void OnDisable()
    {
        GameEvents.OnLoginSuccess -= GetPlayerCredits;
        GameEvents.OnLoginSuccess -= GetInventoryItems;
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

            GetInventoryOptions options = new GetInventoryOptions
            {
                ItemsPerFetch = 5
            };

            GetInventoryResult inventoryResult = await EconomyService.Instance.PlayerInventory.GetInventoryAsync(options);
            List<PlayersInventoryItem> firstFiveItems = inventoryResult.PlayersInventoryItems;

            if (inventoryResult.HasNext)
            {
                inventoryResult = await inventoryResult.GetNextAsync(5);
                List<PlayersInventoryItem> nextFiveItems = inventoryResult.PlayersInventoryItems;
            }

            foreach (var item in inventoryResult.PlayersInventoryItems)
            {
                var definition = item.GetItemDefinition();

                string itemName = definition.Name;

                UIEvents.AddInventoryItem(itemName);
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
            string purchaseID = "PURCHASE1";
            MakeVirtualPurchaseResult purchaseResult = await EconomyService.Instance.Purchases.MakeVirtualPurchaseAsync(purchaseID);

        }
        catch (Exception e)
        {
            Debug.LogException(e);

        }

    }
}

