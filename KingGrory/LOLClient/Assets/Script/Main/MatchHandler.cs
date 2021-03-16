using UnityEngine;
using System.Collections;
using GameProtocol;

public class MatchHandler : MonoBehaviour,IHandler {

    public void MessageReceive(SocketModel model)
    {
        if (model.command == MatchProtocol.ENTER_SELECT_BRO) {
            Application.LoadLevel(2);
        }
    }
}
