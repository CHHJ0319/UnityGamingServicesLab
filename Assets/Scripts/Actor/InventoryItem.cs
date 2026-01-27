using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.CloudCode;
using Unity.Services.Economy.Model;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button sellBtn;

    [SerializeField] private TMP_InputField priceInput;

    private string playersInventoryItemId;

    private string currencyId = "CREDIT";

    public void Bind(
        PlayersInventoryItem item)
    {
        playersInventoryItemId = item.PlayersInventoryItemId;

        if (nameText != null) nameText.text = item.GetItemDefinition().Name;

        string description = ItemDescriptions.GetDescription(item.InventoryItemId);
        if (descriptionText != null) descriptionText.text = description;

        if (sellBtn != null)
        {
            sellBtn.onClick.RemoveAllListeners();
            sellBtn.onClick.AddListener(() => { _ = SellAsync(); });
        }
    }

    private async Task SellAsync()
    {
        if (string.IsNullOrEmpty(playersInventoryItemId))
        {
            Debug.LogError("[SellAsync] playersInventoryItemId is null or empty!");
            return;
        }

        int price = GetPrice();
        Debug.Log($"[SellAsync] Calling createListingAsync with ID: {playersInventoryItemId}, Price: {price}");

        await CreateListingAsync(playersInventoryItemId, price);
    }

    private int GetPrice()
    {
        if (priceInput == null) return 100;
        if (int.TryParse(priceInput.text, out int price)) return Mathf.Max(1, price);
        return 100;
    }

    private async Task CreateListingAsync(string playersInventoryItemId, int price)
    {
        try
        {
            var args = new Dictionary<string, object>
            {
                { "players_inventory_item_id", playersInventoryItemId },
                { "price", price },
                { "currency_id", currencyId }
            };

            CreateListingResult res = await CloudCodeService.Instance.CallEndpointAsync<CreateListingResult>(
                "Mkt_CreateListing",
                args
            );

            DataManager.UpdatePlayerPanel();
            DataManager.UpdateMarkerPanel();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
