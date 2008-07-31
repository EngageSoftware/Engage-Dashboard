<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.Home" AutoEventWireup="false" CodeBehind="Home.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Charting, Culture=neutral, PublicKeyToken=d14f3dcc8e3e8763" %>
<%@ Register Assembly="DundasWebGauge" Namespace="Dundas.Gauges.WebControl" TagPrefix="DGWC" %>
<%@ Import Namespace="Globals=DotNetNuke.Common.Globals" %>


<%--<DGWC:GaugeContainer runat="server" ID="PagesGauge" BackColor="White">
    <BackFrame FrameShape="Rectangular" FrameStyle="None" />
    <CircularGauges>
        <DGWC:CircularGauge Name="Default">
            <Scales>
                <DGWC:CircularScale Name="Default">
                </DGWC:CircularScale>
            </Scales>
            <Ranges>
                <DGWC:CircularRange Name="Default" EndWidth="25" StartValue="0" StartWidth="10" />
            </Ranges>
            <Pointers>
                <DGWC:CircularPointer Name="Default" />
            </Pointers>
            <PivotPoint X="50" Y="50" />
            <Location X="0" Y="0" />
            <Size Height="100" Width="100" />
            <BackFrame FrameShape="CustomCircular3" FrameStyle="Edged" />
        </DGWC:CircularGauge>
    </CircularGauges>
</DGWC:GaugeContainer>

<DGWC:GaugeContainer runat="server" ID="DatabaseGauge" BackColor="White">
    <BackFrame FrameShape="Rectangular" FrameStyle="None" />
    <CircularGauges>
        <DGWC:CircularGauge Name="Default">
            <Scales>
                <DGWC:CircularScale Name="Default">
                </DGWC:CircularScale>
            </Scales>
            <Ranges>
                <DGWC:CircularRange Name="Default" EndWidth="15" StartValue="0" StartWidth="5" 
                    EndValue="10" FillGradientType="None" />
                <DGWC:CircularRange EndWidth="25" FillColor="Tomato" 
                    FillGradientEndColor="DarkRed" Name="Range2" StartValue="10" />
            </Ranges>
            <Pointers>
                <DGWC:CircularPointer Name="Default" />
            </Pointers>
            <PivotPoint X="50" Y="50"></PivotPoint>
            <Location X="0" Y="0"></Location>
            <Size Width="100" Height="100" />
            <BackFrame FrameShape="CustomCircular3" FrameStyle="Edged" />
        </DGWC:CircularGauge>
    </CircularGauges>
</DGWC:GaugeContainer>
--%>


	<DGWC:GAUGECONTAINER id="DashboardGaugeContainer" RenderType="RealTimeStreaming" BackColor="WhiteSmoke" Width="714px"
		Height="144px" runat="server">
		<%--<Values>
			<dgwc:InputValue Name="PurlHits"></dgwc:InputValue>
			<dgwc:InputValue Name="Conversions"></dgwc:InputValue>
		</Values>--%>
		<BackFrame FrameWidth="0" Style="None" BackGradientEndColor="Gainsboro" Shape="Rectangular"
			BackColor="Transparent" FrameGradientEndColor="Black" BackGradientType="None"></BackFrame>
		<CircularGauges>
			<dgwc:CircularGauge Name="PagesGauge">
				<Scales>
					<dgwc:CircularScale Maximum="100" ShadowOffset="0" Width="2" FillColor="Black" StartAngle="90" SweepAngle="180" 
 Radius="32" Name="Default">
						<LabelStyle Font="Trebuchet MS, 8.25pt" Placement="Outside" FontUnit="Default"></LabelStyle>
						<MajorTickMark Width="2" EnableGradient="False" BorderWidth="0" FillColor="Black" Length="12" Shape="Rectangle"
							Placement="Outside"></MajorTickMark>
						<MinorTickMark Width="2" EnableGradient="False" BorderWidth="0" FillColor="Black" Placement="Outside"></MinorTickMark>
					</dgwc:CircularScale>
				</Scales>
				<BackFrame FrameWidth="0" Style="Simple" BorderColor="Silver" BackGradientEndColor="211, 229, 211"
					Shape="AutoShape" BackColor="White" BackGradientType="TopBottom" BorderWidth="2"></BackFrame>
				<Size Height="100" Width="25"></Size>
				<PivotPoint Y="70" X="50"></PivotPoint>
				<Location Y="0" X="0"></Location>
				<Pointers>
					<dgwc:CircularPointer DistanceFromScale="8" NeedleStyle="NeedleStyle4" MarkerLength="30" FillGradientEndColor="White" 
 CapWidth="30" FillColor="Red" BorderWidth="0" ShadowOffset="1" Name="Default" Placement="Outside"></dgwc:CircularPointer>
				</Pointers>
				<Ranges>
					<dgwc:CircularRange Name="Default" BorderWidth="0" StartValue="0" 
                        StartWidth="100" FillColor="80, 0, 255, 0" DistanceFromScale="0" EndWidth="300" 
 FillGradientType="LeftRight"></dgwc:CircularRange>
				</Ranges>
			</dgwc:CircularGauge>
			<dgwc:CircularGauge Name="DatabaseGauge">
				<Scales>
					<dgwc:CircularScale Maximum="100" ShadowOffset="0" Width="2" FillColor="Black" StartAngle="90" SweepAngle="180" 
 Radius="32" Name="Default">
						<LabelStyle Font="Trebuchet MS, 8.25pt" Placement="Outside" FontUnit="Default"></LabelStyle>
						<MajorTickMark Width="2" EnableGradient="False" BorderWidth="0" FillColor="Black" Length="12" Shape="Rectangle"
							Placement="Outside"></MajorTickMark>
						<MinorTickMark Width="2" EnableGradient="False" BorderWidth="0" FillColor="Black" Placement="Outside"></MinorTickMark>
					</dgwc:CircularScale>
				</Scales>
				<BackFrame FrameWidth="0" Style="Simple" BorderColor="Silver" BackGradientEndColor="237, 215, 208"
					Shape="AutoShape" BackColor="White" BackGradientType="TopBottom" BorderWidth="2"></BackFrame>
				<Size Height="100" Width="25"></Size>
				<PivotPoint Y="70" X="50"></PivotPoint>
				<Location Y="0" X="25"></Location>
				<Pointers>
					<dgwc:CircularPointer DistanceFromScale="8" NeedleStyle="NeedleStyle4" FillGradientEndColor="White" CapWidth="30" 
 FillColor="Red" BorderWidth="0" ShadowOffset="1" Name="Default" Placement="Outside"></dgwc:CircularPointer>
				</Pointers>
				<Ranges>
					<dgwc:CircularRange Name="RedZone" BorderWidth="0" StartValue="10" 
                        StartWidth="300" FillColor="80, 255, 127, 80" DistanceFromScale="0" EndWidth="300" 
 FillGradientType="LeftRight"></dgwc:CircularRange>
				    <DGWC:CircularRange DistanceFromScale="0" EndValue="10" EndWidth="100" 
                        FillGradientType="None" Name="GreenZone" StartValue="0" StartWidth="100" />
				</Ranges>
			</dgwc:CircularGauge>
		</CircularGauges>
	</DGWC:GAUGECONTAINER>

<asp:MultiView ID="ChartsMultiview" runat="server" ActiveViewIndex="0" EnableViewState="false">
    <asp:View ID="ChartsView" runat="server">
        <telerik:RadChart ID="UserRegistrationsChart" runat='server' DefaultType="SplineArea">
            <ChartTitle><TextBlock Text="Chart goes here"/></ChartTitle>
        </telerik:RadChart>
    </asp:View>
    <asp:View ID="InstallChartingView" runat="server">
        <asp:Button ID="InstallChartingButton" runat="server" ResourceKey="InstallChartingButton" />
    </asp:View>
</asp:MultiView>
