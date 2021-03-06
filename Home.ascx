<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.Home" AutoEventWireup="false" CodeBehind="Home.ascx.cs" %>
<%@ Import Namespace="DotNetNuke.Services.Localization"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Charting, Version=2.0.4.0, Culture=neutral, PublicKeyToken=d14f3dcc8e3e8763" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>

<asp:MultiView ID="ChartsMultiview" runat="server" ActiveViewIndex="0" EnableViewState="false">
    <asp:View ID="ChartsView" runat="server">
        <div class="chartWrapper">
            <fieldset>
                <legend class="SubHead">
                    <asp:Label runat="server" ResourceKey="Event Log.Title" /> <asp:HyperLink runat="server" ID="AboutEventLogLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
                </legend>

                <telerik:RadChart ID="EventLogChart" runat="server" Skin="Telerik" IntelligentLabelsEnabled="True" EnableHandlerDetection="False" TempImagesFolder="~/DesktopModules/EngageDashboard/ChartsTemp" AutoLayout="True" AutoTextWrap="True">
                    <Series>
                        <telerik:ChartSeries Type="Pie" DefaultLabelValue="#%" Appearance-LegendDisplayMode="ItemLabels"/>
                    </Series>
                </telerik:RadChart>
                <engage:ModuleMessage ID="AboutEventLogMessage" runat="server" MessageType="Information" CssClass="chartText" ResourceKey="EventLogChart.Text" style="display:none" />
            </fieldset>
        </div>
        <div class="chartWrapper">
            <fieldset>
                <legend class="SubHead">
                    <asp:Label runat="server" ResourceKey="Seo Pages.Title" /> <asp:HyperLink runat="server" ID="AboutSeoPagesLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
                </legend>
                
                <telerik:RadChart ID="SeoPagesChart" runat="server" Skin="Telerik" IntelligentLabelsEnabled="True" EnableHandlerDetection="False" TempImagesFolder="~/DesktopModules/EngageDashboard/ChartsTemp" AutoLayout="True" AutoTextWrap="True">
                    <Series>
                        <telerik:ChartSeries Type="Pie" DefaultLabelValue="#%" Appearance-LegendDisplayMode="ItemLabels"/>
                    </Series>
                </telerik:RadChart>
                <engage:ModuleMessage ID="AboutSeoPagesMessage" runat="server" MessageType="Information" CssClass="chartText" ResourceKey="SeoPagesChart.Text" style="display:none" />
            </fieldset>
        </div>
        <asp:Panel ID="databaseSizeChartPanel" runat="server" CssClass="chartWrapper" Visible="false">
            <fieldset>
                <legend class="SubHead">
                    <asp:Label runat="server" ResourceKey="Database Size.Title" /> <asp:HyperLink runat="server" ID="AboutDatabaseSizeLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
                </legend>

                <telerik:RadChart ID="DatabaseSizeChart" runat="server" Skin="Telerik" IntelligentLabelsEnabled="True" EnableHandlerDetection="False" TempImagesFolder="~/DesktopModules/EngageDashboard/ChartsTemp" AutoLayout="True" AutoTextWrap="True" >
                    <Series>
                        <telerik:ChartSeries Type="Pie" DefaultLabelValue="#%" Appearance-LegendDisplayMode="ItemLabels"/>
                    </Series>
                </telerik:RadChart>
                <engage:ModuleMessage ID="AboutDatabaseSizeMessage" runat="server" MessageType="Information" CssClass="chartText" ResourceKey="DatabaseSizeChart.Text" style="display:none" />
            </fieldset>
        </asp:Panel>
    </asp:View>
    <asp:View ID="InstallChartingView" runat="server">
        <fieldset>
            <legend class="SubHead"><asp:Label runat="server" ResourceKey="InstallChartingSupport.Title" /></legend>
            <div style="clear:both;">
                <div class="installMessage">
                    <asp:Label runat="server" ResourceKey="InstallChartingSupportIntro.Text" CssClass="Normal" /> <asp:HyperLink runat="server" ID="ChartingSupportLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
                </div>
                <img src='<%=ResolveUrl("Images/disabledChart.png") %>' alt='<%=Localization.GetString("DisabledChart.Alt", this.LocalResourceFile) %>' />
            </div>
            <div id="InstallChartingPanel" style="display: none;">
                <engage:ModuleMessage runat="server" ID="ChartingSupportMessage" ResourceKey="InstallChartingSupport.Text" CssClass="Normal" MessageType="Information" />
                <asp:Button ID="InstallChartingButton" runat="server" ResourceKey="InstallChartingButton" />
            </div>
        </fieldset>
    </asp:View>
</asp:MultiView>
