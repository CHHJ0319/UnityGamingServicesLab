public static class UIManager
{
    private static UIController uIController;
    private static AuthenticationPanel authenticationPanel;

    public static void SetUIController(UIController controller)
    {
        uIController = controller;
    }

    public static void SetauthenticationPanel(AuthenticationPanel panel)
    {
        authenticationPanel = panel;
    }

    public static void ShowPopupPanel(string message)
    {
        if (uIController != null)
        {
            uIController.ShowPopupPanel(message);
        }

    }
}
