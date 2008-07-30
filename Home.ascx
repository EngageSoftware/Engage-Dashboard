<%@ Import Namespace="Globals=DotNetNuke.Common.Globals" %>
<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.Home" AutoEventWireup="false" CodeBehind="Home.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Charting, Version=2.0.3.0, Culture=neutral, PublicKeyToken=d14f3dcc8e3e8763" %>
<asp:MultiView ID="ChartsMultiview" runat="server" ActiveViewIndex="0">
    <asp:View ID="ChartsView" runat="server">
        <telerik:RadChart ID="UserRegistrationsChart" runat='server' DefaultType="SplineArea">
            <ChartTitle><TextBlock Text="Chart goes here"/></ChartTitle>
        </telerik:RadChart>
    </asp:View>
    <asp:View ID="InstallChartingView" runat="server">
        <asp:Button ID="InstallChartingButton" runat="server" ResourceKey="InstallChartingButton" />
    </asp:View>
</asp:MultiView>
