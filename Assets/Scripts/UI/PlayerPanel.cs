using TMPro;
using Unity.Services.Economy.Model;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    public TextMeshProUGUI creditsText;

    public GameObject inventoryItemPrefabs; 
    public Transform inventory; 

    public void UpdateCreditsText(int credits)
    {
        creditsText.text = $"{credits} C";
    }

    public void ClearInventory()
    {
        foreach (Transform item in inventory)
        {
            Destroy(item.gameObject);
        }
    }

    public void AddInvetoryItem(PlayersInventoryItem item)
    {
        if (item.InventoryItemId == "ITEM1") return;

        GameObject newItem = Instantiate(inventoryItemPrefabs, inventory);
        newItem.GetComponent<InventoryItem>().Bind(item);
    }

}
