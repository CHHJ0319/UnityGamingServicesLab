using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticationPanel : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public Button signupButton;
    public Button signinButton;

    private void Awake()
    {
        UIManager.SetauthenticationPanel(this);
    }

    public void OnClickSignUp()
    {
        _ = SignUpWithUsernamePasswordAsync();
    }

    public void OnClickSignIn()
    {
        _ = SignInWithUsernamePasswordAsync();
    }

    private async Task SignUpWithUsernamePasswordAsync()
    {
        string username = usernameInput != null ? usernameInput.text : "";
        string password = passwordInput != null ? passwordInput.text : "";

        if (!ValidatePassword(password, out string error))
        {
            UIManager.ShowPopupPanel(error);
            if (signupButton != null) signupButton.interactable = true;
            return;
        }

        try
        {
            if (AuthenticationService.Instance.IsSignedIn)
            {
                UIManager.ShowPopupPanel("이미 로그인됨 (회원가입 전에 SignOut 필요)");
                return;
            }

            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            UIManager.ShowPopupPanel("회원가입 성공. 이제 로그인");
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
            UIManager.ShowPopupPanel("회원가입 실패 (아이디 중복/정책/네트워크 확인)");
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
            UIManager.ShowPopupPanel("요청 실패 (프로젝트/환경/네트워크 확인)");
        }
        finally
        {
            if (signupButton != null) signupButton.interactable = true;
        }
    }

    private async Task SignInWithUsernamePasswordAsync()
    {
        string username = usernameInput != null ? usernameInput.text : "";
        string password = passwordInput != null ? passwordInput.text : "";

        try
        {
            if (AuthenticationService.Instance.IsSignedIn)
            {
                UIManager.ShowPopupPanel("이미 로그인된 상태");
                //if (marketDemo != null) await marketDemo.RefreshAllAsync();
                return;
            }

            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            UIManager.ShowPopupPanel("로그인 성공");

            GameEvents.Login();

            //if (marketDemo != null)
            //{
            //    await marketDemo.RefreshAllAsync();
            //}
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
            UIManager.ShowPopupPanel("로그인 실패 (아이디/비번 확인)");
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
            UIManager.ShowPopupPanel("요청 실패 (프로젝트/환경/네트워크 확인)");
        }
        finally
        {
            if (signinButton != null) signinButton.interactable = true;
        }
    }

    private bool ValidatePassword(string password, out string error)
    {
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(password))
        {
            error = "비밀번호가 비어있음";
            return false;
        }

        if (password.Length < 8 || password.Length > 30)
        {
            error = "비밀번호 길이는 8~30";
            return false;
        }

        bool hasUpper = Regex.IsMatch(password, "[A-Z]");
        bool hasLower = Regex.IsMatch(password, "[a-z]");
        bool hasDigit = Regex.IsMatch(password, "[0-9]");
        bool hasSymbol = Regex.IsMatch(password, @"[^A-Za-z0-9]");

        if (!hasUpper || !hasLower || !hasDigit || !hasSymbol)
        {
            error = "대문자 소문자 숫자 특수문자 최소 1개씩 필요 (예: Abcd1234!)";
            return false;
        }

        return true;
    }
}
