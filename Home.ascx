<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.Home" AutoEventWireup="false" CodeBehind="Home.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Charting, Culture=neutral, PublicKeyToken=d14f3dcc8e3e8763" %>
<%@ Register TagPrefix="dundas" Namespace="Dundas.Gauges.WebControl" Assembly="DundasWebGauge" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>

<%@ Register assembly="Telerik.Charting, Version=2.0.3.0, Culture=neutral, PublicKeyToken=d14f3dcc8e3e8763" namespace="Telerik.Charting" tagprefix="telerik" %>

<dundas:GaugeContainer id="DashboardGaugeContainer" RenderType="RealTimeStreaming" BackColor="WhiteSmoke" Width="714px" Height="144px" runat="server">
    <BackFrame FrameWidth="0" Style="none" BackGradientEndColor="Gainsboro" Shape="Rectangular" BackColor="Transparent" FrameGradientEndColor="Black" BackGradientType="None" />
    <CircularGauges>
		<dundas:CircularGauge Name="PagesGauge">
			<Scales>
				<dundas:CircularScale Maximum="100" ShadowOffset="0" Width="2" FillColor="Black" StartAngle="90" SweepAngle="180" Radius="32" Name="Default">
					<LabelStyle Font="Trebuchet MS, 8.25pt" Placement="Outside" FontUnit="Default"/>
					<MajorTickMark Width="2" EnableGradient="False" BorderWidth="0" FillColor="Black" Length="12" Shape="Rectangle" Placement="Outside"/>
					<MinorTickMark Width="2" EnableGradient="False" BorderWidth="0" FillColor="Black" Placement="Outside"/>
				</dundas:CircularScale>
			</Scales>
			<BackFrame FrameWidth="0" Style="Simple" BorderColor="Silver" BackGradientEndColor="211, 229, 211" Shape="AutoShape" BackColor="White" BackGradientType="TopBottom" BorderWidth="2"/>
			<Size Height="100" Width="25"/>
			<PivotPoint Y="70" X="50"/>
			<Location Y="0" X="0"/>
			<Pointers>
				<dundas:CircularPointer DistanceFromScale="8" NeedleStyle="NeedleStyle4" MarkerLength="30" FillGradientEndColor="White" CapWidth="30" FillColor="Red" BorderWidth="0" ShadowOffset="1" Name="Default" Placement="Outside"/>
			</Pointers>
			<Ranges>
				<dundas:CircularRange Name="Default" BorderWidth="0" StartValue="0" StartWidth="100" FillColor="80, 0, 255, 0" DistanceFromScale="0" EndWidth="300" FillGradientType="LeftRight"/>
			</Ranges>
		</dundas:CircularGauge>
		<dundas:CircularGauge Name="DatabaseGauge">
			<Scales>
				<dundas:CircularScale Maximum="100" ShadowOffset="0" Width="2" FillColor="Black" StartAngle="90" SweepAngle="180" Radius="32" Name="Default">
					<LabelStyle Font="Trebuchet MS, 8.25pt" Placement="Outside" FontUnit="Default"/>
					<MajorTickMark Width="2" EnableGradient="False" BorderWidth="0" FillColor="Black" Length="12" Shape="Rectangle" Placement="Outside"/>
					<MinorTickMark Width="2" EnableGradient="False" BorderWidth="0" FillColor="Black" Placement="Outside"/>
				</dundas:CircularScale>
			</Scales>
			<BackFrame FrameWidth="0" Style="Simple" BorderColor="Silver" BackGradientEndColor="237, 215, 208" Shape="AutoShape" BackColor="White" BackGradientType="TopBottom" BorderWidth="2"/>
			<Size Height="100" Width="25"/>
			<PivotPoint Y="70" X="50"/>
			<Location Y="0" X="25"/>
			<Pointers>
				<dundas:CircularPointer DistanceFromScale="8" NeedleStyle="NeedleStyle4" FillGradientEndColor="White" CapWidth="30" FillColor="Red" BorderWidth="0" ShadowOffset="1" Name="Default" Placement="Outside"/>
			</Pointers>
			<Ranges>
				<dundas:CircularRange Name="RedZone" BorderWidth="0" StartValue="10" StartWidth="300" FillColor="80, 255, 127, 80" DistanceFromScale="0" EndWidth="300" FillGradientType="LeftRight"/>
				<dundas:CircularRange DistanceFromScale="0" EndValue="10" EndWidth="100" FillGradientType="None" Name="GreenZone" StartValue="0" StartWidth="100" />
			</Ranges>
		</dundas:CircularGauge>
	</CircularGauges>
</dundas:GaugeContainer>
<asp:MultiView ID="ChartsMultiview" runat="server" ActiveViewIndex="0" EnableViewState="false">
    <asp:View ID="ChartsView" runat="server">
        <telerik:RadChart ID="UserRegistrationsChart" runat='server' BorderWidth="0" Legend-Visible="false" ChartTitle-Visible="false" Height="150px" Width="200px" IntelligentLabelsEnabled="True" Skin="Telerik" CreateImageMap="False" EnableHandlerDetection="False" TempImagesFolder="~/DesktopModules/EngageDashboard/ChartsTemp">
            <PlotArea>
                <XAxis AutoScale="False" Visible="False">
                    <%--<Appearance ValueFormat="ShortDate">
                        <LabelAppearance RotationAngle="-15"/>
                    </Appearance>--%>
                </XAxis>
                <YAxis AutoScale="true" LabelStep="2" />
                <Appearance Dimensions-Margins="18%, 4%, 12%, 12%"/>
            </PlotArea>
        </telerik:RadChart>
    </asp:View>
    <asp:View ID="InstallChartingView" runat="server">
        <fieldset>
            <legend class="SubHead"><asp:Label runat="server" ResourceKey="InstallChartingSupport.Title" /> <asp:HyperLink runat="server" ID="ChartingSupportLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" /></legend>
            <div id="InstallChartingPanel" style="display: none;">
                <%-- Add greyed-out chart image --%>
                <engage:ModuleMessage runat="server" ID="ChartingSupportMessage" ResourceKey="InstallChartingSupport.Text" CssClass="Normal" MessageType="Information" />
                <asp:Button ID="InstallChartingButton" runat="server" ResourceKey="InstallChartingButton" />
            </div>
        </fieldset>
    </asp:View>
</asp:MultiView>
