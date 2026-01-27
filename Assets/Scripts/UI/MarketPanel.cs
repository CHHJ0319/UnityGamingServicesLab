using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using UnityEngine;
using UnityEngine.UI;

public class MarketPanel : MonoBehaviour
{
    public Button refreshButton;

    public GameObject marketItemPrefabs;
    public Transform market;

    private int marketLimit = 30;
    private string marketSort = "NEWEST";

    private void Awake()
    {
        DataManager.SetMarketPanel(this);
    }

    void Start()
    {
        if (refreshButton != null) refreshButton.onClick.AddListener(() => _ = RefreshAllAsync());
    }

    public async Task RefreshAllAsync()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            UIManager.ShowPopupPanel("로그인 필요");
            return;
        }

        DataManager.UpdatePlayerPanel();
        GetMarketItems();
    }

    public void ClearMarket()
    {
        foreach (Transform item in market)
        {
            Destroy(item.gameObject);
        }
    }

    public async void GetMarketItems()
    {
        ClearMarket();

        try 
        {
            var args = new Dictionary<string, object>
            {
                { "limit", marketLimit },
                { "sort", marketSort }
            };

            MarketListResult res = await CloudCodeService.Instance.CallEndpointAsync<MarketListResult>(
                    "Mkt_GetActiveListings",
                    args
                );

            if (res.listings == null)
            {
                UIManager.ShowPopupPanel("거래소 목록 0개");
                return;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            UIManager.ShowPopupPanel("거래소 조회 실패 (Cloud Code/스크립트명 확인)");
        }
    }
}
