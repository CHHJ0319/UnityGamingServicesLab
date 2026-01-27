public static class DataManager
{
    private static EconomyContoroller economyContoroller;
    private static MarketPanel marketPanel;

    public static void SetEconomyContoroller(EconomyContoroller controller)
    {
        economyContoroller = controller;
    }

    public static void SetMarketPanel(MarketPanel market)
    {
        marketPanel = market;
    }

    public static void UpdatePlayerPanel()
    {
        economyContoroller.GetPlayerCredits();
        economyContoroller.GetInventoryItems();
    }

    public static void UpdateMarkerPanel()
    {
        marketPanel.GetMarketItems();
    }
}
