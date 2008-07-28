<%@ Import Namespace="Globals=DotNetNuke.Common.Globals" %>
<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.Admin" AutoEventWireup="false" CodeBehind="Admin.ascx.cs" %>
<%@ Register TagPrefix="engage" TagName="DashboardItem" Src="Controls/DashboardItem.ascx" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>

<fieldset>
    <legend class="SubHead"><asp:Label runat="server" ResourceKey="Users.Title" /> <asp:HyperLink runat="server" ID="AboutUsersLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" /></legend>

    <engage:DashboardItem Id="NumberOfUsersRegisteredItem" runat="server" TitleResourceKey="NumberOfUsersRegisteredItem.Text" HasDateRange="true" />
    <engage:DashboardItem Id="UniqueUsersLoggedInItem" runat="server" TitleResourceKey="UniqueUsersLoggedInItem.Text" HasDateRange="true" />
    <engage:DashboardItem Id="NumberOfRolesInPortalItem" runat="server" TitleResourceKey="NumberOfRolesInPortalItem.Text" NavigateLinkResourceKey="Security Roles Link.Text" />

    <engage:ModuleMessage runat="server" ID="AboutUsersMessage" ResourceKey="Users.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>

<fieldset>
    <legend class="SubHead"><asp:Label runat="server" ResourceKey="Pages and Content.Title" /> <asp:HyperLink runat="server" ID="AboutPagesAndContentLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" /></legend>

    <engage:DashboardItem Id="NumberOfPagesInPortalItem" runat="server" TitleResourceKey="NumberOfPagesInPortalItem.Text" NavigateLinkResourceKey="Pages Link.Text" />
    <engage:DashboardItem Id="NumberInRecycleBinItem" runat="server" TitleResourceKey="NumberInRecycleBinItem.Text" NavigateLinkResourceKey="Recycle Bin Link.Text" />
    
    <engage:DashboardItem Id="EmptyPagesItem" runat="server" TitleResourceKey="EmptyPagesItem.Text" DetailsPanelId="EmptyPagesPanel" />
    <asp:Panel ID="EmptyPagesPanel" runat="server" CssClass="detailsTableWrapper">
        <asp:GridView ID="EmptyPagesGridView" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="Tab ID">
                    <ItemTemplate>
                        <%# Eval("TabID") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Page Name">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# Globals.NavigateURL((int)Eval("TabId")) %>'>
                            <%# Eval("TabName") %>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><asp:Label ResourceKey="None.Text" runat="server" /></EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>

    <engage:DashboardItem Id="AdministratorPagesItem" runat="server" TitleResourceKey="AdministratorPagesItem.Text" DetailsPanelId="AdministratorPagesPanel" />
    <asp:Panel ID="AdministratorPagesPanel" runat="server" CssClass="detailsTableWrapper">
        <asp:GridView ID="AdministratorPagesGridView" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="Tab ID">
                    <ItemTemplate>
                        <%# Eval("TabId") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Page Name">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# Globals.NavigateURL((int)Eval("TabId")) %>'>
                            <%# Eval("TabName") %>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><asp:Label ResourceKey="None.Text" runat="server" /></EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>

    <engage:DashboardItem Id="AdministratorModulesItem" runat="server" TitleResourceKey="AdministratorModulesItem.Text" DetailsPanelId="AdministratorModulesPanel" />
    <asp:Panel ID="AdministratorModulesPanel" runat="server" CssClass="detailsTableWrapper">
        <asp:GridView ID="AdministratorModulesGridView" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="Tab ID">
                    <ItemTemplate>
                        <%# Eval("TabId") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Page Link">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# Globals.NavigateURL((int)Eval("TabId")) %>'>
                            <%# Eval("TabName") %>
                        </asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit Module">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# GetModuleEditUrl((int)Eval("ModuleId"), (int)Eval("TabId"))%>'>
                            <%# Eval("ModuleTitle") %>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><asp:Label ResourceKey="None.Text" runat="server" /></EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>

    <engage:ModuleMessage runat="server" ID="AboutPagesAndContentMessage" ResourceKey="Pages and Content.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>

<fieldset>
    <legend class="SubHead"><asp:Label runat="server" ResourceKey="Search Engine Optimization.Title" /> <asp:HyperLink runat="server" id="AboutSeoLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" /></legend>

    <engage:DashboardItem Id="PagesWithoutDescriptionItem" runat="server" TitleResourceKey="PagesWithoutDescriptionItem.Text" DetailsPanelId="PagesWithoutDescriptionPanel" />
    <asp:Panel ID="PagesWithoutDescriptionPanel" runat="server" CssClass="detailsTableWrapper">
        <asp:GridView ID="PagesWithoutDescriptionGridView" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None" >
            <Columns>
                <asp:TemplateField HeaderText="Tab ID">
                    <ItemTemplate>
                        <%# Eval("TabId") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Page Settings">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# GetPageEditUrl((int)Eval("TabId")) %>'>
                            <%# Eval("TabName") %>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><asp:Label ResourceKey="None.Text" runat="server" /></EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>
    
    <engage:DashboardItem Id="PagesWithoutKeywordsItem" runat="server" TitleResourceKey="PagesWithoutKeywordsItem.Text" DetailsPanelId="PagesWithoutKeywordsPanel" />
    <asp:Panel ID="PagesWithoutKeywordsPanel" runat="server" CssClass="detailsTableWrapper">
        <asp:GridView ID="PagesWithoutKeywordsGridView" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="Tab ID">
                    <ItemTemplate>
                        <%# Eval("TabId") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Page Settings">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# GetPageEditUrl((int)Eval("TabId")) %>'>
                            <%# Eval("TabName") %>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><asp:Label ResourceKey="None.Text" runat="server" /></EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>

    <engage:DashboardItem Id="TextModulesWithoutSummaryItem" runat="server" TitleResourceKey="TextModulesWithoutSummaryItem.Text" DetailsPanelId="TextModulesWithoutSummaryPanel" />
    <asp:Panel ID="TextModulesWithoutSummaryPanel" runat="server" CssClass="detailsTableWrapper">
        <asp:GridView ID="TextModulesWithoutSummaryGridView" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="Tab ID">
                    <ItemTemplate>
                        <%# Eval("TabId") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Page Link">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# Globals.NavigateURL((int)Eval("TabId")) %>'>
                            <%# Eval("TabName") %>
                        </asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit Module">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# GetModuleEditUrl((int)Eval("ModuleId"), (int)Eval("TabId"))%>'>
                            <%# Eval("ModuleTitle") %>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate><asp:Label ResourceKey="None.Text" runat="server" /></EmptyDataTemplate>
        </asp:GridView>
    </asp:Panel>
       
    <engage:ModuleMessage runat="server" ID="AboutSeoMessage" ResourceKey="Search Engine Optimization.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>
