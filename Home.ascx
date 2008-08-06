<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.Home" AutoEventWireup="false" CodeBehind="Home.ascx.cs" %>
<%@ Import Namespace="DotNetNuke.Services.Localization"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Charting, Culture=neutral, PublicKeyToken=d14f3dcc8e3e8763" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>

<%@ Register assembly="Telerik.Charting, Version=2.0.3.0, Culture=neutral, PublicKeyToken=d14f3dcc8e3e8763" namespace="Telerik.Charting" tagprefix="telerik" %>

<asp:MultiView ID="ChartsMultiview" runat="server" ActiveViewIndex="0" EnableViewState="false">
    <asp:View ID="ChartsView" runat="server">
        <div class="chartWrapper">
            <fieldset>
                <legend class="SubHead">
                    <asp:Label runat="server" ResourceKey="Event Log.Title" /> <asp:HyperLink runat="server" ID="AboutEventLogLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
                </legend>

                <telerik:RadChart ID="EventLogChart" runat="server" IntelligentLabelsEnabled="True" EnableHandlerDetection="False" TempImagesFolder="~/DesktopModules/EngageDashboard/ChartsTemp" AutoLayout="True" AutoTextWrap="True">
                    <Series>
                        <telerik:ChartSeries Type="Pie" DefaultLabelValue="#%" Appearance-LegendDisplayMode="ItemLabels">
                            <appearance legenddisplaymode="ItemLabels">
                                <fillstyle maincolor="186, 207, 141" secondcolor="146, 176, 83">
                                    <fillsettings gradientmode="Vertical">
                                        <complexgradient>
                                            <telerik:GradientElement Color="213, 247, 255" />
                                            <telerik:GradientElement Color="193, 239, 252" Position="0.5" />
                                            <telerik:GradientElement Color="157, 217, 238" Position="1" />
                                        </complexgradient>
                                    </fillsettings>
                                </fillstyle>
                                <textappearance textproperties-color="89, 89, 89"/>
                                <border color="" />
                            </appearance>
                        </telerik:ChartSeries>
                    </Series>
                    <plotarea>
                        <xaxis>
                            <appearance color="134, 134, 134" majortick-color="134, 134, 134">
                                <majorgridlines color="196, 196, 196" width="0" />
                                <textappearance textproperties-color="89, 89, 89"/>
                            </appearance>
                            <axislabel>
                                <appearance dimensions-paddings="1px, 1px, 10%, 1px"/>
                                <textblock>
                                    <appearance textproperties-color="51, 51, 51"/>
                                </textblock>
                            </axislabel>
                        </xaxis>
                        <yaxis>
                            <appearance color="134, 134, 134" majortick-color="196, 196, 196" minortick-color="196, 196, 196">
                                <majorgridlines color="196, 196, 196" />
                                <minorgridlines color="196, 196, 196" width="0" />
                                <textappearance textproperties-color="89, 89, 89"/>
                            </appearance>
                            <axislabel>
                                <textblock>
                                    <appearance textproperties-color="220, 158, 119"/>
                                </textblock>
                            </axislabel>
                        </yaxis>
                        <appearance dimensions-margins="19%, 90px, 12%, 9%">
                            <fillstyle maincolor="Transparent" secondcolor="Transparent"/>
                            <border color="WhiteSmoke" />
                        </appearance>
                    </plotarea>
                    <appearance corners="Round, Round, Round, Round, 7">
                        <fillstyle filltype="ComplexGradient">
                            <fillsettings>
                                <complexgradient>
                                    <telerik:GradientElement Color="243, 253, 255" />
                                    <telerik:GradientElement Color="White" Position="0.5" />
                                    <telerik:GradientElement Color="243, 253, 255" Position="1" />
                                </complexgradient>
                            </fillsettings>
                        </fillstyle>
                        <border color="212, 221, 222" />
                    </appearance>
                    <charttitle>
                        <appearance dimensions-margins="3%, 10px, 14px, 6%">
                            <fillstyle maincolor=""/>
                        </appearance>
                        <textblock>
                            <appearance textproperties-color="86, 88, 89" textproperties-font="Verdana, 22px"/>
                        </textblock>
                    </charttitle>
                    <legend>
                        <appearance dimensions-margins="1px, 1%, 10%, 1px">
                            <itemtextappearance textproperties-color="86, 88, 89"/>
                            <itemmarkerappearance figure="Rectangle"/>
                            <fillstyle maincolor=""/>
                            <border color="" />
                        </appearance>
                        <textblock>
                            <appearance position-alignedposition="Top" textproperties-color="86, 88, 89"/>
                        </textblock>
                    </legend>
                </telerik:RadChart>
                <engage:ModuleMessage ID="AboutEventLogMessage" runat="server" MessageType="Information" CssClass="chartText" ResourceKey="EventLogChart.Text" style="display:none" />
            </fieldset>
        </div>
        <div class="chartWrapper">
            <fieldset>
                <legend class="SubHead">
                    <asp:Label runat="server" ResourceKey="Seo Pages.Title" /> <asp:HyperLink runat="server" ID="AboutSeoPagesLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
                </legend>
                
                <telerik:RadChart ID="SeoPagesChart" runat="server" IntelligentLabelsEnabled="True" EnableHandlerDetection="False" TempImagesFolder="~/DesktopModules/EngageDashboard/ChartsTemp" AutoLayout="True" AutoTextWrap="True">
                    <Series>
                        <telerik:ChartSeries Type="Pie" DefaultLabelValue="#%" Appearance-LegendDisplayMode="ItemLabels">
                            <appearance legenddisplaymode="ItemLabels">
                                <fillstyle maincolor="186, 207, 141" secondcolor="146, 176, 83">
                                    <fillsettings gradientmode="Vertical">
                                        <complexgradient>
                                            <telerik:GradientElement Color="213, 247, 255" />
                                            <telerik:GradientElement Color="193, 239, 252" Position="0.5" />
                                            <telerik:GradientElement Color="157, 217, 238" Position="1" />
                                        </complexgradient>
                                    </fillsettings>
                                </fillstyle>
                                <textappearance textproperties-color="89, 89, 89"/>
                                <border color="" />
                            </appearance>
                        </telerik:ChartSeries>
                    </Series>
                    <plotarea>
                        <xaxis>
                            <appearance color="134, 134, 134" majortick-color="134, 134, 134">
                                <majorgridlines color="196, 196, 196" width="0" />
                                <textappearance textproperties-color="89, 89, 89"/>
                            </appearance>
                            <axislabel>
                                <appearance dimensions-paddings="1px, 1px, 10%, 1px"/>
                                <textblock>
                                    <appearance textproperties-color="51, 51, 51"/>
                                </textblock>
                            </axislabel>
                        </xaxis>
                        <yaxis>
                            <appearance color="134, 134, 134" majortick-color="196, 196, 196" minortick-color="196, 196, 196">
                                <majorgridlines color="196, 196, 196" />
                                <minorgridlines color="196, 196, 196" width="0" />
                                <textappearance textproperties-color="89, 89, 89"/>
                            </appearance>
                            <axislabel>
                                <textblock>
                                    <appearance textproperties-color="220, 158, 119"/>
                                </textblock>
                            </axislabel>
                        </yaxis>
                        <appearance dimensions-margins="19%, 90px, 12%, 9%">
                            <fillstyle maincolor="Transparent" secondcolor="Transparent"/>
                            <border color="WhiteSmoke" />
                        </appearance>
                    </plotarea>
                    <appearance corners="Round, Round, Round, Round, 7">
                        <fillstyle filltype="ComplexGradient">
                            <fillsettings>
                                <complexgradient>
                                    <telerik:GradientElement Color="243, 253, 255" />
                                    <telerik:GradientElement Color="White" Position="0.5" />
                                    <telerik:GradientElement Color="243, 253, 255" Position="1" />
                                </complexgradient>
                            </fillsettings>
                        </fillstyle>
                        <border color="212, 221, 222" />
                    </appearance>
                    <charttitle>
                        <appearance dimensions-margins="3%, 10px, 14px, 6%">
                            <fillstyle maincolor=""/>
                        </appearance>
                        <textblock>
                            <appearance textproperties-color="86, 88, 89" textproperties-font="Verdana, 22px"/>
                        </textblock>
                    </charttitle>
                    <legend>
                        <appearance dimensions-margins="1px, 1%, 10%, 1px">
                            <itemtextappearance textproperties-color="86, 88, 89"/>
                            <itemmarkerappearance figure="Rectangle"/>
                            <fillstyle maincolor=""/>
                            <border color="" />
                        </appearance>
                        <textblock>
                            <appearance position-alignedposition="Top" textproperties-color="86, 88, 89"/>
                        </textblock>
                    </legend>
                </telerik:RadChart>
                <engage:ModuleMessage ID="AboutSeoPagesMessage" runat="server" MessageType="Information" CssClass="chartText" ResourceKey="SeoPagesChart.Text" style="display:none" />
            </fieldset>
        </div>
        <asp:Panel ID="databaseSizeChartPanel" runat="server" CssClass="chartWrapper" Visible="false">
            <fieldset>
                <legend class="SubHead">
                    <asp:Label runat="server" ResourceKey="Database Size.Title" /> <asp:HyperLink runat="server" ID="AboutDatabaseSizeLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
                </legend>

                <telerik:RadChart ID="DatabaseSizeChart" runat="server" IntelligentLabelsEnabled="True" EnableHandlerDetection="False" TempImagesFolder="~/DesktopModules/EngageDashboard/ChartsTemp" AutoLayout="True" AutoTextWrap="True" >
                    <Series>
                        <telerik:ChartSeries Type="Pie" DefaultLabelValue="#%" Appearance-LegendDisplayMode="ItemLabels">
                            <appearance legenddisplaymode="ItemLabels">
                                <fillstyle maincolor="186, 207, 141" secondcolor="146, 176, 83">
                                    <fillsettings gradientmode="Vertical">
                                        <complexgradient>
                                            <telerik:GradientElement Color="213, 247, 255" />
                                            <telerik:GradientElement Color="193, 239, 252" Position="0.5" />
                                            <telerik:GradientElement Color="157, 217, 238" Position="1" />
                                        </complexgradient>
                                    </fillsettings>
                                </fillstyle>
                                <textappearance textproperties-color="89, 89, 89"/>
                                <border color="" />
                            </appearance>
                        </telerik:ChartSeries>
                    </Series>
                    <plotarea>
                        <xaxis>
                            <appearance color="134, 134, 134" majortick-color="134, 134, 134">
                                <majorgridlines color="196, 196, 196" width="0" />
                                <textappearance textproperties-color="89, 89, 89"/>
                            </appearance>
                            <axislabel>
                                <appearance dimensions-paddings="1px, 1px, 10%, 1px"/>
                                <textblock>
                                    <appearance textproperties-color="51, 51, 51"/>
                                </textblock>
                            </axislabel>
                        </xaxis>
                        <yaxis>
                            <appearance color="134, 134, 134" majortick-color="196, 196, 196" minortick-color="196, 196, 196">
                                <majorgridlines color="196, 196, 196" />
                                <minorgridlines color="196, 196, 196" width="0" />
                                <textappearance textproperties-color="89, 89, 89"/>
                            </appearance>
                            <axislabel>
                                <textblock>
                                    <appearance textproperties-color="220, 158, 119"/>
                                </textblock>
                            </axislabel>
                        </yaxis>
                        <appearance dimensions-margins="19%, 90px, 12%, 9%">
                            <fillstyle maincolor="Transparent" secondcolor="Transparent"/>
                            <border color="WhiteSmoke" />
                        </appearance>
                    </plotarea>
                    <appearance corners="Round, Round, Round, Round, 7">
                        <fillstyle filltype="ComplexGradient">
                            <fillsettings>
                                <complexgradient>
                                    <telerik:GradientElement Color="243, 253, 255" />
                                    <telerik:GradientElement Color="White" Position="0.5" />
                                    <telerik:GradientElement Color="243, 253, 255" Position="1" />
                                </complexgradient>
                            </fillsettings>
                        </fillstyle>
                        <border color="212, 221, 222" />
                    </appearance>
                    <charttitle>
                        <appearance dimensions-margins="3%, 10px, 14px, 6%">
                            <fillstyle maincolor=""/>
                        </appearance>
                        <textblock>
                            <appearance textproperties-color="86, 88, 89" textproperties-font="Verdana, 22px"/>
                        </textblock>
                    </charttitle>
                    <legend>
                        <appearance dimensions-margins="1px, 1%, 10%, 1px">
                            <itemtextappearance textproperties-color="86, 88, 89"/>
                            <itemmarkerappearance figure="Rectangle"/>
                            <fillstyle maincolor=""/>
                            <border color="" />
                        </appearance>
                        <textblock>
                            <appearance position-alignedposition="Top" textproperties-color="86, 88, 89"/>
                        </textblock>
                    </legend>
                </telerik:RadChart>
                <engage:ModuleMessage ID="AboutDatabaseSizeMessage" runat="server" MessageType="Information" CssClass="chartText" ResourceKey="DatabaseSizeChart.Text" style="display:none" />
            </fieldset></asp:Panel>
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
