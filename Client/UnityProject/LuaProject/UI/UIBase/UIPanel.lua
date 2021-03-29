local UIPanel = class("UIPanel")

function UIPanel:Back()
	for i=1,#self.panelChildren do
		self.panelChildren[i]:Back()
	end
end

function UIPanel:Close()
	UIMgr.Close(self.UI)
end

function UIPanel:Create(UI,root,cb)
	self.UI = UI
	local prefab = ResourceMgr.LoadSyncPrefab(UI.prefab)
	prefab.name = UI.name
	GOUtil.AttachGameObject(prefab,root)
	self.prefab = prefab
	if cb then
		cb()
	end
end

function UIPanel:Init()
	print("[UI Init]"..self.UI.name)
	self.panelChildren = {}
end

function UIPanel:PreEnter(cb)
	print("[UI PreEnter]"..self.UI.name)
	if cb then
		cb()
	end
end

function UIPanel:Enter(param)
	print("[UI Enter]"..self.UI.name)
	self.param = param
	self:Show()
end

function UIPanel:Update(deltaTime)

end

function UIPanel:PreExit(cb)
	print("[UI PreExit]"..self.UI.name)
	if cb then
		cb()
	end
end

function UIPanel:Exit() 
	print("[UI Exit]"..self.UI.name)
	self:Hide()
end

function UIPanel:Destroy() 
	print("[UI Destroy]"..self.UI.name)
	self:RemoveAllChild()
	ResourceMgr.Unload(self.prefab)
    self.prefab = nil
end

function UIPanel:Show()
	GOUtil.SetActive(self.prefab,true)
end

function UIPanel:Hide()
	GOUtil.SetActive(self.prefab,false)
end

function UIPanel:Register()
	for i=1,#self.panelChildren do
		self.panelChildren[i]:Register()
	end
end

function UIPanel:Unregister()
	for i=1,#self.panelChildren do
		self.panelChildren[i]:Unregister()
	end
end

function UIPanel:ClearUICacheData()
	for i=1,#self.panelChildren do
		self.panelChildren[i]:ClearUICacheData()
	end
	self.param = nil
	self.UI.param = nil
end

--child begin
function UIPanel:AddChild(UI)
    local obj = ResourceMgr.LoadSyncPrefab(UI.prefab)
    obj.name = UI.name
    local child = require(UI.lua)()
    child:Init(UI, obj, self)
    table.insert(self.panelChildren, child)
    return child
end

function UIPanel:GetChildPanel(UI)
    for i = 1, #self.panelChildren do
        if self.panelChildren[i].UI == UI then
            return self.panelChildren[i]
        end
    end
    return nil
end

function UIPanel:RemoveChild(child)
    for i = 1, #self.panelChildren do
        if self.panelChildren[i] == child then
            table.remove(self.panelChildren, i)
            child:Destroy()
            break
        end
    end
end

function UIPanel:RemoveAllChild()
    for i = 1, #self.panelChildren do
        self.panelChildren[i]:Destroy()
    end
    self.panelChildren = { }
end
--child end

return UIPanel