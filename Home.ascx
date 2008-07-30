<%@ Import Namespace="Globals=DotNetNuke.Common.Globals" %>
<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.Home" AutoEventWireup="false" CodeBehind="Home.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Charting, Version=2.0.3.0, Culture=neutral, PublicKeyToken=d14f3dcc8e3e8763" %>

<asp:Panel ID="ChartsPanel" runat="server">
<telerik:RadChart ID="UserRegistrationsChart" runat='server' DefaultType="SplineArea">
    <Series>
<telerik:ChartSeries Name="Series 1">
<Appearance>
<FillStyle MainColor="222, 202, 155" FillType="ComplexGradient">
<FillSettings><ComplexGradient>
<telerik:GradientElement Color="222, 202, 152"></telerik:GradientElement>
<telerik:GradientElement Color="211, 185, 123" Position="0.5"></telerik:GradientElement>
<telerik:GradientElement Color="183, 154, 84" Position="1"></telerik:GradientElement>
</ComplexGradient>
</FillSettings>
</FillStyle>

<TextAppearance TextProperties-Color="159, 159, 159"></TextAppearance>

<Border Color="187, 149, 58"></Border>
</Appearance>
</telerik:ChartSeries>
<telerik:ChartSeries Name="Series 2">
<Appearance>
<FillStyle MainColor="172, 208, 217" FillType="ComplexGradient">
<FillSettings><ComplexGradient>
<telerik:GradientElement Color="172, 208, 217"></telerik:GradientElement>
<telerik:GradientElement Color="149, 193, 204" Position="0.5"></telerik:GradientElement>
<telerik:GradientElement Color="114, 162, 175" Position="1"></telerik:GradientElement>
</ComplexGradient>
</FillSettings>
</FillStyle>

<TextAppearance TextProperties-Color="159, 159, 159"></TextAppearance>

<Border Color="129, 180, 193"></Border>
</Appearance>
</telerik:ChartSeries>
</Series>

    <PlotArea>
        <EmptySeriesMessage Visible="True">
            <Appearance Visible="True"></Appearance>
        </EmptySeriesMessage>
        <XAxis>
            <Appearance Color="62, 62, 62" MajorTick-Color="48, 48, 48">
                <MajorGridLines Color="77, 77, 77" />
                <TextAppearance TextProperties-Color="159, 159, 159"></TextAppearance>
            </Appearance>
            <AxisLabel>
                <TextBlock>
                    <Appearance TextProperties-Color="159, 159, 159"></Appearance>
                </TextBlock>
            </AxisLabel>
        </XAxis>
        <YAxis>
            <Appearance Color="62, 62, 62" MajorTick-Color="48, 48, 48" MinorTick-Color="48, 48, 48">
                <MajorGridLines Color="77, 77, 77" />
                <MinorGridLines Color="77, 77, 77" />
                <TextAppearance TextProperties-Color="159, 159, 159"></TextAppearance>
            </Appearance>
            <AxisLabel>
                <TextBlock>
                    <Appearance TextProperties-Color="159, 159, 159"></Appearance>
                </TextBlock>
            </AxisLabel>
        </YAxis>
        <Appearance Dimensions-Margins="18%, 100px, 12%, 8%">
            <FillStyle FillType="Solid" MainColor="51, 51, 51"></FillStyle>
            <Border Color="62, 62, 62" />
        </Appearance>
    </PlotArea>
    <Appearance>
        <FillStyle MainColor="25, 25, 25"></FillStyle>
        <Border Color="5, 5, 5" />
    </Appearance>
    <ChartTitle>
        <Appearance Dimensions-Margins="3%, 10px, 14px, 6%">
            <FillStyle MainColor="Transparent"></FillStyle>
            <Border Color="Transparent" />
        </Appearance>
        <TextBlock>
            <Appearance TextProperties-Color="White" TextProperties-Font="Arial, 18pt"></Appearance>
        </TextBlock>
    </ChartTitle>
    <Legend>
        <Appearance Dimensions-Margins="1px, 2%, 9%, 1px" Position-AlignedPosition="BottomRight">
            <ItemTextAppearance TextProperties-Color="159, 159, 159" TextProperties-Font="Arial, 10pt"></ItemTextAppearance>
            <FillStyle MainColor="Transparent"></FillStyle>
            <Border Color="Transparent" />
        </Appearance>
    </Legend></telerik:RadChart>
</asp:Panel>

<asp:Panel ID="InstallChartingPanel" runat="server" Visible="false">
    <asp:Button ID="InstallChartingButton" runat="server" ResourceKey="InstallChartingButton"/>
</asp:Panel>