using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


   public static class Ex
    {
        /// <summary>
        /// 扩展monobehaviour 发送消息方法
        /// </summary>
        /// <param name="mono"></param>
        /// <param name="type"></param>
        /// <param name="area"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public static void WriteMessage(this MonoBehaviour mono, byte type, int area, int command, object message)
        {
            NetIO.Instance.write(type, area, command, message);
        }
    }

