using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIShadowAlpha : MonoBehaviour
{

    public Color shadowColor = Color.white;

    private int shaderPropertyId = Shader.PropertyToID("_Color");

    private Material mShadowMaterial = null;

    // Start is called before the first frame update
    void Awake()
    {
        var img = this.GetComponent<Image>();
        if(img)
            this.mShadowMaterial = img.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.mShadowMaterial)
        {
            this.mShadowMaterial.SetColor(shaderPropertyId,shadowColor);
        }
    }
}
