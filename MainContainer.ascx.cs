// <copyright file="MainContainer.ascx.cs" company="Engage Software">
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
    using System.IO;
    using DotNetNuke.Security;
    using DotNetNuke.Services.Exceptions;

    /// <summary>
    /// The main container that is used by the Engage: Dashboard module.  
    /// This control is registered with DNN, and is in charge of loading other requested control.
    /// </summary>
    public partial class MainContainer : ModuleBase
    {
        /// <summary>
        /// The default control to load when none is indicated
        /// </summary>
        private const string DefaultControl = "Admin.ascx";

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (PortalSecurity.HasEditPermissions(this.ModuleId, this.TabId))
            {
                this.LoadChildControl(this.GetControlToLoad());
            }
            else
            {
                this.GlobalNavigation.Visible = false;
                this.NoPermissionMessage.Visible = true;
            }
        }

        /// <summary>
        /// Gets the control to load, based on the key (or lack thereof) that is passed on the querystring.
        /// </summary>
        /// <returns>A relative path to the control that should be loaded into this container</returns>
        private string GetControlToLoad()
        {
            string keyParam = string.Empty;
            string[] modIdParams = this.Request.QueryString["modId"] == null ? new string[] { } : this.Request.QueryString["modId"].Split(';');
            string[] keyParams = this.Request.QueryString["key"] == null ? new string[] { } : this.Request.QueryString["key"].Split(';');

            for (int i = 0; i < modIdParams.Length && i < keyParams.Length; i++)
            {
                int modId;
                if (int.TryParse(modIdParams[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out modId) && modId == this.ModuleId)
                {
                    keyParam = keyParams[i];
                    break;
                }
            }

            if (Engage.Utility.HasValue(keyParam) && Enum.IsDefined(typeof(ControlKey), keyParam))
            {
                return keyParam + ".ascx";
            }
            else
            {
                return DefaultControl;
            }
        }

        /// <summary>
        /// Loads the child control to be displayed in this container.
        /// </summary>
        /// <param name="controlToLoad">The control to load.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catching Exception at highest level as a safeguard")]
        private void LoadChildControl(string controlToLoad)
        {
            try
            {
                if (controlToLoad == null)
                {
                    return;
                }

                ModuleBase currentControl = (ModuleBase)this.LoadControl(controlToLoad);
                currentControl.ModuleConfiguration = this.ModuleConfiguration;
                currentControl.ID = Path.GetFileNameWithoutExtension(controlToLoad);
                this.ControlPlaceholder.Controls.Add(currentControl);
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
    }
}