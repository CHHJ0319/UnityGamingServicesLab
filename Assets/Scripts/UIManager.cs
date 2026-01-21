using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerPanel playerPanel;

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
}
