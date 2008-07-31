// <copyright file="DashboardItem.ascx.cs" company="Engage Software">
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
    using System.Diagnostics;
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// The display of a dashboard statistic
    /// </summary>
    public partial class DashboardItem : ModuleBase
    {
        /// <summary>
        /// Backing field for <see cref="Title"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string title;

        /// <summary>
        /// Backing field for <see cref="TitleResourceKey"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string titleResourceKey;

        /// <summary>
        /// Backing field for <see cref="Value"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string value;

        /// <summary>
        /// Backing field for <see cref="CssClass"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string cssClass;

        /// <summary>
        /// Backing field for <see cref="HasDateRange"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool hasDateRange;

        /// <summary>
        /// Backing field for <see cref="DetailsPanelId"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string detailsPanelId;

        /// <summary>
        /// Backing field for <see cref="NavigateUrl"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string navigateUrl;

        /// <summary>
        /// Backing field for <see cref="NavigateLinkText"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string navigateLinkText;

        /// <summary>
        /// Backing field for <see cref="NavigateLinkResourceKey"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string navigateLinkResourceKey;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title of the section.</value>
        public string Title
        {
            [DebuggerStepThrough]
            get { return this.title; }
            [DebuggerStepThrough]
            set { this.title = value; }
        }

        /// <summary>
        /// Gets or sets the title resource key.
        /// </summary>
        /// <value>The title resource key.</value>
        public string TitleResourceKey
        {
            [DebuggerStepThrough]
            get { return this.titleResourceKey; }
            [DebuggerStepThrough]
            set { this.titleResourceKey = value; }
        }

        /// <summary>
        /// Gets the main text to display.
        /// </summary>
        /// <value>The main text to display.</value>
        public string Value
        {
            [DebuggerStepThrough]
            get { return this.value; }
        }

        /// <summary>
        /// Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.
        /// </summary>
        /// <value>The CSS class rendered by the Web server control on the client.</value>
        public string CssClass
        {
            [DebuggerStepThrough]
            get { return this.cssClass; }
            [DebuggerStepThrough]
            set { this.cssClass = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance renders a date range.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance renders a date range; otherwise, <c>false</c>.
        /// </value>
        public bool HasDateRange
        {
            [DebuggerStepThrough]
            get { return this.hasDateRange; }
            [DebuggerStepThrough]
            set { this.hasDateRange = value; }
        }

        /// <summary>
        /// Gets or sets the ID of the associated details <see cref="Panel"/> for this <see cref="DashboardItem"/>, if it exists.  If the value is <c>null</c> or <see cref="string.Empty"/>, no link will be displayed.
        /// </summary>
        /// <value>The details <see cref="Panel"/> ID.</value>
        public string DetailsPanelId
        {
            [DebuggerStepThrough]
            get { return this.detailsPanelId; }
            [DebuggerStepThrough]
            set { this.detailsPanelId = value; }
        }

        /// <summary>
        /// Gets or sets the begin date.
        /// </summary>
        /// <value>The begin date.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public DateTime? BeginDate
        {
            [DebuggerStepThrough]
            get { return this.BeginDatePicker.SelectedDate; }
            [DebuggerStepThrough]
            set { this.BeginDatePicker.SelectedDate = value; }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public DateTime? EndDate
        {
            [DebuggerStepThrough]
            get { return this.EndDatePicker.SelectedDate; }
            [DebuggerStepThrough]
            set { this.EndDatePicker.SelectedDate = value; }
        }

        /// <summary>
        /// Gets or sets the URL to link to for this control's link.  If the value is <c>null</c> or <see cref="string.Empty"/>, no link will be displayed.
        /// </summary>
        /// <value>The URL to link to for this control's link. If the value is <c>null</c> or <see cref="string.Empty"/>, no link will be displayed.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Mimicking HyperLink's API")]
        public string NavigateUrl
        {
            [DebuggerStepThrough]
            get { return this.navigateUrl; }
            [DebuggerStepThrough]
            set { this.navigateUrl = value; }
        }

        /// <summary>
        /// Gets or sets the text for the navigate link.
        /// </summary>
        /// <value>The text for the navigate link</value>
        public string NavigateLinkText
        {
            [DebuggerStepThrough]
            get { return this.navigateLinkText; }
            [DebuggerStepThrough]
            set { this.navigateLinkText = value; }
        }

        /// <summary>
        /// Gets or sets the resource key for the navigate link.
        /// </summary>
        /// <value>The resource key for the navigate link</value>
        public string NavigateLinkResourceKey
        {
            [DebuggerStepThrough]
            get { return this.navigateLinkResourceKey; }
            [DebuggerStepThrough]
            set { this.navigateLinkResourceKey = value; }
        }

        /// <summary>
        /// Gets the id of the span which hold the value field.
        /// </summary>
        /// <value>The id of the span which hold the value field</value>
        protected string ValueId
        {
            get { return this.ClientID + this.ClientIDSeparator + "value"; }
        }

        /// <summary>
        /// Sets <see cref="Value"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetValue(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Sets <see cref="Value"/>.  Hides the details panel if appropriate.
        /// </summary>
        /// <param name="count">The count.</param>
        public void SetValue(int count)
        {
            this.SetValue(count.ToString(CultureInfo.CurrentCulture));
            if (count == 0)
            {
                Panel detailsPanel = this.GetDetailsPanel();
                if (detailsPanel != null)
                {
                    detailsPanel.Visible = false;
                }

                this.DetailsPanelId = null;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catching Exception at highest level as a safeguard")]
        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);

                // since the global navigation control is not loaded using DNN mechanisms we need to set it here so that calls to 
                // module related information will appear the same as the actual control this navigation is sitting on.hk
                this.ModuleConfiguration = this.ParentPortalModuleBase.ModuleConfiguration;
                this.LocalResourceFile = "~" + DesktopModuleFolderName + "Controls/App_LocalResources/DashboardItem";

                this.PreRender += this.Page_PreRender;
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the <c>PreRender</c> event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// The call to <see cref="SetupDetails"/> needs to take place in the <c>PreRender</c> event, so that <see cref="DetailsPanelId"/> can be set from a DataBind event.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catching Exception at highest level as a safeguard")]
        private void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                this.TitleLiteral.Text = Engage.Utility.HasValue(this.TitleResourceKey) ? Localization.GetString(this.TitleResourceKey, this.ParentPortalModuleBase.LocalResourceFile) : this.Title;

                this.SetupDetails();
                this.SetupDateRange();
                this.SetupNavigateLink();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Sets up the details panel.
        /// </summary>
        private void SetupDetails()
        {
            if (Engage.Utility.HasValue(this.DetailsPanelId))
            {
                Panel detailsPanel = this.GetDetailsPanel();
                detailsPanel.Style[HtmlTextWriterStyle.Display] = "none";
                this.DetailsLink.Attributes.Add("OnClick", GetToggleBehavior(detailsPanel.ClientID, ShowDetails, HideDetails));
                this.DetailsLink.Text = Localization.GetString("DetailsLink.Text", DashboardResourceFile);
                this.DetailsLink.Visible = true;
            }
        }

        /// <summary>
        /// Sets up the date range controls.
        /// </summary>
        private void SetupDateRange()
        {
            if (this.HasDateRange)
            {
                this.TitleLiteral.Text = string.Format(
                    CultureInfo.CurrentCulture,
                    this.TitleLiteral.Text,
                    "<span class='DisplayDate'>" + this.BeginDatePicker.SelectedDate.Value.ToShortDateString() + "</span>",
                    "<span class='DisplayDate'>" + this.EndDatePicker.SelectedDate.Value.ToShortDateString() + "</span>");
                this.DateRangeLink.Attributes.Add("OnClick", GetToggleBehavior(this.DateRangePanel.ClientID, ShowDateRange, HideDateRange));
                this.DateRangeLink.Text = Localization.GetString("DateRangeLink.Text", DashboardResourceFile);
                this.DateRangeLink.Visible = true;
                this.DateRangePanel.Visible = true;
            }
        }

        /// <summary>
        /// Sets up the navigate link.
        /// </summary>
        private void SetupNavigateLink()
        {
            if (Engage.Utility.HasValue(this.NavigateUrl))
            {
                this.NavigateLink.Visible = true;
                this.NavigateLink.NavigateUrl = this.NavigateUrl;
                this.NavigateLink.Text = Engage.Utility.HasValue(this.NavigateLinkResourceKey) ? Localization.GetStringUrl(this.NavigateLinkResourceKey, this.ParentPortalModuleBase.LocalResourceFile) : this.NavigateLinkText;
            }
        }

        /// <summary>
        /// Gets the details panel based on <see cref="DetailsPanelId"/>.
        /// </summary>
        /// <returns>The details panel for this control</returns>
        private Panel GetDetailsPanel()
        {
            return this.DetailsPanelId != null ? (Panel)this.Parent.FindControl(this.DetailsPanelId) : null;
        }
    }
}