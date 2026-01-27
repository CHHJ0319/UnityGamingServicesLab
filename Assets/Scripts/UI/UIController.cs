using TMPro;
using Unity.Services.Economy.Model;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public PlayerPanel playerPanel;
    public PopuoPanel popupPanel;


    private void Awake()
    {
        UIManager.SetUIController(this);
    }

    private void OnEnable()
    {
        UIEvents.OnCreditUpdated += UpdateCreditsText;
        UIEvents.OnInventoryLoaded += ClearInventory;
    }

    private void OnDisable()
    {
        UIEvents.OnCreditUpdated -= UpdateCreditsText;
        UIEvents.OnInventoryLoaded -= ClearInventory;
    }

    public void UpdateCreditsText(int credits)
    {
        playerPanel.UpdateCreditsText(credits);
    }

    public void ClearInventory()
    {
        playerPanel.ClearInventory();
    }

    public void AddInvetoryItem(PlayersInventoryItem item)
    {
        playerPanel.AddInvetoryItem(item);
    }

    public void ShowPopupPanel(string message)
    {
        popupPanel.gameObject.SetActive(true);

        var textElement = popupPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (textElement != null)
        {
            textElement.text = message;
        }
        else
        {
            Debug.LogError("첫 번째 자식 오브젝트에 TMP 컴포넌트가 없습니다!");
        }
    }
}
