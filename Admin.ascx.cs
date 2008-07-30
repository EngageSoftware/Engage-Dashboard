// <copyright file="Admin.ascx.cs" company="Engage Software">
// Engage: Dashboard - http://www.engagemodules.com
// Copyright (c) 2004-2008
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.Dashboard
{
    using System;
    using System.Data;
    using System.Globalization;
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// Displays admin-level statistics for the current portal.
    /// </summary>
    public partial class Admin : ModuleBase
    {
        /// <summary>
        /// Gets the URL to the edit control key of the given module.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="tabId">The tab Id.</param>
        /// <returns>The URL to the edit control key of the given module</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Usage is ASP.NET markup, shouldn't be strongly typed")]
        protected static string GetModuleEditUrl(int moduleId, int tabId)
        {
            return Globals.NavigateURL(tabId, "edit", "&mid=" + moduleId.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets the URL to edit the page with given tab ID.
        /// </summary>
        /// <param name="tabId">The tab id of the page to create the edit link for.</param>
        /// <returns>The URL to edit the page with given tab ID.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Usage is ASP.NET markup, shouldn't be strongly typed")]
        protected static string GetPageEditUrl(int tabId)
        {
            return Globals.NavigateURL(tabId, "Tab", "action=edit");
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catching Exception at highest level as a safeguard")]
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.LocalizeGrids();
                this.SetupAboutLinks();

                SetupDateRangeControls(this.NumberOfUsersRegisteredItem);
                SetupDateRangeControls(this.UniqueUsersLoggedInItem);
                
                this.FillData();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        private void FillData()
        {
            this.NumberOfUsersRegisteredItem.SetValue(DataProvider.Instance().GetNumberOfUserRegistrationsInDateSpan(
                this.NumberOfUsersRegisteredItem.BeginDate.Value, this.NumberOfUsersRegisteredItem.EndDate.Value, this.PortalId));

            this.UniqueUsersLoggedInItem.SetValue(DataProvider.Instance().GetNumberOfUserLoginsInDateSpan(
                this.UniqueUsersLoggedInItem.BeginDate.Value, this.UniqueUsersLoggedInItem.EndDate.Value, this.PortalId));

            this.NumberOfPagesInPortalItem.NavigateUrl = this.GetUrlForModule("Tabs");
            this.NumberOfRolesInPortalItem.NavigateUrl = this.GetUrlForModule("Security Roles");
            this.NumberInRecycleBinItem.NavigateUrl = this.GetUrlForModule("Recycle Bin");

            this.NumberOfPagesInPortalItem.SetValue(DataProvider.Instance().CountPages(this.PortalId));
            this.NumberOfRolesInPortalItem.SetValue(DataProvider.Instance().CountRoles(this.PortalId));
            this.NumberInRecycleBinItem.SetValue(DataProvider.Instance().CountRecycleBin(this.PortalId));

            using (IDataReader pagesWithoutDescription = DataProvider.Instance().GetPagesWithoutDescription(this.PortalId))
            {
                this.PagesWithoutDescriptionGridView.DataSource = pagesWithoutDescription;
                this.PagesWithoutDescriptionGridView.DataBind();
                this.PagesWithoutDescriptionItem.SetValue(this.PagesWithoutDescriptionGridView.Rows.Count);
            }

            using (IDataReader pagesWithoutKeywords = DataProvider.Instance().GetPagesWithoutKeywords(this.PortalId))
            {
                this.PagesWithoutKeywordsGridView.DataSource = pagesWithoutKeywords;
                this.PagesWithoutKeywordsGridView.DataBind();
                this.PagesWithoutKeywordsItem.SetValue(this.PagesWithoutKeywordsGridView.Rows.Count);
            }

            using (IDataReader emptyPages = DataProvider.Instance().GetEmptyPages(this.PortalId))
            {
                this.EmptyPagesGridView.DataSource = emptyPages;
                this.EmptyPagesGridView.DataBind();
                this.EmptyPagesItem.SetValue(this.EmptyPagesGridView.Rows.Count);
            }

            using (IDataReader htmlTextModulesWithoutSummary = DataProvider.Instance().GetHtmlTextModulesWithoutSummary(this.PortalId))
            {
                this.TextModulesWithoutSummaryGridView.DataSource = htmlTextModulesWithoutSummary;
                this.TextModulesWithoutSummaryGridView.DataBind();
                this.TextModulesWithoutSummaryItem.SetValue(this.TextModulesWithoutSummaryGridView.Rows.Count);
            }

            using (IDataReader adminOnlyModules = DataProvider.Instance().GetAdminOnlyModules(this.PortalId))
            {
                this.AdministratorModulesGridView.DataSource = adminOnlyModules;
                this.AdministratorModulesGridView.DataBind();
                this.AdministratorModulesItem.SetValue(this.AdministratorModulesGridView.Rows.Count);
            }

            using (IDataReader adminOnlyPages = DataProvider.Instance().GetAdminOnlyPages(this.PortalId))
            {
                this.AdministratorPagesGridView.DataSource = adminOnlyPages;
                this.AdministratorPagesGridView.DataBind();
                this.AdministratorPagesItem.SetValue(this.AdministratorPagesGridView.Rows.Count);
            }
        }

        /// <summary>
        /// Sets up the visibility toggle behavior for the about text links.
        /// </summary>
        private void SetupAboutLinks()
        {
            this.AboutUsersLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);
            this.AboutPagesAndContentLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);
            this.AboutSeoLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);

            this.AboutUsersLink.Attributes.Add("OnClick", GetToggleBehavior(this.AboutUsersMessage.ClientID));
            this.AboutPagesAndContentLink.Attributes.Add("OnClick", GetToggleBehavior(this.AboutPagesAndContentMessage.ClientID));
            this.AboutSeoLink.Attributes.Add("OnClick", GetToggleBehavior(this.AboutSeoMessage.ClientID));
        }

        /// <summary>
        /// Gets a URL to a page the given module is on for this portal.
        /// </summary>
        /// <param name="moduleDefinitionFriendlyName">The friendly name of the module definition for the module to which to link.</param>
        /// <returns>A URL to a page the given module is on for this portal</returns>
        private string GetUrlForModule(string moduleDefinitionFriendlyName)
        {
            ModuleInfo module = new ModuleController().GetModuleByDefinition(this.PortalId, moduleDefinitionFriendlyName);
            return module != null ? Globals.NavigateURL(module.TabID) : string.Empty;
        }

        /// <summary>
        /// Localizes the grids on this control.
        /// </summary>
        private void LocalizeGrids()
        {
            if (!this.IsPostBack)
            {
                Utility.LocalizeGridView(ref this.AdministratorModulesGridView, this.LocalResourceFile);
                Utility.LocalizeGridView(ref this.AdministratorPagesGridView, this.LocalResourceFile);
                Utility.LocalizeGridView(ref this.EmptyPagesGridView, this.LocalResourceFile);
                Utility.LocalizeGridView(ref this.TextModulesWithoutSummaryGridView, this.LocalResourceFile);
                Utility.LocalizeGridView(ref this.PagesWithoutDescriptionGridView, this.LocalResourceFile);
                Utility.LocalizeGridView(ref this.PagesWithoutKeywordsGridView, this.LocalResourceFile);
            }
        }
    }
}
