using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AnimatorOver完毕后回调管理
/// </summary>
public class AnimatorFinishTrigger : MonoBehaviour {
    Animator _animator;
    Action _action;
    string _animName;
    string _endName;
    bool isPlaying = false;

    void GetAnimator() {
        if (_animator == null) {
            _animator = GetComponent<Animator>();
        }
    }
    public void Play(string name, string endName, Action action) {
        GetAnimator();
        isPlaying = true;
        _animName = name;
        _endName = endName;
        _action = action;
        _animator.Play(name, 0, 0f);
        _animator.Update(0);
    }

    public void Set(string name, string endName, Action action) {
        GetAnimator();
        isPlaying = true;
        _animName = name;
        _endName = endName;
        _action = action;
        _animator.SetBool(name, true);
        _animator.Update(0);
    }

    //自动进入播放时候注册
    public void OnComplete(string name, string endName, Action action) {
        GetAnimator();
        isPlaying = true;
        _animName = name;
        _endName = endName;
        _action = action;
    }

    Action<string> _eventaction;

    public void SetOnEventAction(Action<string> _eventaction) {
        this._eventaction = _eventaction;
    }

    public void OnEventTrigger(string name) {
        this._eventaction?.Invoke(name);
    }

    void Update() {
        if (isPlaying && _animator != null) {
            AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
            //Debug.Log("==AnimatorFinishTrigger===" + info.normalizedTime + " : " + _animName + " : " + _endName + " : " + info.IsName(_endName));
            if ((info.normalizedTime >= 1f && info.IsName(_endName)) || (_animName.Equals(_endName) && !info.IsName(_endName))) {
                isPlaying = false;
                //_animator.SetBool(_animName, false);
                _action?.Invoke();
                //_animator.Update(info.length-1);
                //Clear();
            }
        }
    }
}

