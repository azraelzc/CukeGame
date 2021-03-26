local M = {}
M.UIType = {
    BACKGROUND = 1,
    HUD = 2,
    WIN = 3,
    TIP = 4,
    TOP = 5,
}
--[[
    
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
        prefab = "Prefabs/UI/Common/Common_Back",
    }
}

return M