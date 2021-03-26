
using UnityEngine;
using ZCGame.Manager;

namespace ZCGame {
    public class RunGame: MonoBehaviour {

        // Start is called before the first frame update
        void Start() {
            Init();
        }

        void Init() {
            Application.targetFrameRate = 60;

            UIManager.Init();
            LuaManager.Instance.Init();
            LuaManager.Instance.Start();

            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update() {
            LuaManager.Instance.Update(Time.deltaTime);
        }

        void FixedUpdate() {
            LuaManager.Instance.FixedUpdate(Time.fixedDeltaTime);
        }

        void OnDestroy() {
            
        }
    }
}
    
