using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class AuthenticationManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public void OnClickSignUp()
    {
        _ = SignUpWithUsernamePasswordAsync(usernameInput.text, passwordInput.text);
    }

    public void OnClickSignIn()
    {
        _ = SignInWithUsernamePasswordAsync(usernameInput.text, passwordInput.text);
    }

    private async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("UGS 회원가입 성공!");

            string playerId = AuthenticationService.Instance.PlayerId;
            Debug.Log($"가입된 플레이어 ID: {playerId}");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogError($"회원가입 실패: {ex.Message}");
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"요청 실패: {ex.Message}");
        }
    }

    private async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");

            GameEvents.Login();
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
}
