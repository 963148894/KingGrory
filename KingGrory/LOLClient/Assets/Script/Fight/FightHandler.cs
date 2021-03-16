using UnityEngine;
using System.Collections;
using GameProtocol;
using GameProtocol.dto.fight;
using System.Collections.Generic;

public class FightHandler : MonoBehaviour,IHandler {
    FightRoomModel room;
    [SerializeField]
    private Transform[] positions;//队伍1的建筑初始位置表
    [SerializeField]
    private Transform startPosition;//队伍1的复活（出生）点
    [SerializeField]
    private Transform startPosition1;//队伍2的复活（出生）点

    [SerializeField]
    private Transform[] positions1;//队伍2的建筑初始位置表

    private Dictionary<int, GameObject> models = new Dictionary<int, GameObject>();
   // private Dictionary<int, GameObject> teamTwo = new Dictionary<int, GameObject>();

    public void MessageReceive(SocketModel model)
    {
        switch (model.command) { 
            case FightProtocol.START_BRO:
                start(model.GetMessage<FightRoomModel>());
                break;
            case FightProtocol.MOVE_BRO:
                move(model.GetMessage<MoveDTO>());
                break;

        }
    }

    private void move(MoveDTO value) {
        Debug
            .Log("yidong");
        Vector3 target = new Vector3(value.x, value.y, value.z);
        models[value.userId].SendMessage("move",target);
    }

    private void start(FightRoomModel value){
        room = value;

        int myTeam=-1;
        foreach (AbsFightModel item in value.teamOne)
        {
            if (item.id == GameData.user.id) {
                myTeam = item.team;
            }
        }
        if (myTeam == -1) {
            foreach (AbsFightModel item in value.teamTwo)
            {
                if (item.id == GameData.user.id)
                {
                    myTeam = item.team;
                }
            }
        }


        foreach (AbsFightModel item in value.teamOne)
        {
            GameObject go;
            if (item.type == ModelType.HUMAN)
            {
                go = (GameObject)Instantiate(Resources.Load<GameObject>("prefab/Player/" + item.code), startPosition.position + new Vector3(Random.Range(0.5f, 1.5f), 0, Random.Range(0.5f, 1.5f)), startPosition.rotation);
              
                PlayerCon pc = go.GetComponent<PlayerCon>();
                pc.init((FightPlayerModel)item,myTeam);
            }
            else {
                go = (GameObject)Instantiate(Resources.Load<GameObject>("prefab/build/1_" + item.code), positions[item.code - 1].position, positions[item.code - 1].rotation);
            
            }
            
            this.models.Add(item.id, go);
            if (item.id == GameData.user.id) {
                FightScene.instance.initView((FightPlayerModel)item, go);
                FightScene.instance.lookAt();
            }
        }

        foreach (AbsFightModel item in value.teamTwo)
        {
            GameObject go;
            if (item.type == ModelType.HUMAN)
            {
                go = (GameObject)Instantiate(Resources.Load<GameObject>("prefab/Player/" + item.code), startPosition1.position + new Vector3(Random.Range(5, 15), 0, Random.Range(5, 15)), startPosition1.rotation);
                PlayerCon pc = go.GetComponent<PlayerCon>();
                pc.init((FightPlayerModel)item,myTeam);
            }
            else
            {
                go = (GameObject)Instantiate(Resources.Load<GameObject>("prefab/build/2_" + item.code), positions1[item.code - 1].position, positions1[item.code - 1].rotation);
            }
            
            this.models.Add(item.id, go);
            if (item.id == GameData.user.id)
            {
                FightScene.instance.initView((FightPlayerModel)item,go);
                FightScene.instance.lookAt();
            }
        }
    }
}
