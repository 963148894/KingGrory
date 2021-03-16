using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroGrid : MonoBehaviour {
    [SerializeField]
    private Button btn;
     [SerializeField]
    private Image img;
    private int id = -1;
    public void init(int id) {
        this.id = id;
        img.sprite = Resources.Load<Sprite>("HeroIcon/"+id);
    }

    public void active() {
        btn.interactable = true;
    }
    public void deactive() {
        btn.interactable = false;
    }
    public void click() {
        if (id != -1) { 
        //处理角色选择事件
            SelectEventUtil.selectHero(id);
        }
    }
}
