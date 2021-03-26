using UnityEngine;
using UnityEngine.UI;
using ZCGame.Manager;

namespace ZCGame.Utils {
    /// <summary>
    /// 为Lua封装的GameObject操作接口
    /// </summary>
    public static class GOUtil {
        /// <summary>
        /// 获取游戏对象上的组件
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public static Component GetComponent(GameObject gameObject, string componentType) {
            Component comp = gameObject.GetComponent(componentType);
            if (comp == null) {
                return null;
            }
            return comp;
        }

        public static Component GetComponent(Component component, string componentType) {
            return GetComponent(component.gameObject, componentType);
        }


        public static Component GetComponentInChildren(GameObject gameObject, string componentType) {
            System.Type type = TypeUtil.GetType(componentType);
            Component comp = gameObject.GetComponentInChildren(type);
            if (comp == null) {
                return null;
            }

            return comp;
        }

        /// <summary>
        /// 查找节点下所有组件
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="parent">查找节点</param>
        /// <returns>返回第一个名字符合的对象，如果找不到返回null</returns>
        public static Component[] GetComponentsInChildren(GameObject gameObject, string componentType) {
            System.Type type = TypeUtil.GetType(componentType);
            Component[] comps = gameObject.GetComponentsInChildren(type);
            if (comps == null) {
                return null;
            }
            return comps;
        }

        /// <summary>
        /// 为游戏对象添加组件
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public static Component AddComponent(GameObject gameObject, string componentType) {
            System.Type type = TypeUtil.GetType(componentType);
            return gameObject.AddComponent(type);
        }

        /// <summary>
        /// 获取游戏对象组件如果不存在则添加该组件
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public static Component AddMissingComponent(GameObject gameObject, string componentType) {
            Component component = GetComponent(gameObject, componentType);

            if (!component) {
                component = AddComponent(gameObject, componentType);
            }
            return component;
        }

        public static Component AddMissingComponent(Component component, string componentType) {
            return AddMissingComponent(component.gameObject, componentType);
        }


        public static void ResetTransform(GameObject go, bool ignoreScale = false) {
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            if (!ignoreScale)
                go.transform.localScale = Vector3.one;
        }

        public static void AttachUIGameObject(GameObject gameObject, GameObject parent) {
            gameObject.transform.SetParent(parent != null ? parent.transform : null, false);
        }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        /// <param name="parent">父节点对象</param>
        public static void AttachGameObject(GameObject gameObject, GameObject parent, bool resetTransform = false) {
            gameObject.transform.SetParent(parent != null ? parent.transform : null, false);
            if (resetTransform) {
                ResetTransform(gameObject);
            }
        }

        public static void AttachGameObjectByCenter(GameObject go, GameObject parent) {
            go.transform.SetParent(parent.transform, false);
            Renderer[] rdrs = go.GetComponentsInChildren<Renderer>();
            Vector3 offset = Vector3.zero;
            foreach (Renderer rdr in rdrs) {
                offset += rdr.bounds.center;
            }
            offset /= rdrs.Length;
            go.transform.position -= offset;
        }

        /// <summary>
        /// 新建一个空游戏对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <returns>返回新建的游戏对象</returns>
        public static GameObject NewGameObject(string name, GameObject parent) {
            GameObject gameObject = new GameObject(name);
            AttachGameObject(gameObject, parent, true);

            return gameObject;
        }

        /// <summary>
        /// 新建一个RectTransform空游戏对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject NewGameObjectUI(string name, GameObject parent) {
            GameObject gameObject = new GameObject(name, new System.Type[] { typeof(RectTransform) });
            AttachGameObject(gameObject, parent, true);

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.offsetMin = Vector2.zero;

            return gameObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObjet"></param>
        /// <param name="avtive"></param>
        public static void SetActive(GameObject gameObject, bool avtive) {
            if (gameObject != null && gameObject.activeSelf != avtive) {
                gameObject.SetActive(avtive);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="avtive"></param>
        public static void SetActive(Component component, bool avtive) {
            SetActive(component.gameObject, avtive);
        }

        public static void OppositeActive(GameObject go) {
            if (go != null) {
                go.SetActive(!go.activeSelf);
            }
        }

        public static void OppositeActive(Component component) {
            OppositeActive(component.gameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsActiveSelf(GameObject gameObject) {
            return gameObject.activeSelf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsActiveInHierarchy(GameObject gameObject) {
            return gameObject.activeInHierarchy;
        }

        /// <summary>
        /// 获取游戏对象的哈希码
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static string GetGameObjectHashCode(GameObject gameObject) {
            if (gameObject != null) {
                return gameObject.GetHashCode().ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static string GetGameObjectPath(GameObject gameObject) {
            if (gameObject == null) {
                return string.Empty;
            }

            Transform curt = gameObject.transform;

            string ret = curt.name + "/";
            while (curt.parent != null) {
                ret = curt.parent.name + "/" + ret;
                curt = curt.parent;
            }

            return ret;
        }

        /// <summary>
        /// 查找指定路径下的节点的组件
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="childPath"></param>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public static Component FindChildComponent(GameObject gameObject, string childPath, string componentType) {
            Transform transform = gameObject.transform.Find(childPath);

            if (transform != null) {
                return transform.GetComponent(componentType);
            }
            return null;
        }

        public static Component FindChildComponent(Component component, string childPath, string componentType) {
            return FindChildComponent(component.gameObject, childPath, componentType);
        }

        /// <summary>
        /// 查找指定路径下的节点
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="childPath"></param>
        /// <returns></returns>
        public static GameObject FindChildGameObject(GameObject gameObject, string childPath) {
            Transform transform = gameObject.transform.Find(childPath);
            if (transform != null) {
                return transform.gameObject;
            }
            return null;
        }

        public static GameObject FindChildGameObject(Component component, string childPath) {
            Transform transform = component.transform.Find(childPath);
            if (transform != null) {
                return transform.gameObject;
            }
            return null;
        }

        /// <summary>
        /// 查找指定名字的子节点对象
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="parent">查找节点</param>
        /// <returns>返回第一个名字符合的对象，如果找不到返回null</returns>
        public static GameObject FindGameObjectByName(string name, GameObject parent) {
            Transform[] children = parent.GetComponentsInChildren<Transform>();

            for (int i = 0; i < children.Length; i++) {
                Transform t = children[i];

                if (name.Equals(t.name))

                    return t.gameObject;
            }
            return null;
        }

        /// <summary>
        /// 全局搜索，慎用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindGameObject(string name) {
            GameObject go = GameObject.Find(name);
            if (go != null)
                return go;
            return null;
        }

        public static GameObject Instantiate(GameObject go) {
            return GameObject.Instantiate<GameObject>(go);
        }

        public static GameObject Instantiate(GameObject go, Transform parent, bool worldPositionStays) {
            return GameObject.Instantiate<GameObject>(go, parent, worldPositionStays);
        }

        public static Transform GetTransformByObject(Object obj) {
            if (obj is GameObject) {
                return (obj as GameObject).transform;
            } else if (obj is Component) {
                return (obj as Component).transform;
            } else if (obj is Component) {
                return (obj as Component).transform;
            } else {
                return null;
            }
        }

        public static RectTransform GetRectTransformByObject(GameObject obj) {
            RectTransform rect = obj.GetComponent<RectTransform>();
            if (rect == null) {
                return null;
            }
            return rect;
        }

        public static int GetChildCount(Object parent) {
            return GetTransformByObject(parent).childCount;
        }

        public static GameObject GetChild(Object parent, int index) {
            return GetTransformByObject(parent).GetChild(index).gameObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        public static void DestroyAllChildren(GameObject gameObject) {
            if (gameObject != null) {
                Transform t = gameObject.transform;

                int childCount = t.childCount;

                for (int i = 0; i < childCount; ++i) {
                    Object.Destroy(t.GetChild(i).gameObject);
                }
            }
        }

        /// <summary>
        /// 设置游戏对象以及子对象层
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="layer"></param>
        public static void SetGameObjectLayer(GameObject gameObject, int layer) {
            Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();

            for (int i = 0; i < transforms.Length; i++) {
                Transform t = transforms[i];

                t.gameObject.layer = layer;
            }
        }


        /// <summary>
        /// 设置游戏对象世界坐标
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void SetTransformPosition(GameObject gameObject, float x, float y, float z) {
            gameObject.transform.position = new Vector3(x, y, z);
        }

        /// <summary>
        /// 获取游戏对象世界坐标
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void GetTransformPosition(GameObject gameObject, out float x, out float y, out float z) {
            Vector3 position = gameObject.transform.position;
            x = position.x;
            y = position.y;
            z = position.z;
        }

        /// <summary>
        /// 设置游戏对象自身坐标
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>

        public static void SetTransformLocalPosition(Component component, float x, float y, float z) {
            component.transform.localPosition = new Vector3(x, y, z);
        }
        public static void SetTransformLocalPosition(GameObject gameObject, float x, float y, float z) {
            gameObject.transform.localPosition = new Vector3(x, y, z);
        }

        public static bool PositionInRectTransform(RectTransform rect, float x, float y) {
            return RectTransformUtility.RectangleContainsScreenPoint(rect, new Vector2(x, y), UIManager.UICamera);
        }

        public static bool PositionInRectTransform(GameObject gameObject, float x, float y) {
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            if (rect != null) {
                return PositionInRectTransform(rect, x, y);
            }
            return false;
        }

        public static Vector2 GetUIPosForCameraMode(Camera sceneCamera, Canvas canvas, Vector3 worldPos) {
            Vector2 uiPos = Vector3.zero;

            if (sceneCamera == null || canvas == null) {
                return uiPos;
            }

            Vector3 screenPos = sceneCamera.WorldToScreenPoint(worldPos);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out uiPos);

            return uiPos;
        }

        public static int GetScreenPosX(GameObject g) {
            //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            //  cvs.transform as RectTransform, Camera.main.WorldToScreenPoint(transform.position),
            //  cvs.worldCamera, out local))
            //{
            //    Debug.Log(local);
            //}

            int x = (int)UIManager.UICamera.WorldToScreenPoint(g.transform.position).x;
            return x;
        }

        public static int GetScreenPosY(GameObject g) {
            int y = (int)UIManager.UICamera.WorldToScreenPoint(g.transform.position).y;
            return y;
        }

        /// <summary>
        /// 获取游戏对象自身坐标
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void GetTransformLocalPosition(GameObject gameObject, out float x, out float y, out float z) {
            Vector3 position = gameObject.transform.localPosition;
            x = position.x;
            y = position.y;
            z = position.z;
        }

        /// <summary>
        /// 设置游戏对象旋转
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void SetTransformRotation(GameObject gameObject, float x, float y, float z) {
            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }

        /// <summary>
        /// 获取游戏对象旋转欧拉角
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void GetTransformRotation(GameObject gameObject, out float x, out float y, out float z) {
            Vector3 euler = gameObject.transform.rotation.eulerAngles;
            x = euler.x;
            y = euler.y;
            z = euler.z;
        }

        public static DG.Tweening.Tweener DORotate(Transform transform, int targetPos) {
            return DG.Tweening.ShortcutExtensions.DORotate(transform, new Vector3(0, 0, targetPos), 0.1f, DG.Tweening.RotateMode.FastBeyond360);
        }

        public static DG.Tweening.Tweener DOScale(Transform transform, float targetPos, float duration) {
            return DG.Tweening.ShortcutExtensions.DOScale(transform, new Vector3(targetPos, 1, 1), duration);
        }

        public static DG.Tweening.Tweener DOScaleAll(Transform transform, float targetPos, float duration) {
            return DG.Tweening.ShortcutExtensions.DOScale(transform, new Vector3(targetPos, targetPos, 1), duration);
        }

        public static DG.Tweening.Tweener DoMoveX(Transform trans, float endValue, float duration) {
            return DG.Tweening.ShortcutExtensions.DOMoveX(trans, endValue, duration);
        }

        public static DG.Tweening.Tweener DoMoveY(Transform trans, float endValue, float duration) {
            return DG.Tweening.ShortcutExtensions.DOMoveY(trans, endValue, duration);
        }

        public static void SetTweenerCallback(DG.Tweening.Tweener tween, DG.Tweening.TweenCallback onComplete) {
            if (tween != null && tween.onComplete == null) {
                tween.onComplete = onComplete;
            }
        }

        /// <summary>
        /// 设置游戏对象自身旋转
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void SetTransformLocalRotation(GameObject gameObject, float x, float y, float z) {
            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }


        /// <summary>
        /// 获取游戏对象自身旋转欧拉角
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void GetTransformLocalRotation(GameObject gameObject, out float x, out float y, out float z) {
            Vector3 euler = gameObject.transform.localRotation.eulerAngles;
            x = euler.x;
            y = euler.y;
            z = euler.z;
        }

        /// <summary>
        /// 设置游戏对象自身缩放
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="scale"></param>
        public static void SetTransformLocalScale(GameObject gameObject, float scale) {
            gameObject.transform.localScale = Vector3.one * scale;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void GetTransformLossyScale(GameObject gameObject, out float x, out float y, out float z) {
            Vector3 scale = gameObject.transform.lossyScale;
            x = scale.x;
            y = scale.y;
            z = scale.z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static void GetTransformLocalScale(GameObject gameObject, out float x, out float y, out float z) {
            Vector3 scale = gameObject.transform.localScale;
            x = scale.x;
            y = scale.y;
            z = scale.z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetRectTransformOffsetMax(GameObject gameObject, float x, float y) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.offsetMax = new Vector2(x, y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetRectTransformOffsetMin(GameObject gameObject, float x, float y) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.offsetMin = new Vector2(x, y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetRectTransformPivot(GameObject gameObject, float x, float y) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.pivot = new Vector2(x, y);
            }
        }

        // 0- center, 1- right up, 2- left up
        public static void SetRectTransformOffsetType(GameObject gameObject, int type, int offx, int offy) {
            RectTransform rectTransform = gameObject.transform as RectTransform;
            if (type == 1) {
                rectTransform.anchorMin = new Vector2(1f, 1);
                rectTransform.anchorMax = new Vector2(1f, 1f);
            } else if (type == 2) {
                rectTransform.anchorMin = new Vector2(0f, 1f);
                rectTransform.anchorMax = new Vector2(0f, 1f);
            } else {
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            }
            rectTransform.anchoredPosition = new Vector2((float)offx, (float)offy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetRectTransformSizeDelta(GameObject gameObject, float x, float y) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.sizeDelta = new Vector2(x, y);
            }
        }

        public static void SetRectTransformSizeDeltaWidth(GameObject gameObject, float x) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.sizeDelta = new Vector2(x, rectTransform.sizeDelta.y);
            }
        }

        public static void SetRectTransformSizeDeltaHeight(GameObject gameObject, float y) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, y);
            }
        }
        public static int GetRectTransformSizeDeltaWidth(GameObject gameObject) {
            RectTransform rectTransform = gameObject.transform as RectTransform;
            return (int)rectTransform.sizeDelta.x;
        }

        public static int GetRectTransformSizeDeltaHeight(GameObject gameObject) {
            RectTransform rectTransform = gameObject.transform as RectTransform;
            return (int)rectTransform.sizeDelta.y;
        }

        public static int GetRectTransformWidth(GameObject go) {
            RectTransform rectTransform = go.transform as RectTransform;
            return (int)rectTransform.rect.width;
        }

        public static int GetRectTransformHeight(GameObject go) {
            RectTransform rectTransform = go.transform as RectTransform;
            return (int)rectTransform.rect.height;
        }

        public static void GetRectTransformAnchoredPosition(GameObject gameObject, out float x, out float y) {
            RectTransform rectTransform = gameObject.transform as RectTransform;
            var p = rectTransform.anchoredPosition;
            x = p.x;
            y = p.y;
        }

        public static void GetRectTransformAnchoredPosition(Component component, out float x, out float y) {
            RectTransform rectTransform = component.transform as RectTransform;
            var p = rectTransform.anchoredPosition;
            x = p.x;
            y = p.y;
        }

        public static void SetRectTransformAnchoredPosition(GameObject gameObject, float x, float y) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.anchoredPosition = new Vector2(x, y);
            }
        }

        public static void SetRectTransformAnchoredPosition3D(GameObject gameObject, float x, float y, float z) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.anchoredPosition3D = new Vector3(x, y, z);
            }
        }

        public static void SetRectTransformAnchoredPosition(Component component, float x, float y) {
            RectTransform rect = component.GetComponent<RectTransform>();
            if (rect != null) {
                rect.anchoredPosition = new Vector2(x, y);
            }
        }

        public static void SetRectTransformAnchoredPosition3D(Component component, float x, float y, float z) {
            RectTransform rect = component.GetComponent<RectTransform>();
            if (rect != null) {
                rect.anchoredPosition3D = new Vector3(x, y, z);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetRectTransformAnchorMax(GameObject gameObject, float x, float y) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.anchorMax = new Vector2(x, y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetRectTransformAnchorMin(GameObject gameObject, float x, float y) {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            if (rectTransform != null) {
                rectTransform.anchorMin = new Vector2(x, y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="sprite"></param>
        public static void SetSpriteRendererSprite(GameObject gameObject, Sprite sprite) {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) {
                spriteRenderer.sprite = sprite;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public static void SetLightColor(GameObject gameObject, float r, float g, float b) {
            Light light = gameObject.GetComponent<Light>();

            if (light != null) {
                light.color = new Color(r, g, b);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="intensity"></param>
        public static void SetLightIntensity(GameObject gameObject, float intensity) {
            Light light = gameObject.GetComponent<Light>();

            if (light != null) {
                light.intensity = intensity;
            }
        }

        public static void SetAsFirstSibling(GameObject gameObject) {
            gameObject.transform.SetAsFirstSibling();
        }

        public static void SetAsFirstSibling(Component component) {
            component.transform.SetAsFirstSibling();
        }

        public static void SetAsLastSibling(GameObject gameObject) {
            gameObject.transform.SetAsLastSibling();
        }

        public static void SetAsLastSibling(Component component) {
            component.transform.SetAsLastSibling();
        }

        public static float ParticleSystemLength(Transform transform) {
            var pts = transform.GetComponentsInChildren<ParticleSystem>();
            float maxDuration = 0f;
            foreach (var p in pts) {
                if (p.emission.enabled) {
                    if (p.main.loop) {
                        return -1f;
                    }
                    float dunration = 0f;
                    if (p.emission.rateOverTimeMultiplier <= 0) {
                        dunration = p.main.startDelayMultiplier + p.main.startLifetimeMultiplier;
                    } else {
                        dunration = p.main.startDelayMultiplier + Mathf.Max(p.main.duration, p.main.startLifetimeMultiplier);
                    }
                    if (dunration > maxDuration) {
                        maxDuration = dunration;
                    }
                }
            }
            return maxDuration;
        }

        public static float ParticleSystemLength(GameObject gameObject) {
            return ParticleSystemLength(gameObject.transform);
        }

        public static void SetLayer(GameObject gameObject,int layer) {
            gameObject.layer = layer;
            Transform tran = gameObject.transform;
            if (tran.childCount == 0) {
                return;
            }
            for (int i = 0; i < tran.childCount; i++) {
                SetLayer(tran.GetChild(i).gameObject, layer);
            }
        }

        public static void SetLayer(Component component, int layer) {
            SetLayer(component.gameObject,layer);
        }

        public static int GetLayer(GameObject gameObject) {
            return gameObject.layer;
        }

        public static int GetLayer(Component component) {
            return GetLayer(component.gameObject);
        }
    }

}

