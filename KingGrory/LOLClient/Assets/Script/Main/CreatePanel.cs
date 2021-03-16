using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol;
/// <summary>
/// 召唤师创建面板处理
/// </summary>
public class CreatePanel : MonoBehaviour {

    [SerializeField]
    private InputField nameField;
    [SerializeField]
    public Button btn;

    public void open() {
        btn.enabled = true;
        gameObject.SetActive(true);
    }
    public void close() {
        gameObject.SetActive(false);
    }

    public void click() {
        if (nameField.text.Length > 6 || nameField.text == string.Empty) {
            WarrningManager.errors.Add(new WarrningModel("请输入正确的昵称"));
            return;
        }
        btn.enabled = false;
        this.WriteMessage(Protocol.TYPE_USER, 0, UserProtocol.CREATE_CREQ, nameField.text);
    }
}
