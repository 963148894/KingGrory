using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SceneProcess : MonoBehaviour,IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.pointerId) { 
            case PointerInputModule.kMouseLeftId:
                //TODO: 左键点击 自行扩展
                break;
            case PointerInputModule.kMouseRightId:
                FightScene.instance.rightClick(eventData.position);
                break;
        }
    }

    void Update() {
        Vector3 position= Input.mousePosition;
        if (position.x < 10)
        {
            //通知相机向左移动
            FightScene.instance.cameraHMove(1);
        }
        else if (position.x > Screen.width - 10)
        {
            //通知相机向右移动
            FightScene.instance.cameraHMove(-1);
        }
        else {
            FightScene.instance.cameraHMove(0);
        }

        if (position.y < 10)
        {
            //通知相机向下移动
            FightScene.instance.cameraVMove(-1);
        }
        else if (position.y > Screen.height - 10)
        {
            //通知相机向上移动
            FightScene.instance.cameraVMove(1);
        }
        else {
            FightScene.instance.cameraVMove(0);
        }
        if (Input.GetKey(KeyCode.Space)) {
            FightScene.instance.lookAt();
        }
    }
}
