using UnityEngine;
using System.Collections;
using GameProtocol;
using UnityEngine.UI;

public class MainScene : MonoBehaviour {

    [SerializeField]
    private GameObject mask;//防止用户误操作 顶层遮罩

    [SerializeField]
    private CreatePanel createPanel;


    [SerializeField]
    private Text matchText;//匹配按钮文本对象

    #region 数据显示UI组件 刷新调用
    [SerializeField]
    private Text nameText;//召唤师名称 等级显示text
    [SerializeField]
    private Slider expBar;//召唤师经验条

    #endregion

    void Start() {
        if (GameData.user == null) {
            mask.SetActive(true);
            //向服务器申请用户数据
            this.WriteMessage(Protocol.TYPE_USER, 0, UserProtocol.INFO_CREQ, null);
        }
    }

    public void RefreshView() {
        nameText.text = GameData.user.name + "  等级:" + GameData.user.level;
        expBar.value = GameData.user.exp / 100;
    }

    /// <summary>
    /// 打开创建面板
    /// </summary>
    void OpenCreate() {
        createPanel.open();
    }
    /// <summary>
    /// 关闭创建面板
    /// </summary>
    void CloseCreate()
    {
        createPanel.close();
    }

    void CloseMask() {
        mask.SetActive(false);
    }

    public void match() {
        if (matchText.text == "开始排队")
        {
            matchText.text = "取消排队";
            this.WriteMessage(Protocol.TYPE_MATCH, 0, MatchProtocol.ENTER_CREQ, null);
        }
        else {
            matchText.text = "开始排队";
            this.WriteMessage(Protocol.TYPE_MATCH, 0, MatchProtocol.LEAVE_CREQ, null);
        }
    }
}
