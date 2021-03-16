using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WarrningWindow : MonoBehaviour {

    [SerializeField]
    private Text text;

    WarrningResult result;

    public void active(WarrningModel value) {
        text.text = value.value;
        this.result = value.result;
        if (value.delay > 0) {
            Invoke("close", value.delay);
        }
        gameObject.SetActive(true);
    }

    public void close() {
        if (IsInvoking("close")) CancelInvoke("close");
        gameObject.SetActive(false);
        if (result != null) {
            result();
        }
        
    }
}
