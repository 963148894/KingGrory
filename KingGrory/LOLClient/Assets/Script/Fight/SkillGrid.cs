using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol.dto.fight;
using UnityEngine.EventSystems;

public class SkillGrid : MonoBehaviour {
    [SerializeField]
    private Image mask;
    [SerializeField]
    public Image background;

    private FightSkill skill;

    private bool sclied = false;

    private float maxTime = 0;
    private float nowTime = 0;
    [SerializeField]
    private Button levelUpBtn;

    public void init(FightSkill skill) {
        this.skill = skill;
        Sprite sp = Resources.Load<Sprite>("SkillIcon/"+skill.code);
        background.sprite = sp;
        mask.gameObject.SetActive(true);
    }
    void Update() {
        if (sclied) {
            nowTime -= Time.deltaTime;
            if(nowTime<=0){
                nowTime = 0;
                sclied = false;
                mask.gameObject.SetActive(false);
            }
            mask.fillAmount = nowTime / maxTime;
        }
    }
    /// <summary>
    /// 进行冷却
    /// </summary>
    /// <param name="time"></param>
    public void setMask(int time) {
        //如果为0 说明要取消冷却遮罩
        if (time == 0) {
            //判断 是否满足强制取消遮罩条件 当前不是冷却状态 并且技能等级大于0 则允许取消
            if (!sclied && skill.level > 0)
            {
                mask.gameObject.SetActive(false);
                return;
            }
            else {
                mask.gameObject.SetActive(true);
                return;
            }
        }
        //设置技能冷却时长
        maxTime = time;
        nowTime = time;
        mask.gameObject.SetActive(true);
        sclied = true;
    }
    /// <summary>
    /// 获取焦点
    /// </summary>
    public void pointerEnter() { 
        //显示tip
    }
    /// <summary>
    /// 失去焦点
    /// </summary>
    public void pointerExit()
    {
        //关闭TIP显示
    }

    public void setBtnState(bool state) {
        levelUpBtn.interactable = state;
    }

    public void levelUp() { 
        //向服务器发送消息 申请升级技能
    }
}
