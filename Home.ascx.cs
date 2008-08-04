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
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Web.Hosting;
    using System.Xml.XPath;
    using DotNetNuke.Common;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Services.Packages;
    using Telerik.Charting;
    using Telerik.Charting.Styles;
    using Telerik.Web.UI;

    /// <summary>
    /// Displays an overview of the statistics contained in the module
    /// </summary>
    public partial class Home : ModuleBase
    {
        /// <summary>
        /// A <see cref="IDictionary{TKey,TValue}"/> which maps between the CSS class for an Event Log type, and the color for that CSS class and type.
        /// </summary>
        private static readonly IDictionary<string, Color> eventLogTypeColors = FillEventLogTypeColorDictionary();

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
        /// Fills <see cref="eventLogTypeColors"/>.
        /// </summary>
        /// <remarks>
        /// Based on <c>~/DesktopModules/Admin/LogViewer/logviewer.css</c>
        /// </remarks>
        /// <returns>A dictionary for <see cref="eventLogTypeColors"/></returns>
        private static IDictionary<string, Color> FillEventLogTypeColorDictionary()
        {
            IDictionary<string, Color> eventLogTypeColorDictionary = new Dictionary<string, Color>(9);
            eventLogTypeColorDictionary.Add("Exception", Color.FromArgb(0xff, 0x14, 0x14));
            eventLogTypeColorDictionary.Add("ItemCreated", Color.FromArgb(0x00, 0x99, 0x00));
            eventLogTypeColorDictionary.Add("ItemUpdated", Color.FromArgb(0x00, 0x99, 0x99));
            eventLogTypeColorDictionary.Add("ItemDeleted", Color.FromArgb(0x14, 0xff, 0xff));
            eventLogTypeColorDictionary.Add("OperationSuccess", Color.FromArgb(0x99, 0x99, 0x00));
            eventLogTypeColorDictionary.Add("OperationFailure", Color.FromArgb(0x99, 0x00, 0x00));
            eventLogTypeColorDictionary.Add("GeneralAdminOperation", Color.FromArgb(0x4d, 0x00, 0x99));
            eventLogTypeColorDictionary.Add("AdminAlert", Color.FromArgb(0x14, 0x8a, 0xff));
            eventLogTypeColorDictionary.Add("HostAlert", Color.FromArgb(0xff, 0x8a, 0x14));
            eventLogTypeColorDictionary.Add("SecurityException", Color.FromArgb(0x00, 0x00, 0x00));
            return eventLogTypeColorDictionary;
        }

        /// <summary>
        /// Gets the color of the given event log type, based on that type's associated CSS class.
        /// </summary>
        /// <param name="typeCssClass">The event log type's CSS class.</param>
        /// <returns>The color of the event log type</returns>
        private static Color GetEventLogTypeColor(string typeCssClass)
        {
            return eventLogTypeColors.ContainsKey(typeCssClass) ? eventLogTypeColors[typeCssClass] : Color.Empty;
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
                if (!this.IsPostBack)
                {
                    this.SetupAboutLinks();
                    this.FillData();
                }
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
        /// Sets up the visibility toggle behavior for the about text links.
        /// </summary>
        private void SetupAboutLinks()
        {
            this.ChartingSupportLink.Text = Localization.GetString("Learn More.Text", DashboardResourceFile);
            this.ChartingSupportLink.Attributes.Add("OnClick", GetToggleBehavior("InstallChartingPanel", LearnMore, LearnLess));
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        private void FillData()
        {
            this.SetupCharting();
        }

        /// <summary>
        /// Sets up the Telerik <see cref="RadChart"/>s on the page.  Installs charting support into the web.config if necessary. 
        /// </summary>
        private void SetupCharting()
        {
            if (this.ChartingIsInstalled())
            {
                this.FillDatabaseSizeChart();
                this.FillSeoPagesChart();
                this.FillEventLogChart();
            }
            else
            {
                this.ChartsMultiview.SetActiveView(this.InstallChartingView);
            }
        }

        /// <summary>
        /// Fills the database size chart.
        /// </summary>
        private void FillDatabaseSizeChart()
        {
            if (this.UserInfo.IsSuperUser)
            {
                this.databaseSizeChartPanel.Visible = true;
                this.DatabaseSizeChart.ChartTitle.TextBlock.Text = Localization.GetString("Database Size By Log and Data.Text", this.LocalResourceFile);

                // TODO: make getting the size of the database and the log safer!
                double databaseSize;
                double logSize;
                if (Double.TryParse(DataProvider.Instance().GetSizeOfDatabase().Replace(" KB", string.Empty), out databaseSize)
                    && Double.TryParse(DataProvider.Instance().GetDatabaseLogSize().Replace(" KB", string.Empty), out logSize))
                {
                    this.DatabaseSizeChart.Series[0].Items.Add(this.GetPieChartSeriesItem(logSize, "DatabaseLogSize.Text"));
                    this.DatabaseSizeChart.Series[0].Items.Add(this.GetPieChartSeriesItem(databaseSize, "DatabaseSize.Text"));
                }
            }
        }

        /// <summary>
        /// Fills the SEO pages chart.
        /// </summary>
        private void FillSeoPagesChart()
        {
            this.SeoPagesChart.ChartTitle.TextBlock.Text = Localization.GetString("Pages With and Without Keywords or Descriptions.Text", this.LocalResourceFile);

            using (IDataReader seoPagesReader = DataProvider.Instance().CountPagesWithoutDescriptionOrKeywords(this.PortalId))
            {
                if (seoPagesReader.Read())
                {
                    int pagesWithoutSeoCount = (int)seoPagesReader[0];

                    if (seoPagesReader.NextResult() && seoPagesReader.Read())
                    {
                        int totalPagesCount = (int)seoPagesReader[0];
                        this.SeoPagesChart.Series[0].Items.Add(this.GetPieChartSeriesItem(pagesWithoutSeoCount, "PagesWithoutDescriptionOrKeywords.Text"));
                        this.SeoPagesChart.Series[0].Items.Add(this.GetPieChartSeriesItem(totalPagesCount - pagesWithoutSeoCount, "PagesWithDescriptionAndKeywords.Text"));
                    }
                }
            }
        }

        /// <summary>
        /// Fills the event log chart.
        /// </summary>
        private void FillEventLogChart()
        {
            this.EventLogChart.ChartTitle.TextBlock.Text = Localization.GetString("Event Log Entries by Type.Text", this.LocalResourceFile);
            this.EventLogChart.PlotArea.EmptySeriesMessage.TextBlock.Text = Localization.GetString("EmptyEventLogMessage.Text", this.LocalResourceFile);

            int? portalId = this.UserInfo.IsSuperUser ? (int?)null : this.PortalId;
            using (IDataReader eventLogReader = DataProvider.Instance().GetEventLogSummaryByType(portalId))
            {
                while (eventLogReader.Read())
                {
                    string eventLogTypeCssClass = (string)eventLogReader["LogTypeCssClass"];
                    ChartSeriesItem eventLogItem = this.GetPieChartSeriesItem((int)eventLogReader["Count"], eventLogTypeCssClass, GetEventLogTypeColor(eventLogTypeCssClass));
                    this.EventLogChart.Series[0].Items.Add(eventLogItem);
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="ChartSeriesItem"/> for a pie chart.
        /// </summary>
        /// <param name="value">The value of the pie slice.</param>
        /// <param name="resourceKey">The resource key for the label text.</param>
        /// <returns>A <see cref="ChartSeriesItem"/> for a pie chart with the given value and text</returns>
        private ChartSeriesItem GetPieChartSeriesItem(double value, string resourceKey)
        {
            return this.GetPieChartSeriesItem(value, resourceKey, Color.Empty);
        }

        /// <summary>
        /// Creates a <see cref="ChartSeriesItem"/> for a pie chart.
        /// </summary>
        /// <param name="value">The value of the pie slice.</param>
        /// <param name="resourceKey">The resource key for the label text.</param>
        /// <param name="fillColor">Color with which to fill the region.</param>
        /// <returns>
        /// A <see cref="ChartSeriesItem"/> for a pie chart with the given value and text
        /// </returns>
        private ChartSeriesItem GetPieChartSeriesItem(double value, string resourceKey, Color fillColor)
        {
            ChartSeriesItem chartSeriesItem = new ChartSeriesItem(value);
            chartSeriesItem.Name = chartSeriesItem.ActiveRegion.Tooltip = Localization.GetString(resourceKey, this.LocalResourceFile);
            if (fillColor != Color.Empty)
            {
                chartSeriesItem.Appearance.FillStyle.MainColor = fillColor;
                chartSeriesItem.Appearance.FillStyle.FillType = FillType.Solid;
            }

            return chartSeriesItem;
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
        /// Determines whether the Telerik Charting component has been installed into the web.config.
        /// </summary>
        /// <returns><c>true</c> if Telerik charting is installed in the web.config, otherwise <c>false</c></returns>
        private bool ChartingIsInstalled()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            XPathDocument configXml = new XPathDocument(HostingEnvironment.MapPath("~/web.config"));

            // ReSharper restore AssignNullToNotNullAttribute
            double iisVersion;
            string iisSoftwareVariable = this.Request.ServerVariables["SERVER_SOFTWARE"];
            if (iisSoftwareVariable.Contains("/") && double.TryParse(iisSoftwareVariable.Split('/')[1], NumberStyles.Float, CultureInfo.InvariantCulture, out iisVersion))
            {
                XPathExpression httpHandlerPath = iisVersion < 7
                                                      ? XPathExpression.Compile("//configuration/system.web/httpHandlers/add[@path='ChartImage.axd']")
                                                      : XPathExpression.Compile("//configuration/system.webServer/handlers/add[@path='ChartImage.axd']");

                return configXml.CreateNavigator().SelectSingleNode(httpHandlerPath) != null;
            }

            return false;
        }
    }
}
