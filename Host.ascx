<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.Host" AutoEventWireup="False" CodeBehind="Host.ascx.cs" %>
<%@ Register TagPrefix="engage" TagName="DashboardItem" Src="Controls/DashboardItem.ascx" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>

<fieldset>
    <legend class="SubHead">
        <asp:Label runat="server" ResourceKey="Database Size.Title" /> <asp:HyperLink runat="server" ID="AboutDatabaseSizeLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
    </legend>
    
    <engage:DashboardItem Id="DatabaseLogSizeItem" runat="server" TitleResourceKey="DatabaseLogSize.Text" /> 
    <engage:DashboardItem Id="DatabaseSizeItem" runat="server" TitleResourceKey="DatabaseSize.Text" /> 
   
    <engage:ModuleMessage runat="server" ID="AboutDatabaseSizeMessage" ResourceKey="Database Size.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>
    
<fieldset>
    <legend class="SubHead">
        <asp:Label runat="server" ResourceKey="Backup History.Title" /> <asp:HyperLink runat="server" ID="AboutBackupHistoryLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
    </legend>

    <engage:DashboardItem Id="BackupHistoryItem" runat="server" TitleResourceKey="BackupHistory.Text" DetailsPanelId="BackupHistoryPanel" /> 
    <asp:Panel ID="BackupHistoryPanel" runat="server" CssClass="detailsTableWrapper">
        <asp:GridView ID="BackupHistoryGridView" AutoGenerateColumns="false" runat="server" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:BoundField DataField="Backup Date" HeaderText="Backup Date" DataFormatString="{0:MM/dd HH:mm}" />
                <asp:BoundField DataField="Backup Type" HeaderText="Backup Type" />
                <asp:BoundField DataField="Server Name" HeaderText="Server Name" />
                <asp:BoundField DataField="Database" HeaderText="Database" />
                <asp:BoundField DataField="Recovery Model" HeaderText="Recovery Model" />
                <asp:BoundField DataField="Device" HeaderText="Device" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ResourceKey="NoBackups.Text" runat="server" />
            </EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>
    
    <engage:ModuleMessage runat="server" ID="AboutBackupHistoryMessage" ResourceKey="Backup History.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>

<fieldset>
    <legend class="SubHead">
        <asp:Label runat="server" ResourceKey="Log Records.Title" /> <asp:HyperLink runat="server" ID="AboutLogRecordsLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
    </legend>
    
    <engage:DashboardItem Id="NumberOfEventLogRecordsItem" runat="server" TitleResourceKey="NumberOfEventLogRecords.Text" /> 
    <engage:DashboardItem Id="NumberOfSiteLogRecordsItem" runat="server" TitleResourceKey="NumberOfSiteLogRecords.Text" /> 
    
    <engage:ModuleMessage runat="server" ID="AboutLogRecordsMessage" ResourceKey="Log Records.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>

<fieldset>
    <legend class="SubHead">
        <asp:Label runat="server" ResourceKey="Scheduled Processes.Title" /> <asp:HyperLink runat="server" ID="AboutScheduledProcessesLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
    </legend>

    <engage:DashboardItem Id="NumberOfFailedScheduleProcessesItem" runat="server" TitleResourceKey="NumberOfFailedScheduleProcesses.Text" HasDateRange="true" /> 
    
    <engage:ModuleMessage runat="server" ID="AboutScheduledProcessesMessage" ResourceKey="Scheduled Processes.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>

<fieldset>
    <legend class="SubHead">
        <asp:Label runat="server" ResourceKey="Unused Modules.Title" /> <asp:HyperLink runat="server" ID="AboutUnusedModulesLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" />
    </legend>

    <engage:DashboardItem Id="ModulesNotInUseItem" runat="server" TitleResourceKey="ModulesNotInUse.Text" DetailsPanelId="UnusedModulesPanel" /> 
    <asp:Panel ID="UnusedModulesPanel" runat="server" CssClass="detailsTableWrapper">
        <asp:GridView ID="ModulesNotInUseGridView" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:BoundField HeaderText="Friendly Name" DataField="FriendlyName"/>
                <asp:BoundField HeaderText="Version" DataField="Version"/>
                <asp:CheckBoxField HeaderText="Premium?" DataField="IsPremium" ReadOnly="true" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ResourceKey="None.Text" runat="server" />
            </EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>

    <engage:ModuleMessage runat="server" ID="AboutUnusedModulesMessage" ResourceKey="Unused Modules.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>
