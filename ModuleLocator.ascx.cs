// <copyright file="ModuleLocator.ascx.cs" company="Engage Software">
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
    using System.Web.UI.WebControls;
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using Telerik.Web.UI;

    /// <summary>
    /// Displays which pages a particular module is loaded on, with links to the page and the page's settings.
    /// Also displays module and tab module settings.
    /// </summary>
    public partial class ModuleLocator : ModuleBase
    {
        /// <summary>
        /// Gets the URL to the settings page of the given module.
        /// </summary>
        /// <param name="tabId">The tab id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>A URL to the settings page of the given module.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Usage is ASP.NET markup, shouldn't be strongly typed")]
        protected static string GetSettingsUrl(int tabId, int moduleId)
        {
            return Globals.NavigateURL(tabId, "Module", "ModuleId=" + moduleId.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
            this.ModuleComboBox.SelectedIndexChanged += this.ModuleComboBox_SelectedIndexChanged;
            this.ModuleTabsGrid.RowDataBound += this.ModuleTabsGrid_RowDataBound;
            this.ModuleTabsGrid.DataBinding += this.ModuleTabsGrid_DataBinding;
        }

        /// <summary>
        /// Loads the module drop down.
        /// </summary>
        protected void LoadModuleDropDown()
        {
            this.ModuleComboBox.DataSource = new DesktopModuleController().GetDesktopModulesByPortal(this.PortalId);
            this.ModuleComboBox.DataValueField = "DesktopModuleID";
            this.ModuleComboBox.DataTextField = "FriendlyName";
            this.ModuleComboBox.DataBind();
            this.ModuleComboBox.Items.Insert(0, new RadComboBoxItem(Localization.GetString("SelectAModule.Text", LocalResourceFile)));
        }

        /// <summary>
        /// Displays the module info.
        /// </summary>
        /// <param name="desktopModuleId">The desktop module id.</param>
        protected void DisplayModuleInfo(int desktopModuleId)
        {
            using (IDataReader tabsForModule = DataProvider.Instance().GetTabsForModule(desktopModuleId, this.PortalId))
            {
                this.ModuleTabsGrid.DataSource = tabsForModule;
                this.ModuleTabsGrid.DataBind();
            }
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
                    this.SetupAboutText();

                    if (!this.IsPostBack)
                    {
                        this.LoadModuleDropDown();
                    }
                }
                catch (Exception exc)
                {
                    Exceptions.ProcessModuleLoadException(this, exc);
                }
        }

        /// <summary>
        /// Sets up the visibility toggle behavior for the about text links.
        /// </summary>
        private void SetupAboutText()
        {
            this.AboutModuleSettingsLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);
            this.AboutModuleSettingsLink.Attributes.Add("OnClick", GetToggleBehavior(this.aboutModuleSettingsMessage.ClientID));
        }

        /// <summary>
        /// Localizes the grids on this control.
        /// </summary>
        private void LocalizeGrids()
        {
            if (!this.IsPostBack)
            {
                Utility.LocalizeGridView(ref this.ModuleTabsGrid, this.LocalResourceFile);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ModulesDropDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleComboBox_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (this.ModuleComboBox.SelectedIndex > 0)
            {
                this.DisplayModuleInfo(Convert.ToInt32(this.ModuleComboBox.SelectedValue, CultureInfo.InvariantCulture));
            }

            this.ModuleTabsPanel.Visible = (this.ModuleComboBox.SelectedIndex > 0);
            this.ModuleMessageLabel.Visible = (this.ModuleTabsGrid.Rows.Count == 0) && (this.ModuleComboBox.SelectedIndex > 0);
        }

        /// <summary>
        /// Handles the DataBinding event of the ModuleTabsGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ModuleTabsGrid_DataBinding(object sender, EventArgs e)
        {
            this.ModuleTabsGrid.Columns[4].Visible = false;
            this.ModuleTabsGrid.Columns[5].Visible = false;
        }

        /// <summary>
        /// Handles the RowDataBound event of the ModuleTabsGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        private void ModuleTabsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView moduleSettingsGrid = (GridView)e.Row.FindControl("ModuleSettingsGrid");
                GridView tabModuleSettingsGrid = (GridView)e.Row.FindControl("TabModuleSettingsGrid");
                HiddenField moduleIdHiddenField = (HiddenField)e.Row.FindControl("ModuleIdHiddenField");
                HiddenField tabModuleIdHiddenField = (HiddenField)e.Row.FindControl("TabModuleIdHiddenField");
                DashboardItem tabModuleSettingsItem = (DashboardItem)e.Row.FindControl("TabModuleSettingsItem");
                DashboardItem moduleSettingsItem = (DashboardItem)e.Row.FindControl("ModuleSettingsItem");

                Utility.LocalizeGridView(ref tabModuleSettingsGrid, this.LocalResourceFile);
                Utility.LocalizeGridView(ref moduleSettingsGrid, this.LocalResourceFile);

                using (IDataReader tabModuleSettings = DataProvider.Instance().GetTabModuleSettings(Convert.ToInt32(tabModuleIdHiddenField.Value, CultureInfo.InvariantCulture)))
                {
                    tabModuleSettingsGrid.DataSource = tabModuleSettings;
                    tabModuleSettingsGrid.DataBind();

                    tabModuleSettingsItem.SetValue(tabModuleSettingsGrid.Rows.Count);
                    if (tabModuleSettingsGrid.Rows.Count > 0)
                    {
                        this.ModuleTabsGrid.Columns[4].Visible = true;
                    }
                }

                using (IDataReader moduleSettings = DataProvider.Instance().GetModuleSettings(Convert.ToInt32(moduleIdHiddenField.Value, CultureInfo.InvariantCulture)))
                {
                    moduleSettingsGrid.DataSource = moduleSettings;
                    moduleSettingsGrid.DataBind();

                    moduleSettingsItem.SetValue(moduleSettingsGrid.Rows.Count);
                    if (moduleSettingsGrid.Rows.Count > 0)
                    {
                        this.ModuleTabsGrid.Columns[5].Visible = true;
                    }
                }
            }
        }
    }
}