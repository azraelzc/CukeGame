using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NoRaycastTarget : MonoBehaviour {
    /// <summary>
    /// 自动取消RatcastTarget
    /// </summary>
    [MenuItem("GameObject/UI/Image")]
    static void CreatImage() {
        if (Selection.activeTransform) {
            if (Selection.activeTransform.GetComponentInParent<Canvas>()) {
                GameObject go = new GameObject("Image", typeof(Image));
                go.GetComponent<Image>().raycastTarget = false;
                go.transform.SetParent(Selection.activeTransform);
            }
        } else {
            Canvas canvas = CheckHasCanvas();
            if (canvas != null) {
                GameObject go = new GameObject("Image", typeof(Image));
                go.GetComponent<Image>().raycastTarget = false;
                go.transform.SetParent(canvas.transform);
            }
        }
    }
    //重写Create->UI->Text事件  
    [MenuItem("GameObject/UI/Text")]
    static void CreatText() {
        if (Selection.activeTransform) {
            //如果选中的是列表里的Canvas  
            if (Selection.activeTransform.GetComponentInParent<Canvas>()) {
                //新建Text对象  
                GameObject go = new GameObject("Text", typeof(Text));
                //将raycastTarget置为false  
                go.GetComponent<Text>().raycastTarget = false;
                //设置其父物体  
                go.transform.SetParent(Selection.activeTransform);
            }
        } else {
            Canvas canvas = CheckHasCanvas();
            if (canvas != null) {
                GameObject go = new GameObject("Text", typeof(Text));
                go.GetComponent<Text>().raycastTarget = false;
                go.transform.SetParent(canvas.transform);
            }
        }
    }

    //重写Create->UI->Text事件  
    [MenuItem("GameObject/UI/Raw Image")]
    static void CreatRawImage() {
        if (Selection.activeTransform) {
            //如果选中的是列表里的Canvas  
            if (Selection.activeTransform.GetComponentInParent<Canvas>()) {
                //新建Text对象  
                GameObject go = new GameObject("RawImage", typeof(RawImage));
                //将raycastTarget置为false  
                go.GetComponent<RawImage>().raycastTarget = false;
                //设置其父物体  
                go.transform.SetParent(Selection.activeTransform);
            }
        } else {
            Canvas canvas = CheckHasCanvas();
            if (canvas != null) {
                GameObject go = new GameObject("RawImage", typeof(RawImage));
                go.GetComponent<RawImage>().raycastTarget = false;
                go.transform.SetParent(canvas.transform);
            }
        }
    }
    /// <summary>
    /// 兼容没有选择物体时创建
    /// </summary>
    /// <returns></returns>
    static Canvas CheckHasCanvas() {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas == null) {
            GameObject go = new GameObject("Canvas");
            canvas = go.AddComponent<Canvas>();
            go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }
        return canvas;
    }
}

[InitializeOnLoad]
public class Factory {
    static Factory() {
        ObjectFactory.componentWasAdded += ComponentWasAdded;
    }

    private static void ComponentWasAdded(Component component) {
        Image image = component as Image;
        if (image != null)
            image.raycastTarget = false;
        RawImage rawImage = component as RawImage;
        if (rawImage != null)
            rawImage.raycastTarget = false;
        Text text = component as Text;
        if (text != null)
            text.raycastTarget = false;
    }
}