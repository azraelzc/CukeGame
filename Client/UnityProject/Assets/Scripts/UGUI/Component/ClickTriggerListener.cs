using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickTriggerListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IEventSystemHandler {

    #region event
    public delegate void PointerEventDelegate(GameObject go, PointerEventData eventData);
    public delegate void BaseEventDelegate(GameObject go, BaseEventData eventData);
    public delegate void AxisEventDelegate(GameObject go, AxisEventData eventData);

    public event PointerEventDelegate onPointerEnter;
    public event PointerEventDelegate onPointerExit;
    public event PointerEventDelegate onPointerDown;
    public event PointerEventDelegate onPointerUp;
    public event PointerEventDelegate onPointerClick;
    public event PointerEventDelegate onInitializePotentialDrag;
    public event PointerEventDelegate onBeginDrag;
    public event PointerEventDelegate onDrag;
    public event PointerEventDelegate onEndDrag;
    public event PointerEventDelegate onDrop;
    public event PointerEventDelegate onScroll;
    public event BaseEventDelegate onUpdateSelected;
    public event BaseEventDelegate onSelect;
    public event BaseEventDelegate onDeselect;
    public event AxisEventDelegate onMove;
    public event BaseEventDelegate onSubmit;
    public event BaseEventDelegate onCancel;
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

    void OnDisable() {
        if (isPointerDown) {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            OnPointerUp(pointer);
        }
       
    }

    private void OnDestroy() {
        onPointerEnter = null;
        onPointerExit = null;
        onPointerDown = null;
        onPointerUp = null;
        onPointerClick = null;
        onInitializePotentialDrag = null;
        onBeginDrag = null;
        onDrag = null;
        onEndDrag = null;
        onDrop = null;
        onScroll = null;
        onUpdateSelected = null;
        onSelect = null;
        onDeselect = null;
        onMove = null;
        onSubmit = null;
        onCancel = null;
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
    public void OnInitializePotentialDrag(PointerEventData eventData) {
        if (onInitializePotentialDrag != null) onInitializePotentialDrag(gameObject, eventData);
    }
    public void OnBeginDrag(PointerEventData eventData) {
        if (onBeginDrag != null) onBeginDrag(gameObject, eventData);
    }
    public void OnDrag(PointerEventData eventData) {
        if (onDrag != null) onDrag(gameObject, eventData);
    }
    public void OnEndDrag(PointerEventData eventData) {
        if (onEndDrag != null) onEndDrag(gameObject, eventData);
    }
    public void OnDrop(PointerEventData eventData) {
        if (onDrop != null) onDrop(gameObject, eventData);
    }
    public void OnScroll(PointerEventData eventData) {
        if (onScroll != null) onScroll(gameObject, eventData);
    }
    public void OnUpdateSelected(BaseEventData eventData) {
        if (onUpdateSelected != null) onUpdateSelected(gameObject, eventData);
    }
    public void OnSelect(BaseEventData eventData) {
        if (onSelect != null) onSelect(gameObject, eventData);
    }
    public void OnDeselect(BaseEventData eventData) {
        if (onDeselect != null) onDeselect(gameObject, eventData);
    }
    public void OnMove(AxisEventData eventData) {
        if (onMove != null) onMove(gameObject, eventData);
    }
    public void OnSubmit(BaseEventData eventData) {
        if (onSubmit != null) onSubmit(gameObject, eventData);
    }
    public void OnCancel(BaseEventData eventData) {
        if (onCancel != null) onCancel(gameObject, eventData);
    }
    #endregion

}