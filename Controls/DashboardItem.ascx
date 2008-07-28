<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Dashboard.DashboardItem" Codebehind="DashboardItem.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="DashboardItemWrapper">
    <div id='<%=this.ClientID %>' class='DashboardItem Normal <%=CssClass %>'>
        <div class="valueLabel"><label for='<%=this.ValueId %>'><asp:Literal ID="TitleLiteral" runat="server" /></label></div>
        <div class="valueItem" id='<%=this.ValueId %>'><%=this.Value %></div>
        <div class="valueLink"><asp:HyperLink runat="server" NavigateUrl="javascript:void(0);" Cssclass="CommandButton" ID="DetailsLink" Visible="false" /></div>
        <div class="valueLink"><asp:HyperLink runat="server" NavigateUrl="javascript:void(0);" Cssclass="CommandButton" ID="DateRangeLink" Visible="false" /></div>
        <div class="valueLink"><asp:HyperLink runat="server" Cssclass="CommandButton" ID="NavigateLink" Visible="false" /></div>
        <asp:Panel runat="server" ID="DateRangePanel" class="DateRangePanel" style="display: none;" Visible="false">
            <asp:Label runat="server" ResourceKey="BeginDatePicker" AssociatedControlID="BeginDatePicker" />
            <telerik:raddatepicker runat="server" id="BeginDatePicker" />

            <asp:Label runat="server" ResourceKey="EndDatePicker" AssociatedControlID="EndDatePicker" />
            <telerik:raddatepicker runat="server" id="EndDatePicker" />
            <asp:Button runat="server" ResourceKey="ChangeDateRangeButton" />
        </asp:Panel>
    </div>
</div>