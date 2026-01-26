public static class DataManager
{
    private static EconomyContoroller economyContoroller;

    public static void SetEconomyContoroller(EconomyContoroller controller)
    {
        economyContoroller = controller;
    }

    public static void UpdatePlayerPanel()
    {
        economyContoroller.GetPlayerCredits();
        economyContoroller.GetInventoryItems();
    }
}
