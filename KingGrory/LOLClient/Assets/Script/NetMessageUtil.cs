using UnityEngine;
using System.Collections;
using GameProtocol;

public class NetMessageUtil : MonoBehaviour {

    IHandler login;
    IHandler user;
    IHandler match;
    IHandler select;
    IHandler fight;
	// Use this for initialization
	void Start () {
        login = GetComponent<LoginHandler>();
        user = GetComponent<UserHandler>();
        match = GetComponent<MatchHandler>();
        select = GetComponent<SelectHandler>();
        fight = GetComponent<FightHandler>();
    //    InvokeRepeating("checkMessage", 1f / 60, 1f / 60);
	}
	
	void Update () {
        while (NetIO.Instance.messages.Count > 0)
        {
            SocketModel model = NetIO.Instance.messages[0];
            StartCoroutine("MessageReceive", model);
            NetIO.Instance.messages.RemoveAt(0);
        }	    
	}

    void MessageReceive(SocketModel model) {
        switch (model.type) { 
            case Protocol.TYPE_LOGIN:
                login.MessageReceive(model);
                break;
            case Protocol.TYPE_USER:
                user.MessageReceive(model);
                break;
            case Protocol.TYPE_MATCH:
                match.MessageReceive(model);
                break;
            case Protocol.TYPE_SELECT:
                select.MessageReceive(model);
                break;
            case Protocol.TYPE_FIGHT:
                fight.MessageReceive(model);
                break;
        }
    }
}
