using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol;
using GameProtocol.dto;

public class LoginScreen : MonoBehaviour
{

    #region 登陆面板部分
    [SerializeField]
    private InputField accountInput;

    [SerializeField]
    private InputField passwordInput;
    #endregion

    [SerializeField]
    private Button loginBtn;

    [SerializeField]
    private GameObject regPanel;

    #region 注册面板部分
    [SerializeField]
    private InputField regAccountInput;

    [SerializeField]
    private InputField regpwInput;

    [SerializeField]
    private InputField regpw1Input;
    #endregion
    public void loginOnClick() {
        if (accountInput.text.Length == 0 || accountInput.text.Length > 6) {
            WarrningManager.errors.Add(new WarrningModel("账号不合法"));
            Debug.Log("账号不合法");
            return;
        }
        if (passwordInput.text.Length == 0 || passwordInput.text.Length > 6)
        {
            Debug.Log("密码不合法");
            return;
        }
        //验证通过 申请登陆
       // loginBtn.enabled = false; //loginBtn.gameObject.SetActive(false);
        AccountInfoDTO dto = new AccountInfoDTO();
        dto.account = accountInput.text;
        dto.password = passwordInput.text;

        this.WriteMessage(Protocol.TYPE_LOGIN, 0, LoginProtocol.LOGIN_CREQ, dto);
        loginBtn.interactable = false;
    }

    public void openLoginBtn() {
        passwordInput.text = string.Empty;
        loginBtn.interactable = true;
    }

    public void regClick() {
        regPanel.SetActive(true);
    }

    public void regCloseClick() {
        regAccountInput.text = string.Empty;
        regpwInput.text = string.Empty;
        regpw1Input.text = string.Empty;
        regPanel.SetActive(false);
    }

    public void regpanelregClick() {
        if (regAccountInput.text.Length == 0 || regAccountInput.text.Length > 6)
        {
            Debug.Log("账号不合法");
            return;
        }
        if (regpwInput.text.Length == 0 || regpwInput.text.Length > 6)
        {
            Debug.Log("密码不合法");
            return;
        }
        if (!regpwInput.text.Equals(regpw1Input.text))
        {
            Debug.Log("两次输入密码不一致");
            return;
        }
        //验证通过 申请注册 并关闭注册面板
        AccountInfoDTO dto = new AccountInfoDTO();
        dto.account = regAccountInput.text;
        dto.password = regpwInput.text;
        this.WriteMessage(Protocol.TYPE_LOGIN, 0, LoginProtocol.REG_CREQ, dto);
        regCloseClick();
    }
}
