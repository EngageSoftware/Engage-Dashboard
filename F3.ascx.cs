// <copyright file="F3.ascx.cs" company="Engage Software">
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
    using System.Web;
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// Searches Html/Text and Publish modules.
    /// </summary>
    public partial class F3 : ModuleBase
    {
        /// <summary>
        /// Gets the edit link for the given module.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="tabId">The tab id.</param>
        /// <returns>A URL to the edit page of the given module</returns>
        protected static string GetModuleEditLink(int moduleId, int tabId)
        {
            return Globals.NavigateURL(tabId, "edit", "mid=" + moduleId);
        }

        /// <summary>
        /// Gets a URL for the given publish item.
        /// </summary>
        /// <param name="itemId">The item id.</param>
        /// <returns>A URL for the given publish item</returns>
        protected static string GetPublishLink(int itemId)
        {
            return ApplicationUrl + "/desktopmodules/engagepublish/itemlink.aspx?itemId=" + itemId.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///  Encodes and trims the given text.
        /// </summary>
        /// <param name="text">The text to cleanup.</param>
        /// <returns>The given text, HTML encoded and trimmed to 500 characters.</returns>
        protected static string CleanupText(string text)
        {
            string returnVal = HttpUtility.HtmlEncode(text);
            if (returnVal.Length > 500)
            {
                return returnVal.Substring(0, 500);
            }
            else
            {
                return returnVal;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
            this.SearchButton.Click += this.SearchButton_Click;
            this.ReplaceButton.Click += this.ReplaceButton_Click;
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
                this.SetupAboutText();
                this.SearchTypeList.Visible = new DesktopModuleController().GetDesktopModuleByName("Engage: Publish") != null;
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
            this.AboutFindAndReplaceLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);
            this.AboutFindAndReplaceLink.Attributes.Add("OnClick", GetToggleBehavior(this.AboutFindAndReplaceMessage.ClientID));
        }

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = this.SearchValueTextBox.Text.Trim();
            bool searchPublish = this.SearchTypeList.SelectedValue == "Publish";

            if (searchPublish)
            {
                this.BindPublishData(searchTerm);
            }
            else
            {
                this.BindHtmlTextData(searchTerm);
            }

            this.ReplacementPanel.Visible = !searchPublish;
            this.ReplacementValueTextBox.Text = string.Empty;
            this.ReplacementResultsLabel.Text = string.Empty;

            this.ResultsGrid.Visible = !searchPublish;
            this.PublishResultsGrid.Visible = searchPublish;
        }

        /// <summary>
        /// Handles the Click event of the ReplaceButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catching Exception at highest level as a safeguard")]
        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            try
            {
                // loop through the Text/HTML modules and start updating fields
                string replacementString = this.ReplacementValueTextBox.Text.Trim();
                string searchString = this.SearchValueTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(replacementString) && !string.IsNullOrEmpty(searchString))
                {
                    int count = 0;
                    using (IDataReader dr = this.UserInfo.IsSuperUser 
                        ? DataProvider.Instance().GetMatchingHtmlTextModules(searchString) 
                        : DataProvider.Instance().GetMatchingHtmlTextModules(searchString, this.PortalId))
                    {
                        while (dr.Read())
                        {
                            DataProvider.Instance().ReplaceTextHtml(
                                Convert.ToInt32(dr["ModuleId"], CultureInfo.InvariantCulture), 
                                dr["DesktopHtml"].ToString().Replace(searchString, replacementString), 
                                dr["DesktopSummary"].ToString().Replace(searchString, replacementString), 
                                this.UserId);
                            count++;
                        }
                    }

                    string replacementResults = Localization.GetString("ReplacementResults", this.LocalResourceFile);
                    this.ReplacementResultsLabel.Text = String.Format(CultureInfo.CurrentCulture, replacementResults, searchString, replacementString, count);
                    this.ReplacementResultsLabel.Visible = true;
                    this.ReplacementPanel.Visible = false;
                    this.ResultsGrid.Visible = false;
                    this.PublishResultsGrid.Visible = false;
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Binds the HTML text data.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        private void BindHtmlTextData(string searchString)
        {
            if (this.UserInfo.IsSuperUser)
            {
                this.ResultsGrid.DataSource = DataProvider.Instance().GetMatchingHtmlTextModules(searchString);
                this.ResultsGrid.DataBind();
            }
            else
            {
                this.ResultsGrid.DataSource = DataProvider.Instance().GetMatchingHtmlTextModules(searchString, this.PortalId);
                this.ResultsGrid.DataBind();
            }

            this.ReplacementPanel.Visible = true;
        }

        /// <summary>
        /// Binds the publish data.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        private void BindPublishData(string searchString)
        {
            using (IDataReader publishLinks = DataProvider.Instance().GetPublishLinks(searchString, this.PortalId))
            {
                this.PublishResultsGrid.DataSource = publishLinks;
                this.PublishResultsGrid.DataBind();
            }
        }
    }
}