using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.CloudCode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MarketItem : MonoBehaviour
{
    [SerializeField] private ItemImage itemImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyBtn;

    private string listingId;

    public void Bind(ListingDto listing)
    {
        listingId = listing.listingId;

        if (nameText != null) nameText.text = listing.inventoryItemId;
        if (priceText != null) priceText.text = $"{listing.price}C";

        string[] parts = listing.inventoryItemId.Split('_');
        string lastPart = parts[parts.Length - 1];
        if (int.TryParse(lastPart, out int index))
        {
            itemImage.SetImageByIndex(index);
        }
        else
        {
            Debug.LogError($"'{lastPart}'는 숫자로 변환할 수 없습니다. 스트링 형식을 확인하세요.");
        }

        string description = ItemDescriptions.GetDescription(listing.inventoryItemId);
        if (descriptionText != null) descriptionText.text = description;

        if (buyBtn != null)
        {
            buyBtn.onClick.RemoveAllListeners();
            buyBtn.onClick.AddListener(() => _ = BuyAsync());
        }
    }

    private async Task BuyAsync()
    {
        await BuyListingAsync(listingId);
    }

    private async Task BuyListingAsync(string listingId)
    {
        try
        {
            var args = new Dictionary<string, object> { { "listing_id", listingId } };

            BuyResult res = await CloudCodeService.Instance.CallEndpointAsync<BuyResult>(
                "Mkt_BuyListing",
                args
            );

            UIManager.ShowPopupPanel($"구매 완료: newInstance={res.newPlayersInventoryItemId}");

            DataManager.UpdatePlayerPanel();
            DataManager.UpdateMarkerPanel();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            UIManager.ShowPopupPanel("구매 실패 (코인 부족/Listing 상태/Cloud Code 확인)");
        }
    }
}
