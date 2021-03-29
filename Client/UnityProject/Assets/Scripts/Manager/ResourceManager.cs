using UnityEngine;
using UnityEngine.U2D;
using ZCGame.Core.AB;
using static ZCGame.Core.AB.ABRequest;
using Object = UnityEngine.Object;

namespace ZCGame.Manager {
    public class ResourceManager : MonoBehaviour {
        public static ResourceManager Instance { get; set; }

        private void Awake() {
            if (Instance == null) {
                SpriteAtlasManager _saM = new SpriteAtlasManager();
                _saM.Bind();
                Instance = this;
            }
        }

        private void Update() {
            
        }

        public static Object LoadSync(string path, System.Type type) {
            path = path.ToLower();
            return AssetBundleManager.LoadSync(path, type);
        }


        public static T LoadSync<T>(string path) where T : Object {
            path = path.ToLower();
            return AssetBundleManager.LoadSync(path, typeof(T)) as T;
        }

        public static Sprite LoadAtlasSprite(string atlasPath, string atlasName, string spriteName) {
            atlasPath = atlasPath.ToLower();
            atlasName = atlasName.ToLower();
            SpriteAtlas sa = AssetBundleManager.Load(string.Concat(atlasPath, atlasName), typeof(SpriteAtlas)) as SpriteAtlas;
            return sa.GetSprite(spriteName);
        }

        public static void Unload(Object obj) {
            AssetBundleManager.Release(obj);
        }

        public static void Unload(string path) {
            path = path.ToLower();
            AssetBundleManager.Release(path);
        }

    }
}
