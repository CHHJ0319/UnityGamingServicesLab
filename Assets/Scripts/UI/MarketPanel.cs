using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Economy;
using UnityEngine;
using UnityEngine.UI;

public class MarketPanel : MonoBehaviour
{
    public Button refreshButton;

    public GameObject marketItemPrefabs;
    public Transform market;

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
        //await RefreshMarketAsync();
    }

    public void ClearMarket()
    {
        foreach (Transform item in market)
        {
            Destroy(item.gameObject);
        }
    }

}
