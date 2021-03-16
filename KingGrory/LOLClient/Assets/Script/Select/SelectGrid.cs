using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameProtocol.dto;

public class SelectGrid : MonoBehaviour {
    [SerializeField]
    private Image img;
     [SerializeField]
    private Text nText;
     [SerializeField]
    private Image head;

    public void refresh(SelectModel model) {
        nText.text = model.name;
        //是否进入
        if (model.enter)
        {
            //是否选择英雄
            if (model.hero == -1)
            {
                head.sprite = Resources.Load<Sprite>("HeroIcon/nil");
            }
            else {
                head.sprite = Resources.Load<Sprite>("HeroIcon/" + model.hero);
            }
        }
        else {
           head.sprite= Resources.Load<Sprite>("HeroIcon/notenter");
        }
        //是否已准备
        if (model.ready)
        {
            selected();
        }
        else {
            img.color = Color.white;
        }
    }
    private void selected() {
        img.color = Color.red;
    }
}
