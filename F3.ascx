<%@ Control Language="C#" Inherits="Engage.Dnn.Dashboard.F3" AutoEventWireup="false" CodeBehind="F3.ascx.cs" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="Controls/ModuleMessage.ascx" %>

<fieldset>
    <legend class="SubHead"><asp:Label runat="server" ResourceKey="Find And Replace.Title" /> <asp:HyperLink runat="server" ID="AboutFindAndReplaceLink" NavigateUrl="javascript:void(0);" CssClass="CommandButton" /></legend>

    <div id="engageF3" class="Normal">
        <div class="f3SearchTypeList>
            <asp:RadioButtonList ID="SearchTypeList" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                <asp:ListItem ResourceKey="Text/HTML" Value="Text/HTML" Selected="True" />
                <asp:ListItem ResourceKey="Publish" Value="Publish" />
            </asp:RadioButtonList>
        </div>
        <asp:Label id="SearchValueLabel" resourcekey="SearchValueLabel" Runat="server" />
        <asp:TextBox ID="SearchValueTextBox" runat="server" />
        <asp:Button ID="SearchButton" resourcekey="SearchButton" runat="server" />
        <asp:GridView ID="ResultsGrid" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label resourcekey="PageNameHeader" runat="server"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("TabName") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label resourcekey="ModuleTitleHeader" runat="server"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ModuleTitle") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label resourcekey="ModuleTextHeader" runat="server"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# CleanupText(Eval("DesktopHtml").ToString()) %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label resourcekey="EditHeader" runat="server"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HyperLink Target="_blank" NavigateUrl='<%# GetModuleEditLink((int)Eval("ModuleId"), (int)Eval("TabId"))%>' resourcekey="EditLink" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="PublishResultsGrid" runat="server" AutoGenerateColumns="false" CssClass="detailsTable Normal" AlternatingRowStyle-CssClass="detailsTableAltRow" HeaderStyle-CssClass="detailsTableHeader" RowStyle-CssClass="detailsTableRow" GridLines="None">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label resourcekey="ItemIdHeader" runat="server"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ItemId") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label resourcekey="ArticleTitleHeader" runat="server"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("Name") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label resourcekey="ModuleTextHeader" runat="server"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# CleanupText((string)Eval("ArticleText")) %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label resourcekey="EditPublishHeader" runat="server"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HyperLink Target="_blank" NavigateUrl='<%# GetPublishLink((int)Eval("ItemID"))%>' resourcekey="EditLink" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Panel ID="ReplacementPanel" runat="server" Visible="false">
            <asp:Label id="HtmlReplaceLabel" runat="server" ResourceKey="HtmlReplaceLabel" />
            <asp:TextBox ID="ReplacementValueTextBox" runat="server"/>
            <asp:Button ID="ReplaceButton" resourcekey="ReplaceButton" runat="server" />
        </asp:Panel>
        <asp:Label ID="ReplacementResultsLabel" CssClass="NormalBold" runat="server" Visible="false" />
    </div>
    
    <engage:ModuleMessage runat="server" ID="AboutFindAndReplaceMessage" ResourceKey="Find And Replace.Text" CssClass="Normal" MessageType="Information" style="display:none;" />
</fieldset>
