using UnityEngine;
using System.Collections;

public class WarFogTest : MonoBehaviour {

    [SerializeField]
    private RenderTexture mask;
    [SerializeField]
    private Material mat;

    public void OnRenderImage(RenderTexture source,RenderTexture des) {
        mat.SetTexture("_MaskTex", mask);
        Graphics.Blit(source, des, mat);
    }
	
}
