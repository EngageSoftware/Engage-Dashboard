// <copyright file="Host.ascx.cs" company="Engage Software">
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
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// Dispalys host-level statistics for the current DNN instance
    /// </summary>
    public partial class Host : ModuleBase
    {
        /// <summary>
        /// Converts <paramref name="backupTypeAbbreviation"/> into localized text.
        /// </summary>
        /// <remarks>
        /// Based on the type column of msdb.backupset (from http://msdn.microsoft.com/en-us/library/aa260602(SQL.80).aspx)
        /// </remarks>
        /// <param name="backupTypeAbbreviation">The backup type abbreviation.</param>
        /// <returns>A human-readable form of <paramref name="backupTypeAbbreviation"/></returns>
        protected string GetBackupType(object backupTypeAbbreviation)
        {
            switch (backupTypeAbbreviation.ToString()[0])
            {
                case 'D':
                    return Localization.GetString("Database.Text", this.LocalResourceFile);
                case 'I':
                    return Localization.GetString("Database Differential.Text", this.LocalResourceFile);
                case 'L':
                    return Localization.GetString("Log.Text", this.LocalResourceFile);
                case 'F':
                    return Localization.GetString("File or Filegroup.Text", this.LocalResourceFile);
                default:
                    return Localization.GetString("Unknown Backup Type.Text", this.LocalResourceFile);
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
                SetupDateRangeControls(this.NumberOfFailedScheduleProcessesItem);
                this.SetupAboutText();
                this.FillData();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Localizes the grids on this control.
        /// </summary>
        private void LocalizeGrids()
        {
            if (!this.IsPostBack)
            {
                Utility.LocalizeGridView(ref this.BackupHistoryGridView, this.LocalResourceFile);
                Utility.LocalizeGridView(ref this.ModulesNotInUseGridView, this.LocalResourceFile);
            }
        }

        /// <summary>
        /// Sets up the visibility toggle behavior for the about text links.
        /// </summary>
        private void SetupAboutText()
        {
            this.AboutDatabaseSizeLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);
            this.AboutLogRecordsLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);
            this.AboutScheduledProcessesLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);
            this.AboutUnusedModulesLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);
            this.AboutBackupHistoryLink.Text = Localization.GetString("What's This.Text", DashboardResourceFile);

            this.AboutDatabaseSizeLink.Attributes.Add("OnClick", GetToggleBehavior(this.AboutDatabaseSizeMessage.ClientID));
            this.AboutLogRecordsLink.Attributes.Add("OnClick", GetToggleBehavior(this.AboutLogRecordsMessage.ClientID));
            this.AboutScheduledProcessesLink.Attributes.Add("OnClick", GetToggleBehavior(this.AboutScheduledProcessesMessage.ClientID));
            this.AboutUnusedModulesLink.Attributes.Add("OnClick", GetToggleBehavior(this.AboutUnusedModulesMessage.ClientID));
            this.AboutBackupHistoryLink.Attributes.Add("OnClick", GetToggleBehavior(this.AboutBackupHistoryMessage.ClientID));
        }

        /// <summary>
        /// Fills the <see cref="DashboardItem"/>s on this control with data.
        /// </summary>
        private void FillData()
        {
            this.DatabaseSizeItem.SetValue(DataProvider.Instance().GetSizeOfDatabase());
            this.DatabaseLogSizeItem.SetValue(DataProvider.Instance().GetDatabaseLogSize());
            this.NumberOfEventLogRecordsItem.SetValue(DataProvider.Instance().GetRowCount("EventLog"));
            this.NumberOfSiteLogRecordsItem.SetValue(DataProvider.Instance().GetRowCount("SiteLog"));
            this.NumberOfFailedScheduleProcessesItem.SetValue(
                DataProvider.Instance().GetFailedSchedulesInDateSpan(
                    this.NumberOfFailedScheduleProcessesItem.BeginDate.Value, this.NumberOfFailedScheduleProcessesItem.EndDate.Value));

            // the Recovery Model column isn't available before SQL Server 2005
            if (!DataProvider.Instance().SupportsSql2005Functionality())
            {
                this.BackupHistoryGridView.Columns.RemoveAt(4);
            }

            using (IDataReader backupHistory = DataProvider.Instance().GetDatabaseBackupHistory())
            {
                this.BackupHistoryGridView.DataSource = backupHistory;
                this.BackupHistoryGridView.DataBind();
                this.BackupHistoryItem.SetValue(this.BackupHistoryGridView.Rows.Count);
            }

            using (IDataReader unusedModules = DataProvider.Instance().GetUnusedModules())
            {
                this.ModulesNotInUseGridView.DataSource = unusedModules;
                this.ModulesNotInUseGridView.DataBind();
                this.ModulesNotInUseItem.SetValue(this.ModulesNotInUseGridView.Rows.Count);
            }
        }
    }
}