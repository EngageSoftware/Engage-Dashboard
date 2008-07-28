<%@ Import Namespace="DotNetNuke.Services.Localization"%>
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ModuleMessage.ascx.cs" Inherits="Engage.Dnn.Dashboard.ModuleMessage" EnableViewState="false" %>
<div id='<%=ClientID%>' class="<%=MessageStyle %>Message ModuleMessage" <%=StyleAttribute%>>
        <div class="<%=MessageStyle %>Top mmTop"></div>
        <div class="<%=MessageStyle %>Body mmBody">
            <span class="<%=MessageStyle %>Icon mmIcon"><%=Localization.GetString(MessageStyle, LocalResourceFile)%></span>
            <div class="mmText">
                <p class="Normal"><asp:Label ID="messageLabel" runat="server" /></p>
            </div>
        </div>
        <div class="<%=MessageStyle %>Bt mmBt"></div>
</div>