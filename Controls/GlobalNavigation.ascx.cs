// <copyright file="GlobalNavigation.ascx.cs" company="Engage Software">
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
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Security.Permissions;
    using DotNetNuke.Services.Exceptions;

    /// <summary>
    /// A navigation control that is always displayed at the top of the module.  Currently only for admins.
    /// </summary>
    public partial class GlobalNavigation : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // since the global navigation control is not loaded using DNN mechanisms we need to set it here so that calls to 
            // module related information will appear the same as the actual control this navigation is sitting on.hk
            this.ModuleConfiguration = ((PortalModuleBase)this.Parent).ModuleConfiguration;
            this.LocalResourceFile = "~" + DesktopModuleFolderName + "Controls/App_LocalResources/GlobalNavigation";

            this.Load += this.Page_Load;
        }

        /// <summary>
        /// Prepends the correct directory structure to the image name.
        /// </summary>
        /// <param name="imageName">Filename of the image to use.</param>
        /// <returns>A full, relative path to the given image.</returns>
        private static string GetNavigationImage(string imageName)
        {
            return "~" + DesktopModuleFolderName + "Images/" + imageName;
        }

        /////// <summary>
        /////// Gets the current control key.
        /////// </summary>
        /////// <returns>The current control key</returns>
        ////private ControlKey? GetCurrentControlKey()
        ////{
        ////    string keyValue = this.Request.QueryString["key"];
        ////    return keyValue != null && Enum.IsDefined(typeof(ControlKey), keyValue) ? (ControlKey?)Enum.Parse(typeof(ControlKey), keyValue) : null;
        ////}

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
                this.SetupLinks();
                this.SetVisibility();
                this.SetImages();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Sets up the URLs for each of the links.
        /// </summary>
        private void SetupLinks()
        {
            this.AdminLink.NavigateUrl = Globals.NavigateURL();
            this.HostLink.NavigateUrl = this.BuildLinkUrl(this.ModuleId, ControlKey.Host);
            this.ModuleLocatorLink.NavigateUrl = this.BuildLinkUrl(this.ModuleId, ControlKey.ModuleLocator);
            this.SkinLocatorLink.NavigateUrl = this.BuildLinkUrl(this.ModuleId, ControlKey.SkinLocator);
            this.F3Link.NavigateUrl = this.BuildLinkUrl(this.ModuleId, ControlKey.F3);
            this.SettingsLink.NavigateUrl = this.EditUrl("ModuleId", this.ModuleId.ToString(CultureInfo.InvariantCulture), "Module");
        }

        /// <summary>
        /// Sets the visibility.
        /// </summary>
        private void SetVisibility()
        {
            this.SettingsLink.Visible = TabPermissionController.HasTabPermission("EDIT");
            this.HostLink.Visible = this.UserInfo.IsSuperUser;
        }

        /// <summary>
        /// Sets the image for the current page to a disabled image, if appropriate.
        /// </summary>
        private void SetImages()
        {
            this.AdminLink.ImageUrl = GetNavigationImage("admin.gif");
            this.HostLink.ImageUrl = GetNavigationImage("host.gif");
            this.ModuleLocatorLink.ImageUrl = GetNavigationImage("moduleLocator.gif");
            this.SkinLocatorLink.ImageUrl = GetNavigationImage("skinLocator.gif");
            this.F3Link.ImageUrl = GetNavigationImage("f3.gif");
            this.SettingsLink.ImageUrl = GetNavigationImage("settings.gif");

            // TODO: Add selected link images/style to replace disabled images in global navigation
            ////ControlKey? currentControlKey = this.GetCurrentControlKey();
            ////if (!currentControlKey.HasValue)
            ////{
            ////    this.AdminLink.ImageUrl = GetNavigationImage("admin_disabled.gif");
            ////}
            ////else
            ////{
            ////    switch (currentControlKey.Value)
            ////    {
            ////        case ControlKey.Host:
            ////            this.HostLink.ImageUrl = GetNavigationImage("host_disabled.gif");
            ////            break;
            ////        case ControlKey.ModuleLocator:
            ////            this.ModuleLocatorLink.ImageUrl = GetNavigationImage("moduleLocator_disabled.gif");
            ////            break;
            ////        case ControlKey.SkinLocator:
            ////            this.SkinLocatorLink.ImageUrl = GetNavigationImage("skinLocator_disabled.gif");
            ////            break;
            ////        case ControlKey.F3:
            ////            this.F3Link.ImageUrl = GetNavigationImage("f3_disabled.gif");
            ////            break;
            ////        default:
            ////            this.AdminLink.ImageUrl = GetNavigationImage("admin_disabled.gif");
            ////            break;
            ////    }
            ////}
        }
    }
}