using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 挂载于敌方单位身上
/// </summary>
public class Eye : MonoBehaviour {

    List<GameObject> list = new List<GameObject>();
    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private GameObject root;

    void Update() {
        if (list.Count > 0)
        {
            //是否隐身
            //是否敌方反隐

            if (!head.activeSelf) {
                head.SetActive(true);
            }
            if (!title.activeSelf) {
                title.SetActive(true);
            }
            if (!root.activeSelf) {
                root.SetActive(true);
            }
        }
        else {
            if (head.activeSelf)
            {
                head.SetActive(false);
            }
            if (title.activeSelf)
            {
                title.SetActive(false);
            }
            if (root.activeSelf)
            {
                root.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider c) { 
        PlayerCon con=c.gameObject.GetComponent<PlayerCon>();
        if(con){
            if (con.data.team != GetComponent<PlayerCon>().data.team) {
                list.Add(c.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (list.Contains(c.gameObject)) {
            list.Remove(c.gameObject);
        }
    }
}
