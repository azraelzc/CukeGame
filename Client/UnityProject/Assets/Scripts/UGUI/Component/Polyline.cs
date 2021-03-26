using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode, RequireComponent(typeof(RectTransform))]
public class Polyline : MonoBehaviour
{
    public Polyline_Type LineType;
    public float LineWidth = 20;
    public bool FlipX = false;
    public bool FlipY = false;
    public float Rotation = 0;
    public Color LineColor = Color.white;

    public enum Polyline_Type
    { 
        Horizontal,
        Vertical,
        H_V,
        H_V_H,
        H_V_H2,
    }

#if UNITY_EDITOR
    private RectTransform _rect;
    private Polyline_Type _ori_LineType;
    private float _ori_LineWidth;
    private Color _ori_LineColor;
    private Vector2 _ori_Size;

    private RectTransform[] _lineRects;

    // Start is called before the first frame update
    void Start()
    {
        this._rect = this.GetComponent<RectTransform>();
        _lineRects = new RectTransform[3];
        for (int i = 0; i < this.transform.childCount; i++)
        { 
            _lineRects[i] = this.transform.GetChild(i).GetComponent<RectTransform>();
        }

        _ori_LineType = LineType;
        _ori_LineWidth = LineWidth;
        _ori_LineColor = LineColor;
        _ori_Size = this._rect.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (_ori_LineType != LineType)
        {
            this.ShowLines();
            this.SetLinesPos();
            _ori_LineType = LineType;
        }
        if (_ori_LineWidth != LineWidth)
        {
            this.SetLinesPos();
            _ori_LineWidth = LineWidth;
        }
        if (_ori_Size != this._rect.sizeDelta)
        {
            this.SetLinesPos();
            _ori_Size = this._rect.sizeDelta;
        }
        if (_ori_LineColor != LineColor)
        {
            this.SetLinesColor();
            _ori_LineColor = LineColor;
        }

        this._rect.localScale = new Vector3(FlipX ? -1 : 1, FlipY ? -1 : 1, 0);
        this._rect.localRotation = Quaternion.Euler(0, 0, Rotation);
    }

    private void ShowLines()
    {
        for (int i = 0; i < _lineRects.Length; i++)
        {
            _lineRects[i].gameObject.SetActive(false);
        }
        switch (LineType)
        {
            case Polyline_Type.Horizontal:
            case Polyline_Type.Vertical:
                _lineRects[0].gameObject.SetActive(true);
                break;
            case Polyline_Type.H_V:
                _lineRects[0].gameObject.SetActive(true);
                _lineRects[1].gameObject.SetActive(true);
                break;
            case Polyline_Type.H_V_H:
            case Polyline_Type.H_V_H2:
                _lineRects[0].gameObject.SetActive(true);
                _lineRects[1].gameObject.SetActive(true);
                _lineRects[2].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void SetLinesPos()
    {
        switch (LineType)
        {
            case Polyline_Type.Horizontal:
                _lineRects[0].sizeDelta = new Vector2(this._rect.sizeDelta.x, LineWidth);
                _lineRects[0].anchoredPosition = Vector2.zero;
                break;
            case Polyline_Type.Vertical:
                _lineRects[0].sizeDelta = new Vector2(LineWidth, this._rect.sizeDelta.y);
                _lineRects[0].anchoredPosition = Vector2.zero;
                break;
            case Polyline_Type.H_V:
                _lineRects[0].sizeDelta = new Vector2(this._rect.sizeDelta.x, LineWidth);
                _lineRects[0].anchoredPosition = new Vector2(0, (this._rect.sizeDelta.y - LineWidth) / 2);

                _lineRects[1].sizeDelta = new Vector2(LineWidth, this._rect.sizeDelta.y - LineWidth);
                _lineRects[1].anchoredPosition = new Vector2((this._rect.sizeDelta.x - LineWidth) / 2, -LineWidth / 2);
                break;
            case Polyline_Type.H_V_H:
                float p = 0.5f;
                _lineRects[0].sizeDelta = new Vector2(this._rect.sizeDelta.x * p + LineWidth / 2, LineWidth);
                _lineRects[0].anchoredPosition = new Vector2(_lineRects[0].sizeDelta.x / 2 - this._rect.sizeDelta.x / 2, (this._rect.sizeDelta.y - LineWidth) / 2);

                _lineRects[1].sizeDelta = new Vector2(LineWidth, this._rect.sizeDelta.y - LineWidth * 2);
                _lineRects[1].anchoredPosition = new Vector2(this._rect.sizeDelta.x * p - this._rect.sizeDelta.x / 2, 0);

                _lineRects[2].sizeDelta = new Vector2(this._rect.sizeDelta.x * (1 - p) + LineWidth / 2, LineWidth);
                _lineRects[2].anchoredPosition = new Vector2(this._rect.sizeDelta.x / 2 - _lineRects[2].sizeDelta.x / 2, -(this._rect.sizeDelta.y - LineWidth) / 2);
                break;
            case Polyline_Type.H_V_H2:
                _lineRects[0].sizeDelta = new Vector2(this._rect.sizeDelta.x, LineWidth);
                _lineRects[0].anchoredPosition = new Vector2(0, (this._rect.sizeDelta.y - LineWidth) / 2);

                _lineRects[1].sizeDelta = new Vector2(LineWidth, this._rect.sizeDelta.y - LineWidth * 2);
                _lineRects[1].anchoredPosition = new Vector2((this._rect.sizeDelta.x - LineWidth) / 2, 0);

                _lineRects[2].sizeDelta = new Vector2(this._rect.sizeDelta.x, LineWidth);
                _lineRects[2].anchoredPosition = new Vector2(0, -(this._rect.sizeDelta.y - LineWidth) / 2);
                break;
            default:
                break;
        }
    }

    private void SetLinesColor()
    {
        for (int i = 0; i < _lineRects.Length; i++)
        {
            _lineRects[i].GetComponent<Image>().color = LineColor;
        }
    }
#endif
}
