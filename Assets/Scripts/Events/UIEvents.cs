using System;
using UnityEngine;

public static class UIEvents
{
    public static Action<int> OnCreditUpdated;
    public static Action OnInventoryLoaded;
    public static Action<string> OnInventoryItemAdded;

    public static void Clear()
    {
        OnCreditUpdated = null;
        OnInventoryLoaded = null;
        OnInventoryItemAdded = null;
    }

    public static void UpdateCredits(int credits)
    {
        OnCreditUpdated?.Invoke(credits);
    }

    public static void ResetInventory()
    {
        OnInventoryLoaded?.Invoke();
    }

    public static void AddInventoryItem(string name)
    {
        OnInventoryItemAdded?.Invoke(name);
    }
}
