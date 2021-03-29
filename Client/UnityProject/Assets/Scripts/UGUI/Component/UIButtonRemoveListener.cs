using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonRemoveListener : MonoBehaviour {

    Button _button;

    // Start is called before the first frame update
    void Start() {
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnDestroy() {
        if (_button) {
            _button.onClick.RemoveAllListeners();
            _button.onClick.Invoke();
        }
    }
}
