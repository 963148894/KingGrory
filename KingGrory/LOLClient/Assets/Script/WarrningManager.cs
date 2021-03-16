using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarrningManager : MonoBehaviour {

    public static List<WarrningModel> errors = new List<WarrningModel>();

    [SerializeField]
    private WarrningWindow window;
	void Update () {
        if (errors.Count > 0) {
            WarrningModel err = errors[0];
            errors.RemoveAt(0);
            window.active(err);
        }
        
	}
}
