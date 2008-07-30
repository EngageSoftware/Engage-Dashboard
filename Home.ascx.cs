// <copyright file="Home.ascx.cs" company="Engage Software">
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
    using System.IO;
    using System.Web.Hosting;
    using System.Xml.XPath;
    using DotNetNuke.Common;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Services.Packages;

    /// <summary>
    /// Displays an overview of the statistics contained in the module
    /// </summary>
    public partial class Home : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
            this.InstallChartingButton.Click += this.InstallChartingButton_Click;
        }

        /// <summary>
        /// Determines whether the Telerik Charting component has been installed into the web.config.
        /// </summary>
        /// <returns><c>true</c> if Telerik charting is installed in the web.config, otherwise <c>false</c></returns>
        private bool ChartingIsInstalled()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            XPathDocument configXml = new XPathDocument(HostingEnvironment.MapPath("~/web.config"));

            // ReSharper restore AssignNullToNotNullAttribute
            XPathExpression httpHandlerPath = this.Request.ServerVariables["SERVER_SOFTWARE"] != "Microsoft-IIS/7.0"
                                                  ? XPathExpression.Compile("//configuration/system.web/httpHandlers/add[@path='ChartImage.axd']")
                                                  : XPathExpression.Compile("//configuration/system.webServer/handlers/add[@path='ChartImage.axd']");

            return configXml.CreateNavigator().SelectSingleNode(httpHandlerPath) != null;
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
                this.SetupPanelText();
                this.FillData();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the Click event of the InstallChartingButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InstallChartingButton_Click(object sender, EventArgs e)
        {
            this.InstallTelerikCharting();
        }

        /// <summary>
        /// Installs support for Telerik Charting in the web.config.
        /// </summary>
        private void InstallTelerikCharting()
        {
            using (Stream xmlMergeManifest = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Engage.Dnn.Dashboard.01.00.00.xml"))
            {
                XmlMerge configMerge = new XmlMerge(xmlMergeManifest, "01.00.00", "Engage: Dashboard");
                configMerge.UpdateConfigs();
                this.Response.Redirect(Globals.NavigateURL());
            }
        }

        /// <summary>
        /// Setups the panel text.
        /// </summary>
        private void SetupPanelText()
        {
            this.ChartsPanel.GroupingText = Localization.GetString("ChartsPanel.Text", this.LocalResourceFile);
            this.InstallChartingPanel.GroupingText = Localization.GetString("InstallChartingPanel.Text", this.LocalResourceFile);
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        private void FillData()
        {
            if (this.ChartingIsInstalled())
            {
                this.UserRegistrationsChart.DataSource = DataProvider.Instance().GetUserRegistrationsInDateSpan(DateTime.Today.AddMonths(-1), DateTime.Today, this.PortalId);
                this.UserRegistrationsChart.DataBind();
            }
            else
            {
                this.ChartsPanel.Visible = false;
                this.InstallChartingPanel.Visible = true;
            }
        }
    }
}
