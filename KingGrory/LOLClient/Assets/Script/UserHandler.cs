using GameProtocol;
using GameProtocol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


   public class UserHandler:MonoBehaviour,IHandler
    {
        public void MessageReceive(SocketModel model)
        {
            switch (model.command) { 
                case UserProtocol.INFO_SRES:
                    info(model.GetMessage<UserDTO>());
                    break;
                case UserProtocol.CREATE_SRES:
                    create(model.GetMessage<bool>());
                    break;
                case UserProtocol.ONLINE_SRES:
                    online(model.GetMessage<UserDTO>());
                    break;
            }
        }

        private void info(UserDTO user) {
            if (user == null)
            {
                //显示创建面板
                SendMessage("OpenCreate");
            }
            else {
                this.WriteMessage(Protocol.TYPE_USER, 0, UserProtocol.ONLINE_CREQ, null);
            }
        }

        private void online(UserDTO user) {
            GameData.user = user;
            //移除遮罩
            SendMessage("CloseMask");
            //刷新UI数据
            SendMessage("RefreshView");
            WarrningManager.errors.Add(new WarrningModel("登陆成功"));
        }

        private void create(bool value) {
            if (value)
            {
                WarrningManager.errors.Add(new WarrningModel("创建成功成功", delegate {
                    //关闭创建面板
                    SendMessage("CloseCreate");
                    //直接申请登录
                    this.WriteMessage(Protocol.TYPE_USER, 0, UserProtocol.ONLINE_CREQ, null);
                }));
                
            }
            else { 
                //刷新创建面板
                WarrningManager.errors.Add(new WarrningModel("创建失败", delegate
                {
                    SendMessage("OpenCreate");
                }));
               
            }
        }
    }

