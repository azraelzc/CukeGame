using System;
using System.Collections.Generic;
using UnityEngine;
using ZCGame.Core.AB;
using UnityEngine.UI;

namespace ZCGame.Manager {
    public class UIManager {
        public enum UILayer {
            BACKGROUND = 1,
            HUD,
            WIN,
            TIP,
            TOP,
        }

        static Camera _UICamera;
        public static Camera UICamera {
            get { return _UICamera; }
        }

        static Dictionary<UILayer,GameObject> contaners = new Dictionary<UILayer, GameObject>();

        public static void Init() {
            int uiLayer = LayerMask.NameToLayer("UI");
            GameObject root = GameObject.Find("Run/UIRoot");
            _UICamera = GameObject.Find("Run/UICamera").GetComponent<Camera>();
            foreach (UILayer e in Enum.GetValues(typeof(UILayer))) {
                GameObject go = new GameObject(e.ToString());
                go.transform.SetParent(root.transform,false);
                RectTransform layerTransform = go.AddComponent<RectTransform>();
                go.layer = LayerMask.NameToLayer("UI");
                layerTransform.anchorMin = Vector2.zero;
                layerTransform.anchorMax = Vector2.one;
                layerTransform.sizeDelta = Vector2.zero;
                layerTransform.localPosition = Vector3.zero;
                var can = go.AddComponent<Canvas>();
                can.overrideSorting = true;
                can.sortingOrder = (int)e * 2000;
                go.AddComponent<GraphicRaycaster>();

                contaners.Add(e, go);
            }
        }

        public static GameObject GetLayer(int layer) {
            return contaners[(UILayer)layer];
        }

        public static int GetLayerSortinOrder(int layer) {
            var go = contaners[(UILayer)layer];
            return go.GetComponent<Canvas>().sortingOrder;
        }
    }
}
