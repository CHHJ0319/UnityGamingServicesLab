using System;
using System.Collections.Generic;

[Serializable]
public class CreateListingResult { public string listingId; }

[Serializable]
public class MarketListResult { public ListingDto[] listings; }

[Serializable]
public class ListingDto
{
    public string listingId;
    public string status;
    public string sellerPlayerId;
    public string inventoryItemId;
    public Dictionary<string, object> instanceData;
    public string currencyId;
    public int price;
    public long createdAt;
}

[Serializable]
public class BuyResult { public bool ok; public string newPlayersInventoryItemId; }

[Serializable]
public class CancelResult { public bool ok; public string returnedPlayersInventoryItemId; }

[Serializable]
public class ClaimResult { public bool ok; public int claimed; }
