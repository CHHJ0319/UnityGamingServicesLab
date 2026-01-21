using System;

public static class GameEvents
{
    public static Action OnLoginSuccess;

    public static void Clear()
    {
        OnLoginSuccess = null;
    }

    public static void Login()
    {
        OnLoginSuccess?.Invoke();
    }
}
