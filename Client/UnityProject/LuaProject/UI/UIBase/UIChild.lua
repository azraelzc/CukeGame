local UIChild = class("UIChild")

function UIChild:Back()
    for i = 1, #self.panelChildren do
        self.panelChildren[i]:Back()
    end
end

function UIChild:Init(UI, prefab, parent)
    Logger.Info("[ChildUI Init]" .. UI.name .. ", parent:" .. parent.UI.name)
    self.parent = parent
    self.UI = UI
    self.prefab = prefab
    self.panelChildren = { }

    -- 红点
    self.redTips = { }
    local redUIs = RedTipsMgr.UIRedTips[self.UI.name]
    if redUIs then
        for i = 1, #redUIs do
            local redUI = redUIs[i]
            local redTip = { }
            redTip.obj = ResourceMgr.LoadSyncPrefab("UGUI/Prefabs/Common/Widget_RedTips")
            redTip.redUI = redUI
            local root = GOUtil.FindChildGameObject(self.prefab, redUI.root)
            GOUtil.AttachGameObject(redTip.obj, root)
            table.insert(self.redTips, redTip)
        end
    end
end

function UIChild:Show()
    GOUtil.SetActive(self.prefab, true)
end

function UIChild:Hide()
    GOUtil.SetActive(self.prefab, false)
end

function UIChild:IsActive()
    return self.prefab ~= nil and GOUtil.IsActiveInHierarchy(self.prefab)
end


function UIChild:Update(deltaTime)
    for i = 1, #self.panelChildren do
        self.panelChildren[i]:Update(deltaTime)
    end
end

function UIChild:Destroy()
    Logger.Info("[ChildUI Destroy]" .. self.UI.name .. ", parent:" .. self.parent.UI.name)
    self:RemoveAllChild()
    for i = 1, #self.redTips do
        ResourceMgr.Unload(self.redTips[i].obj)
    end
    ResourceMgr.Unload(self.prefab)
    self.prefab = nil
end

function UIChild:AddChild(UI)
    local obj = ResourceMgr.LoadSyncPrefab(UI.prefab)
    obj.name = UI.name
    local child = require(UI.lua)
    child:Init(UI, obj, self)
    table.insert(self.panelChildren, child)
    return child
end

function UIChild:RemoveAllChild()
    for i = 1, #self.panelChildren do
        self.panelChildren[i]:Destroy()
    end
    self.panelChildren = { }
end

function UIChild:SetParent(root)
    if self.prefab then
        GOUtil.AttachGameObject(self.prefab, root, false)
    end
end

function UIChild:RefreshRedTips()
    for i = 1, #self.redTips do
        local redTip = self.redTips[i]
        GOUtil.SetActive(redTip.obj, RedTipsMgr.GetRedTipNew(redTip.redUI.type))
    end
end

function UIChild:Register()
    for i = 1, #self.redTips do
        local redTip = self.redTips[i]
        EventManager.Add(redTip.redUI.type, callback(self.RefreshRedTips, self))
    end
    for i = 1, #self.panelChildren do
        self.panelChildren[i]:Register()
    end
end

function UIChild:Unregister()
    for i = 1, #self.redTips do
        local redTip = self.redTips[i]
        EventManager.Remove(redTip.redUI.type, callback(self.RefreshRedTips, self))
    end
    for i = 1, #self.panelChildren do
        self.panelChildren[i]:Unregister()
    end
end

function UIChild:ClearUICacheData()
    for i = 1, #self.panelChildren do
        self.panelChildren[i]:ClearUICacheData()
    end
end

return UIChild