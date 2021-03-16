using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

    public GameObject pre;

    public void click() {
        Instantiate<GameObject>(pre).transform.parent=transform;
    }
}
