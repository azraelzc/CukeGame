local M = class("UIBack", UIChild)

function M:Init(UI,prefab,parent)
	self.super.Init(self,UI,prefab,parent)
	self.btnBack = ClickTriggerListener.GetListener(GOUtil.FindChildGameObject(self.prefab,"Btn_Back"))
	self.btnBack:onPointerClick("+", function(go, eData)
		if self.closeCallback then
			self.closeCallback()
		else
			UIMgr.Close(self.parent.UI)
		end
	end)
	self.btnHelp = ClickTriggerListener.GetListener(GOUtil.FindChildGameObject(self.prefab,"Btn_Help"))
	self.btnHelp:onPointerClick("+", function(go, eData)
		if self.helpCallback then
			self.helpCallback()
		elseif self.help then
			self.help:Show()
		end
	end)
	
	
end

function M:ShowBackButtion(evtName,flag)
	GOUtil.SetActive(self.btnBack.gameObject,flag)
end



function M:SetCloseCallback(callback)
	self.closeCallback = callback
end

function M:SetHelpCallback(callback)
	self.helpCallback = callback
end

function M:SetParent(root)
	self.super.SetParent(self,root)
	-- local map = ConfigMgr.GetConfigMap("HelpConfig")
	-- local helpConfig
	-- for k,v in pairs(map) do
	-- 	if v.UIName == self.parent.UI.name then
	-- 		helpConfig = v
	-- 		break
	-- 	end
	-- end
	-- GOUtil.SetActive(self.btnHelp,helpConfig ~= nil)
	-- if helpConfig then
	-- 	self.help = self:AddChild(UIDefine.ChildUI.Help)
	-- 	self.help:SetParent(self.parent.prefab,helpConfig)
	-- 	self.help:Hide()
	-- end
end

return M