<%@ Control language="C#" Inherits="Engage.Dnn.Dashboard.MainContainer" AutoEventWireup="false"  Codebehind="MainContainer.ascx.cs" %>
<%@ Register TagPrefix="engage" TagName="GlobalNavigation" Src="Controls/GlobalNavigation.ascx" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>

<div class="GlobalNavigation">
    <engage:GlobalNavigation ID="GlobalNavigation" runat="server" />
</div>
<div id="EngageDashboard">
    <asp:PlaceHolder id="ControlPlaceholder" runat="Server" />
</div>

<engage:ModuleMessage ID="NoPermissionMessage" runat="server" MessageType="Error" ResourceKey="NoPermission.Text" CssClass="NormalRed" Visible="false" EnableViewState="false" />