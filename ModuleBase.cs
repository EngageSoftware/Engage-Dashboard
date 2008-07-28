// <copyright file="ModuleBase.cs" company="Engage Software">
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
    using System.Globalization;
    using System.Web;
    using DotNetNuke.Entities.Host;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Modules.Actions;
    using DotNetNuke.Framework;
    using DotNetNuke.Security;
    using DotNetNuke.Services.Localization;
    using Globals = DotNetNuke.Common.Globals;

    /// <summary>
    /// Contains base functionality 
    /// </summary>
    public class ModuleBase : PortalModuleBase, IActionable
    {
        /// <summary>
        /// Path to the DesktopModules folder for this module.  
        /// Add ~ to be beginning if you want ASP.NET to make it fully relative.
        /// Starts and ends with path separators, that is, "/".
        /// </summary>
        protected const string DesktopModuleFolderName = "/DesktopModules/EngageDashboard/";

        /// <summary>
        /// Path to the resource file for resources that apply to multiple controls within this module.
        /// </summary>
        protected const string DashboardResourceFile = "~" + DesktopModuleFolderName + "App_LocalResources/Dashboard.resx";

        /// <summary>
        /// Name of JavaScript variable registered by <see cref="RegisterScripts"/> which holds the text to display when a <see cref="DashboardItem"/> Date Range is hidden.
        /// </summary>
        protected const string ShowDateRange = "showDateRange";

        /// <summary>
        /// Name of JavaScript variable registered by <see cref="RegisterScripts"/> which holds the text to display when a <see cref="DashboardItem"/> Details Panel is hidden.
        /// </summary>
        protected const string ShowDetails = "showDetails";

        /// <summary>
        /// Name of JavaScript variable registered by <see cref="RegisterScripts"/> which holds the text to display when a <see cref="DashboardItem"/> Date Range is shown.
        /// </summary>
        protected const string HideDateRange = "hideDateRange";

        /// <summary>
        /// Name of JavaScript variable registered by <see cref="RegisterScripts"/> which holds the text to display when a <see cref="DashboardItem"/> Details Panel is shown.
        /// </summary>
        protected const string HideDetails = "hideDetails";

        /// <summary>
        /// Path to the folder that contains JavaScript in this module
        /// </summary>
        private const string JavaScriptResourcePath = "Engage.Dnn.Dashboard.JavaScript.";

        /// <summary>
        /// The URL to the JQuery library script.
        /// </summary>
        private const string JQueryResourcePath = "http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js";

        /// <summary>
        /// The path to the embedded resource of the JavaScript file for this module.
        /// </summary>
        private const string DashboardScriptResourcePath = JavaScriptResourcePath + "EngageDashboard.js";

        /// <summary>
        /// Name of JavaScript variable registered by <see cref="RegisterScripts"/> which holds the text to display when explanatory text is hidden.
        /// </summary>
        private const string ShowExplanatoryText = "showExplanatoryText";

        /// <summary>
        /// Name of JavaScript variable registered by <see cref="RegisterScripts"/> which holds the text to display when explanatory text is shown.
        /// </summary>
        private const string HideExplanatoryText = "hideExplanatoryText";

        /// <summary>
        /// Gets the module actions.
        /// </summary>
        /// <value>The module actions.</value>
        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection actions = new ModuleActionCollection();

                // Add Online Help Action
                if (HostSettings.GetHostSetting("EnableModuleOnLineHelp") == "Y" && Engage.Utility.HasValue(this.ModuleConfiguration.HelpUrl))
                {
                    ModuleAction helpAction = new ModuleAction(this.GetNextActionID());
                    helpAction.Title = Localization.GetString(ModuleActionType.OnlineHelp, Localization.GlobalResourceFile);
                    helpAction.CommandName = ModuleActionType.OnlineHelp;
                    helpAction.CommandArgument = string.Empty;
                    helpAction.Icon = "action_help.gif";
                    helpAction.Url = Globals.FormatHelpUrl(this.ModuleConfiguration.HelpUrl, this.PortalSettings, this.ModuleConfiguration.FriendlyName);
                    helpAction.Secure = SecurityAccessLevel.Edit;
                    helpAction.UseActionEvent = true;
                    helpAction.Visible = true;
                    helpAction.NewWindow = true;
                    actions.Add(helpAction);
                }

                return actions;
            }
        }

        /// <summary>
        /// Gets the base URL for this application.
        /// </summary>
        /// <value>The application URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Usage is ASP.NET markup or NavigateUrl, shouldn't be strongly typed")]
        protected static string ApplicationUrl
        {
            get 
            {
                return HttpContext.Current.Request.ApplicationPath == "/" ? string.Empty : HttpContext.Current.Request.ApplicationPath;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current user has edit rights to this module.
        /// </summary>
        /// <value><c>true</c> if the current user can edit the module; otherwise, <c>false</c>.</value>
        protected bool IsAdmin
        {
            get
            {
                return this.Request.IsAuthenticated && PortalSecurity.HasEditPermissions(this.ModuleId, this.TabId);
            }
        }

        /// <summary>
        /// Sets up the date range controls to the default of one month ago to today, if the date range is not already set.
        /// </summary>
        /// <param name="dashboardItem">A <see cref="DashboardItem"/> with a date range</param>
        protected static void SetupDateRangeControls(DashboardItem dashboardItem)
        {
            dashboardItem.BeginDate = dashboardItem.BeginDate ?? DateTime.Today.AddMonths(-1);
            dashboardItem.EndDate = dashboardItem.EndDate ?? DateTime.Today;
        }

        /// <summary>
        /// Gets the toggle behavior for explanatory text.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <returns>A string containing a JavaScript call.</returns>
        protected static string GetToggleBehavior(string clientId)
        {
            return GetToggleBehavior(clientId, ShowExplanatoryText, HideExplanatoryText);
        }

        /// <summary>
        /// Gets the toggle behavior.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="showTextVariableName">Name of a JavaScript variable that has been defined which holds the text to display in the link when the content is hidden.</param>
        /// <param name="hideTextVariableName">Name of a JavaScript variable that has been defined which holds the text to display in the link when the content is being displayed.</param>
        /// <returns>A string containing a JavaScript call.</returns>
        protected static string GetToggleBehavior(string clientId, string showTextVariableName, string hideTextVariableName)
        {
            return string.Format(CultureInfo.InvariantCulture, "toggleElementVisibility(this, '{0}', '{1}', '{2}');", clientId, showTextVariableName, hideTextVariableName);
        }

        /// <summary>
        /// Builds a URL for this TabId, using the given querystring parameters.
        /// </summary>
        /// <param name="moduleId">The module id of the module for which the control key is being used.</param>
        /// <param name="controlKey">The control key to determine which control to load.</param>
        /// <returns>
        /// A URL to the current TabId, with the given querystring parameters
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Usage is NavigateUrl, shouldn't be strongly typed")]
        protected string BuildLinkUrl(int moduleId, ControlKey controlKey)
        {
            return this.BuildLinkUrl("modId=" + moduleId.ToString(CultureInfo.InvariantCulture), "key=" + controlKey.ToString());
        }

        /// <summary>
        /// Builds a URL for this TabId, using the given querystring parameters.
        /// </summary>
        /// <param name="moduleId">The module id of the module for which the control key is being used.</param>
        /// <param name="controlKey">The control key to determine which control to load.</param>
        /// <param name="queryStringParameters">Any other querystring parameters.</param>
        /// <returns>
        /// A URL to the current TabId, with the given querystring parameters
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Usage is NavigateUrl, shouldn't be strongly typed")]
        protected string BuildLinkUrl(int moduleId, ControlKey controlKey, params string[] queryStringParameters)
        {
            Array.Resize(ref queryStringParameters, queryStringParameters.Length + 2);
            queryStringParameters[queryStringParameters.Length - 1] = "modId=" + moduleId.ToString(CultureInfo.InvariantCulture);
            queryStringParameters[queryStringParameters.Length - 2] = "key=" + controlKey.ToString();
            return this.BuildLinkUrl(queryStringParameters);
        }

        /// <summary>
        /// Builds a URL for this TabId, using the given querystring parameters.
        /// </summary>
        /// <param name="queryStringParameters">The qs parameters.</param>
        /// <returns>A URL to the current TabId, with the given querystring parameters</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings", Justification = "Usage is NavigateUrl, shouldn't be strongly typed")]
        protected string BuildLinkUrl(params string[] queryStringParameters)
        {
            return Globals.NavigateURL(this.TabId, string.Empty, queryStringParameters);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.RegisterScripts();
            if (AJAX.IsInstalled())
            {
                AJAX.RegisterScriptManager();
            }
        }

        /// <summary>
        /// Registers the client script includes for this module.
        /// </summary>
        private void RegisterScripts()
        {
            this.Page.ClientScript.RegisterClientScriptInclude(typeof(ModuleBase), "jquery 1.2.6", JQueryResourcePath);
            this.Page.ClientScript.RegisterClientScriptResource(typeof(ModuleBase), DashboardScriptResourcePath);
            this.Page.ClientScript.RegisterStartupScript(typeof(ModuleBase), "jQueryNoConflict", "jQuery(document).ready(function(){jQuery.noConflict();});", true);

            this.RegisterLinkTextResources();
        }

        /// <summary>
        /// Registers JavaScript variables with the text to use when switching between hidden and shown states using <see cref="GetToggleBehavior(string)"/> or <see cref="GetToggleBehavior(string,string,string)"/>.
        /// </summary>
        private void RegisterLinkTextResources()
        {
            this.Page.ClientScript.RegisterHiddenField(ShowExplanatoryText, Localization.GetString("What's This.Text", DashboardResourceFile));
            this.Page.ClientScript.RegisterHiddenField(HideExplanatoryText, Localization.GetString("Hide What's This.Text", DashboardResourceFile));
            this.Page.ClientScript.RegisterHiddenField(ShowDetails, Localization.GetString("DetailsLink.Text", DashboardResourceFile));
            this.Page.ClientScript.RegisterHiddenField(HideDetails, Localization.GetString("Hide DetailsLink.Text", DashboardResourceFile));
            this.Page.ClientScript.RegisterHiddenField(ShowDateRange, Localization.GetString("DateRangeLink.Text", DashboardResourceFile));
            this.Page.ClientScript.RegisterHiddenField(HideDateRange, Localization.GetString("Hide DateRangeLink.Text", DashboardResourceFile));
        }
    }
}
