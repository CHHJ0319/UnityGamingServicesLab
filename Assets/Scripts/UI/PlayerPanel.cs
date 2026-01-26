using TMPro;
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

    public void AddInvetoryItem(string itemName)
    {
        GameObject newItem = Instantiate(inventoryItemPrefabs, inventory);

        var textMesh = newItem.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = itemName;
        }
    }

}
