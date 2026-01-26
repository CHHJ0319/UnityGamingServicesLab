using TMPro;
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
        UIEvents.OnInventoryItemAdded += AddInvetoryItem;
    }

    private void OnDisable()
    {
        UIEvents.OnCreditUpdated -= UpdateCreditsText;
        UIEvents.OnInventoryLoaded -= ClearInventory;
        UIEvents.OnInventoryItemAdded -= AddInvetoryItem;
    }

    public void UpdateCreditsText(int credits)
    {
        playerPanel.UpdateCreditsText(credits);
    }

    public void ClearInventory()
    {
        playerPanel.ClearInventory();
    }

    public void AddInvetoryItem(string itemName)
    {
        playerPanel.AddInvetoryItem(itemName);
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
