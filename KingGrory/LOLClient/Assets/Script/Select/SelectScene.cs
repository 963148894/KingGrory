using GameProtocol;
using GameProtocol.dto;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScene : MonoBehaviour
{
    [SerializeField]
    private GameObject heroBtn;//英雄头像预设
    [SerializeField]
    private Transform listParent;//英雄头像列表父容器
    [SerializeField]
    private GameObject initMask;//防止误操作遮罩
    [SerializeField]
    private SelectGrid[] left;//左边列表
    [SerializeField]
    private SelectGrid[] right;//右边列表
    [SerializeField]
    private Button ready;

    [SerializeField]
    private InputField talkInput;//聊天输入框
    [SerializeField]
    private Text talkMessageShow;//聊天信息显示框
    [SerializeField]
    private Scrollbar talkScroll;//聊天滚动条


    private Dictionary<int, HeroGrid> gridMap = new Dictionary<int, HeroGrid>();
    void Start()
    {
        SelectEventUtil.selected = selected;
        SelectEventUtil.refreshView = refreshView;
        SelectEventUtil.selectHero = SelectHero;
        //显示遮罩防止误操作
        initMask.SetActive(true);
        //初始化英雄列表
        initHeroList();
        //通知进入场景并加载完成
        this.WriteMessage(Protocol.TYPE_SELECT, 0, SelectProtocol.ENTER_CREQ, null);
    }

    public void closeMask()
    {
        initMask.SetActive(false);
    }

    private void initHeroList()
    {
        if (GameData.user == null)
        {
            return;
        }

        int index = 0;
        foreach (int item in GameData.user.heroList)
        {
            //创建英雄头像并添加进选择列表
            GameObject btn = Instantiate<GameObject>(heroBtn);
            HeroGrid grid = btn.GetComponent<HeroGrid>();
            grid.init(item);
            btn.transform.parent = listParent;
            btn.transform.localScale = Vector3.one;
            btn.transform.localPosition = new Vector3(48 - 262 + 72 * (index % 7), -42 + 150 + index / 7 * -72);
            gridMap.Add(item, grid);
            index++;
        }
    }

    void refreshView(SelectRoomDTO room)
    {
        int team = room.GetTeam(GameData.user.id);
        //如果自身在队伍一  则队伍一显示在左边 否则队伍二显示在左边
        if (team == 1)
        {
            for (int i = 0; i < room.teamOne.Length; i++)
            {
                left[i].refresh(room.teamOne[i]);
            }
            for (int i = 0; i < room.teamTwo.Length; i++)
            {
                right[i].refresh(room.teamTwo[i]);
            }
        }
        else
        {
            for (int i = 0; i < room.teamOne.Length; i++)
            {
                right[i].refresh(room.teamOne[i]);
            }
            for (int i = 0; i < room.teamTwo.Length; i++)
            {
                left[i].refresh(room.teamTwo[i]);
            }
        }
        refreshHeroList(room);
    }

    private void refreshHeroList(SelectRoomDTO room)
    {
        int team = room.GetTeam(GameData.user.id);
        List<int> selected = new List<int>();
        //获取自己所在的队伍 已经选择了的英雄列表
        if (team == 1)
        {
            foreach (SelectModel item in room.teamOne)
            {
                if (item.hero != -1)
                {
                    selected.Add(item.hero);
                }
            }
        }
        else
        {
            foreach (SelectModel item in room.teamTwo)
            {
                if (item.hero != -1)
                {
                    selected.Add(item.hero);
                }
            }
        }
        //将已选英雄从选择菜单中设置状态为不可选状态
        foreach (int item in gridMap.Keys)
        {
            if (selected.Contains(item) || !ready.interactable)
            {
                gridMap[item].deactive();
            }
            else
            {
                gridMap[item].active();
            }
        }
    }

    public void selected()
    {
        ready.interactable = false;
    }

    public void talkShow(string value)
    {
        talkMessageShow.text += "\n" + value;
        talkScroll.value = 0;
    }

    public void sendClick()
    {
        if (talkInput.text != string.Empty)
        {
            this.WriteMessage(Protocol.TYPE_SELECT, 0, SelectProtocol.TALK_CREQ, talkInput.text);
            talkInput.text = string.Empty;
        }
    }

    public void SelectHero(int id)
    {
        this.WriteMessage(Protocol.TYPE_SELECT, 0, SelectProtocol.SELECT_CREQ, id);
    }

    public void readyClick()
    {
        this.WriteMessage(Protocol.TYPE_SELECT, 0, SelectProtocol.READY_CREQ, null);
    }
}
