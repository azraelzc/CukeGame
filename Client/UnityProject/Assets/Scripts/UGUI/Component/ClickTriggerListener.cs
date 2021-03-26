using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickTriggerListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {

    #region event
    public delegate void PointerEventDelegate(GameObject go, PointerEventData eventData);

    public event PointerEventDelegate onPointerEnter;
    public event PointerEventDelegate onPointerExit;
    public event PointerEventDelegate onPointerDown;
    public event PointerEventDelegate onPointerUp;
    public event PointerEventDelegate onPointerClick;
    public Action onLongPress; //长时间按触发一次
    public Action onAlwaysPress;  //长时间按间隔n秒就触发一次

    public float durationPress = 1.0f;      //长时间按超过时间触发一次
    public float perDurationPress = 9999;   //长时间按多次触发间隔
    private bool isPointerDown = false;
    private bool longPressTriggered = false;
    private float timePressStarted;

    float alwaysPressTime = 0;

    private bool IsCanLongPress = true;

    #endregion


    public void SetIsCanLongPress(bool flag) {
        IsCanLongPress = flag;
    }

    public static ClickTriggerListener GetListener(Component c) {
        return GetListener(c.gameObject);
    }

    public static ClickTriggerListener GetListener(GameObject go) {
        ClickTriggerListener listener = go.GetComponent<ClickTriggerListener>();
        if (listener == null)
            listener = go.AddComponent<ClickTriggerListener>();
        return listener;
    }

    private void Update() {
        if (!IsCanLongPress) return;

        if (isPointerDown) {
            if (!longPressTriggered) {
                if (Time.time - timePressStarted > durationPress) {
                    longPressTriggered = true;
                    onLongPress?.Invoke();
                }
            }
            alwaysPressTime += Time.deltaTime;
            if (alwaysPressTime >= perDurationPress) {
                alwaysPressTime = 0;
                onAlwaysPress?.Invoke();
            }
        }
    }

    private void OnDisable() {
        if (isPointerDown) {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            OnPointerUp(pointer);
        }
    }

    #region 方法  
    public void OnPointerEnter(PointerEventData eventData) {
        onPointerEnter?.Invoke(gameObject, eventData);
    }
    public void OnPointerExit(PointerEventData eventData) {
        onPointerExit?.Invoke(gameObject, eventData);
        longPressTriggered = true;
    }
    public void OnPointerDown(PointerEventData eventData) {
        onPointerDown?.Invoke(gameObject, eventData);
        alwaysPressTime = perDurationPress;
        timePressStarted = Time.time;
        isPointerDown = true;
        longPressTriggered = false;
    }
    public void OnPointerUp(PointerEventData eventData) {
        onPointerUp?.Invoke(gameObject, eventData);
        isPointerDown = false;
    }
    public void OnPointerClick(PointerEventData eventData) {

        if (onPointerClick != null && !longPressTriggered) {
            onPointerClick(gameObject, eventData);
            //Debug.Log("执行到OnPointerClick");
        }

    }
    #endregion

}