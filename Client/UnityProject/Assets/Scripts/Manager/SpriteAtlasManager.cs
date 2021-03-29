using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;

public class SpriteAtlasManager {
    private static Dictionary<string, string> SpriteAtlasPath_Dics = new Dictionary<string, string>() {
    };

    public static void AddSelfAtlasPath_Dics(string key, string path) {
        SpriteAtlasPath_Dics[key] = path;
    }


    public void Bind() {
        UnityEngine.U2D.SpriteAtlasManager.atlasRequested += OnAtlasRequested;
    }

    private void OnAtlasRequested(string tag, Action<UnityEngine.U2D.SpriteAtlas> action) {
        string path;
        if (SpriteAtlasPath_Dics.ContainsKey(tag)) {
            path = SpriteAtlasPath_Dics[tag];
        } else {
            path = GetAtlasPath(tag);
        }
        var sa = ZCGame.Manager.ResourceManager.LoadSync<UnityEngine.U2D.SpriteAtlas>(path);
        action(sa);
    }

    private string GetAtlasPath(string tag) {
        string name = tag.ToLower();
        return string.Concat("images/atlas/", name, "/", name);
    }
}
