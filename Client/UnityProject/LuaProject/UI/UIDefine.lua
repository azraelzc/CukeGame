local M = {}
M.UIType = {
    BACKGROUND = 1,
    HUD = 2,
    WIN = 3,
    TIP = 4,
    TOP = 5,
}

-- 定义自定义图集加载路径映射
M.UIAtlas =
{
    -- fightAtlas = "ugui/sprites/newbattleatlas/battletips/fightatlas",
    -- battleinfo = "ugui/sprites/newbattleatlas/battleinfo/battleinfo",
    -- NodeAtlas_Chapter1 = "ugui/sprites/nodeatlas/chapter1/nodeatlas_chapter1",
}

--[[
    UI结构
    cache 缓存优先级，越大优先级越高，越不容易被destroy掉
    lua:lua文件地址
    prefab:预制体路径，没有就不加载
    UIType:放在某个层级下
]]
M.UI = {
    Main = {
        name = "Main",
        cache = 9,
        lua = "UI.Main.UIMain",
        prefab = "Prefabs/UI/Main/Main_Panel",
        UIType = M.UIType.WIN
    },
    Bag = {
        name = "Bag",
        cache = 9,
        lua = "UI.Bag.UIBag",
        prefab = "Prefabs/UI/Bag/Bag_Panel",
        UIType = M.UIType.WIN
    },
    Attribute = {
        name = "Attribute",
        cache = 9,
        lua = "UI.Attribute.UIAttribute",
        prefab = "Prefabs/UI/Attribute/Attribute_Panel",
        UIType = M.UIType.WIN
    },
}

M.ChildUI = {
    Back = {
        name = "Back",
        lua = "UI.Common.UIBack",
        prefab = "Prefabs/UI/Common/Back_Child_Panel",
    }
}

-- 把自定义图集路径映射加载到SpriteAtlasManager中
function M:InitSpriteAtlas()
    for k, v in pairs(M.UIAtlas) do
        SpriteAtlasManager.AddSelfAtlasPath_Dics(k, v);
    end
end

return M