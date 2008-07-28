<%@ Import Namespace="System.Globalization"%>
<%@ Import Namespace="Globals=DotNetNuke.Common.Globals"%>
<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.ModuleLocator" AutoEventWireup="False" CodeBehind="ModuleLocator.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="engage" TagName="DashboardItem" Src="Controls/DashboardItem.ascx" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>

<fieldset>
    <legend class="SubHead"><asp:Label runat="server" ResourceKey="About Module Settings.Title" /> <asp:HyperLink runat="server" ID="AboutModuleSettingsLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" /></legend>

    <telerik:radcombobox ID="ModuleComboBox" runat="server" Width="50%" AllowCustomText="false" MarkFirstMatch="true" AutoPostBack="True"  />
    <asp:Panel ID="ModuleTabsPanel" runat="server" CssClass="detailsTableWrapper">
        <asp:GridView ID="ModuleTabsGrid" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:BoundField DataField="TabId" HeaderText="Tab ID" ItemStyle-HorizontalAlign="Center"/>
                <asp:TemplateField HeaderText="Page Link" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:HiddenField ID="ModuleIdHiddenField" runat="server" Value='<%# ((int)Eval("ModuleID")).ToString(CultureInfo.InvariantCulture) %>' /> 
                        <asp:HiddenField ID="TabModuleIdHiddenField" runat="server" Value='<%# ((int)Eval("TabModuleID")).ToString(CultureInfo.InvariantCulture) %>' /> 
                        <asp:HyperLink runat="server" NavigateUrl='<%# Globals.NavigateURL((int)Eval("TabId")) %>'>
                            <%# Eval("TabName") %>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Module Title" DataField="ModuleTitle" />
                <asp:TemplateField HeaderText="Edit Module Settings" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# GetSettingsUrl((int)Eval("TabId"), (int)Eval("ModuleId")) %>' ImageUrl="~/images/action_settings.gif" ResourceKey="ModuleSettings.Alt"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tab Module Settings" Visible="false">
                    <ItemTemplate>
                        <engage:DashboardItem ID="TabModuleSettingsItem" runat="server" TitleResourceKey="TabModuleSettingsItem" DetailsPanelId="TabModuleSettingsPanel" />
                        <asp:Panel ID="TabModuleSettingsPanel" runat="server" CssClass="detailsTableWrapper">
                            <asp:GridView ID="TabModuleSettingsGrid" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderText="Setting" DataField="SettingName"/>
                                    <asp:BoundField HeaderText="Value" DataField="SettingValue"/>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Module Settings" Visible="false">
                    <ItemTemplate>
                        <engage:DashboardItem ID="ModuleSettingsItem" runat="server" TitleResourceKey="ModuleSettingsItem" DetailsPanelId="ModuleSettingsPanel" />
                        <asp:Panel ID="ModuleSettingsPanel" runat="server" CssClass="detailsTableWrapper">
                            <asp:GridView ID="ModuleSettingsGrid" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderText="Setting" DataField="SettingName"/>
                                    <asp:BoundField HeaderText="Value" DataField="SettingValue"/>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Label ID="ModuleMessageLabel" runat="server" ResourceKey="Ununsed.Text" Visible="False"/>
    
    <engage:ModuleMessage runat="server" ID="aboutModuleSettingsMessage" ResourceKey="About Module Settings.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>
