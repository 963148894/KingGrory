using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SpriteSlider : MonoBehaviour {
    [SerializeField]
    private Transform front;

    private float m_value;

    public float Value
    {
        get { return m_value; }
        set {
            m_value = value;
            front.localScale = new Vector3(m_value, 1);
            front.localPosition = new Vector3((1 - m_value) * -0.8f, 0);
        }
    }
}
