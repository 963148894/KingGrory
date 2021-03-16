using GameProtocol.dto.fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


   public class PlayerCon:MonoBehaviour
    {
       [HideInInspector]
       public FightPlayerModel data;

       protected Animator anim;

       private UnityEngine.AI.NavMeshAgent agent;
       [SerializeField]
       private GameObject mask;//战争迷雾剔除面板
       [SerializeField]
       private PlayerTitile title;//角色头顶信息
       [SerializeField]
       private  MeshRenderer head;

       private int state = AnimState.IDLE;

       void Start() {
           anim = GetComponent<Animator>();
           agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
       }
       /// <summary>
       /// 攻击动画播放结束回调方法
       /// </summary>
       public void attacked() { 
       
       }
       /// <summary>
       /// 移动
       /// </summary>
       /// <param name="target"></param>
       public void move(Vector3 target) {
           agent.ResetPath();
           agent.SetDestination(target);
           anim.SetInteger("state", AnimState.RUN);
           state = AnimState.RUN;
       }

       void Update() {
           switch (state) { 
               case AnimState.RUN:
                   if (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance <= 0 && !agent.pathPending)
                   {
                       state = AnimState.IDLE;
                       anim.SetInteger("state", AnimState.IDLE);
                   }
                   else {
                       if (agent.isOnOffMeshLink) {
                           agent.CompleteOffMeshLink();
                       }
                   }

                   break;
           }
       }
       /// <summary>
       /// 申请攻击，攻击的目标
       /// </summary>
       /// <param name="target"></param>
       public void attack(Transform[] target) { 
            
       }
       /// <summary>
       /// 申请释放技能
       /// </summary>
       /// <param name="code"></param>
       /// <param name="target"></param>
       public void skill(int code, Transform[] target)
       { 
       
       }
       /// <summary>
       /// 设置是否拥有迷雾剔除
       /// </summary>
       /// <param name="state"></param>
       private void MaskState(bool state) {
           mask.SetActive(state);
       }

       public void init(FightPlayerModel data,int myTeam) {
           Debug.Log(data.name);
           this.data = data;
           title.init(data, data.team == myTeam);
           head.material.SetTexture("_MainTex",Resources.Load<Texture>("HeroIcon/"+data.code));
           //如果不是己方单位 移除遮罩
           //是友方单位 移除战争视野处理
           if (data.team != myTeam)
           {
               gameObject.layer = LayerMask.NameToLayer("enemy");
               mask.SetActive(false);
           }
           else {
               gameObject.layer = LayerMask.NameToLayer("friend");
               Destroy(GetComponent<Eye>());
           }
       }
    }

