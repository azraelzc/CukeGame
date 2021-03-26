using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FillRectSize : MonoBehaviour
{
    public bool Aspect = false;
    public float AspectRatio = 2.03f;

    private RectTransform _parent;
    private RectTransform _rect;
    // Start is called before the first frame update
    void Start()
    {
        _rect = this.transform.GetComponent<RectTransform>();
        _parent = this.transform.parent.GetComponent<RectTransform>();
        this.SetSize();
    }

    void Update()
    {
        if (_parent.hasChanged)
        {
            this.SetSize();
        }
    }

    private void SetSize()
    {
        if (_parent == null)
            return;
        Vector2 size = new Vector2(_parent.rect.width, _parent.rect.height);

        if (Aspect)
        {
            float curRatio = size.x / size.y;
            if (curRatio > AspectRatio)
            {
                size.y = size.x / AspectRatio;
            }
            else
            {
                size.x = size.y * AspectRatio;
            }
        }
        this._rect.sizeDelta = size;
    }
}
