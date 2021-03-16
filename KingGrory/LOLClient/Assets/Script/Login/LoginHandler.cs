using UnityEngine;
using System.Collections;
using GameProtocol;

public class LoginHandler : MonoBehaviour ,IHandler{

    public void MessageReceive(SocketModel model)
    {

        
        switch (model.command) { 
            case LoginProtocol.LOGIN_SRES:
                login(model.GetMessage<int>());
                break;         
            case LoginProtocol.REG_SRES:
                reg(model.GetMessage<int>());
                break;
        }
    }
    /// <summary>
    /// 登陆返回处理
    /// </summary>
    private void login(int value) {
        SendMessage("openLoginBtn");
        switch(value){
            case 0:
               // WarrningManager.errors.Add(new WarrningModel(""));
                //加载游戏主场景
                Application.LoadLevel(1);
                break;
            case -1:
                WarrningManager.errors.Add(new WarrningModel("帐号不存在"));
                break;
            case -2:
                WarrningManager.errors.Add(new WarrningModel("帐号在线"));
                break;
            case -3:
                WarrningManager.errors.Add(new WarrningModel("密码错误"));
                break;
            case -4:
                WarrningManager.errors.Add(new WarrningModel("输入不合法"));
                break;
        }

    }
    /// <summary>
    /// 注册返回处理
    /// </summary>
    private void reg(int value)
    {
        switch (value)
        {
            case 0:
                 WarrningManager.errors.Add(new WarrningModel("注册成功"));
                //加载游戏主场景
                break;
            case 1:
                WarrningManager.errors.Add(new WarrningModel("注册失败，帐号重复"));
                break;

        }
    }
}
