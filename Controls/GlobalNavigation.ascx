<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Dashboard.GlobalNavigation" Codebehind="GlobalNavigation.ascx.cs" %>

<div id="divAdminButtons" class="AdminButtons CommandButton">
    <asp:HyperLink ID="AdminLink" runat="server" CausesValidation="false" ResourceKey="Admin.Alt"/>
    <asp:HyperLink ID="HostLink" runat="server" CausesValidation="false" ResourceKey="Host.Alt"/>
    <asp:HyperLink ID="ModuleLocatorLink" runat="server" CausesValidation="false" ResourceKey="ModuleLocator.Alt"/>
    <asp:HyperLink ID="SkinLocatorLink" runat="server" CausesValidation="false" ResourceKey="SkinLocator.Alt" Visible="false"/>
    <asp:HyperLink ID="F3Link" runat="server" CausesValidation="false" ResourceKey="F3.Alt"/>
    <asp:HyperLink ID="SettingsLink" runat="server" CausesValidation="false" ResourceKey="Settings.Alt"/>
</div>