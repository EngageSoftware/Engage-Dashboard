// <copyright file="SqlDataProvider.cs" company="Engage Software">
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
    using System.Globalization;
    using System.Text;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Framework.Providers;
    using Microsoft.ApplicationBlocks.Data;
    using Utility = Engage.Utility;

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract <see cref="DataProvider"/> class
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider : DataProvider
    {
        /// <summary>
        /// The prefix database objects owned by this module.
        /// </summary>
        private const string ModuleQualifier = "EngageDashboard_";

        /// <summary>
        /// The type of DNN provider that this class represents.
        /// </summary>
        private const string ProviderType = "data";

        /// <summary>
        /// The major version number of SQL Server 2000
        /// </summary>
        private const int SqlServer2000MajorVersionNumber = 8;

        /// <summary>
        /// Backing field for <see cref="ConnectionString"/>
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Backing field for <see cref="DatabaseOwner"/>
        /// </summary>
        private readonly string databaseOwner;

        /// <summary>
        /// Backing field for <see cref="ObjectQualifier"/>
        /// </summary>
        private readonly string objectQualifier;

        /// <summary>
        /// Backing field for <see cref="GetDatabaseName"/>
        /// </summary>
        private string databaseName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataProvider"/> class.
        /// </summary>
        public SqlDataProvider()
        {
            // Read the configuration specific information for this provider
            ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
            Provider objProvider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

            // Read the attributes for this provider

            // Get Connection string from web.config
            this.connectionString = Config.GetConnectionString();

            if (string.IsNullOrEmpty(this.connectionString))
            {
                // Use connection string specified in provider
                this.connectionString = objProvider.Attributes["connectionString"];
            }

            this.objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(this.objectQualifier) && !this.objectQualifier.EndsWith("_", StringComparison.Ordinal))
            {
                this.objectQualifier += "_";
            }

            this.databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(this.databaseOwner) && !this.databaseOwner.EndsWith(".", StringComparison.Ordinal))
            {
                this.databaseOwner += ".";
            }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        private string ConnectionString
        {
            get { return this.connectionString; }
        }

        /// <summary>
        /// Gets the object qualifier.
        /// </summary>
        /// <value>The object qualifier.</value>
        private string ObjectQualifier
        {
            get { return this.objectQualifier; }
        }

        /// <summary>
        /// Gets the database owner.
        /// </summary>
        /// <value>The database owner.</value>
        private string DatabaseOwner
        {
            get { return this.databaseOwner; }
        }

        /// <summary>
        /// Gets the DNN prefix.
        /// </summary>
        /// <value>The DNN prefix.</value>
        private string DnnPrefix
        {
            get { return this.databaseOwner + this.objectQualifier; }
        }

        /// <summary>
        /// Gets the number of pages in the given portal.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of pages in the given portal</returns>
        public override int CountPages(int portalId)
        {
            StringBuilder sql = new StringBuilder(128);

            sql.Append("DECLARE @AdminTabID int ");
            sql.AppendFormat("SET @AdminTabID = (SELECT t.TabID FROM {0}Tabs t WHERE t.TabName = 'Admin' AND t.ParentID IS NULL AND t.Level = 0 AND t.PortalID = @portalId) ", this.DnnPrefix);

            sql.Append("SELECT COUNT(*) ");
            sql.AppendFormat("FROM {0}Tabs t ", this.DnnPrefix);
            sql.Append("WHERE t.PortalID = @portalId");
            sql.Append("	AND (t.ParentId <> @AdminTabID OR t.ParentId IS NULL)");
            sql.Append("    AND t.TabName <> 'Admin'");
            sql.Append("	AND t.IsDeleted <> 1");

            return (int)SqlHelper.ExecuteScalar(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets a count of the pages that don't have a description defined.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A count of the pages that don't have a description defined</returns>
        public override int CountPagesWithoutDescriptionOrKeywords(int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("DECLARE @AdminTabID int ");
            sql.AppendFormat("SET @AdminTabID = (SELECT t.TabID FROM {0}Tabs t WHERE t.TabName = 'Admin' AND t.ParentID IS NULL AND t.Level = 0 AND t.PortalID = @portalId) ", this.DnnPrefix);

            sql.Append("SELECT count(*)");
            sql.AppendFormat("FROM {0}Tabs t ", this.DnnPrefix);
            sql.Append("WHERE t.PortalID = @portalId ");
            sql.Append("	AND (t.ParentId <> @AdminTabID OR t.ParentId IS NULL) ");
            sql.Append("	AND t.TabName <> 'Admin' ");
            sql.Append("	AND (t.Description = '' or t.Keywords = '')");

            return (int)SqlHelper.ExecuteScalar(
                this.ConnectionString,
                CommandType.Text,
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets the number of deleted module and pages in the recycle bin fir the given portal.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of deleted module and pages in the recycle bin for the given portal</returns>
        public override int CountRecycleBin(int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("DECLARE @DeletedModules int ");
            sql.Append("DECLARE @DeletedPages int ");
            sql.Append("DECLARE @RecycleBinCount int ");

            sql.AppendFormat("SET @DeletedModules = (SELECT COUNT(*) FROM {0}Modules m WHERE m.IsDeleted = '1' AND m.PortalID = @portalId) ", this.DnnPrefix);
            sql.AppendFormat("SET @DeletedPages = (SELECT COUNT(*) FROM {0}Tabs t WHERE t.IsDeleted = '1' AND t.PortalID = @portalId) ", this.DnnPrefix);
            sql.Append("SET @RecycleBinCount = (@DeletedModules + @DeletedPages) ");
            sql.Append("SELECT @RecycleBinCount");

            return (int)SqlHelper.ExecuteScalar(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets the number of roles in the given portal.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of roles in the given portal</returns>
        public override int CountRoles(int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT COUNT(*) ");
            sql.AppendFormat("FROM {0}Roles r", this.DnnPrefix);
            sql.Append("    WHERE r.PortalID = @portalId");

            return (int)SqlHelper.ExecuteScalar(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets the modules in the current portal that are only visible to administrators.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The modules in the current portal that are only visible to administrators</returns>
        public override IDataReader GetAdminOnlyModules(int portalId)
        {
            StringBuilder sql = new StringBuilder(256);
            sql.Append("DECLARE @adminRoleId int ");
            sql.AppendFormat("SET @adminRoleId = (SELECT r.RoleID FROM {0}Roles r WHERE r.RoleName = 'Administrators' AND r.PortalID = @portalId) ", this.DnnPrefix);

            sql.Append("DECLARE @viewModulePermissionId int ");
            sql.AppendFormat("SET @viewModulePermissionId = (SELECT p.PermissionID FROM {0}Permission p WHERE p.PermissionKey = 'VIEW' AND p.PermissionCode = 'SYSTEM_MODULE_DEFINITION') ", this.DnnPrefix);

            sql.Append("DECLARE @viewTabPermissionId int ");
            sql.AppendFormat("SET @viewTabPermissionId = (SELECT p.PermissionID FROM {0}Permission p WHERE p.PermissionKey = 'VIEW' AND p.PermissionCode = 'SYSTEM_TAB') ", this.DnnPrefix);

            sql.Append("DECLARE @adminTabId int ");
            sql.AppendFormat("SET @adminTabId = (SELECT t.TabID FROM {0}Tabs t WHERE t.TabName = 'Admin' AND t.PortalId = @portalId) ", this.DnnPrefix);
            
            sql.AppendFormat("SELECT vmp.ModuleId, m.ModuleTitle, tm.TabID, t.TabName, t.TabPath  FROM {0}vw_ModulePermissions vmp ", this.DnnPrefix);
            sql.AppendFormat("INNER JOIN {0}Modules m ON m.ModuleId = vmp.ModuleId ", this.DnnPrefix);
            sql.AppendFormat("INNER JOIN {0}TabModules tm ON tm.ModuleID = vmp.ModuleId ", this.DnnPrefix);
            sql.AppendFormat("INNER JOIN {0}Tabs t ON t.TabID = tm.TabID ", this.DnnPrefix);
            sql.Append("WHERE m.PortalID = @portalId ");
            sql.Append("	AND vmp.PermissionId = @viewModulePermissionId ");
            sql.Append("	AND vmp.RoleId = @adminRoleId ");
            sql.Append("	AND NOT EXISTS (");
            sql.AppendFormat("SELECT * FROM {0}vw_ModulePermissions vmp2 ", this.DnnPrefix);
            sql.AppendFormat("INNER JOIN {0}Modules m2 ON m2.ModuleId = vmp2.ModuleId ", this.DnnPrefix);
            sql.AppendFormat("INNER JOIN {0}TabModules tm2 ON tm2.ModuleId = vmp2.ModuleId ", this.DnnPrefix);
            sql.AppendFormat("INNER JOIN {0}TabPermission tp2 ON tp2.TabID = tm2.TabID ", this.DnnPrefix);
            sql.Append("WHERE m2.PortalID = @portalId ");
            sql.Append("	AND vmp2.PermissionId = @viewModulePermissionId ");
            sql.Append("	AND vmp2.RoleId <> @adminRoleId ");
            sql.Append("	AND vmp.ModuleId = vmp2.ModuleId ");
            sql.Append("OR m2.PortalID = @portalId ");
            sql.Append("	AND m2.InheritViewPermissions = 1 ");
            sql.Append("	AND tp2.PermissionId = @viewTabPermissionId ");
            sql.Append("	AND tp2.RoleId = @adminRoleId ");
            sql.Append("	AND vmp.ModuleId = vmp2.ModuleId ");
            sql.Append(")");

            return SqlHelper.ExecuteReader(
                this.ConnectionString,
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets the pages in the given portal that are only visible to administrators.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The pages in the given portal that are only visible to administrators</returns>
        public override IDataReader GetAdminOnlyPages(int portalId)
        {
            StringBuilder sql = new StringBuilder(256);
            sql.Append("DECLARE @adminRoleId int ");
            sql.AppendFormat("SET @adminRoleId = (SELECT r.RoleID FROM {0}Roles r WHERE r.RoleName = 'Administrators' AND  r.PortalID = @portalId) ", this.DnnPrefix);
            
            sql.Append("DECLARE @viewTabPermissionId int ");
            sql.AppendFormat("SET @viewTabPermissionId = (SELECT p.PermissionID FROM {0}Permission p WHERE p.PermissionKey = 'VIEW' AND p.PermissionCode = 'SYSTEM_TAB') ", this.DnnPrefix);

            sql.Append("DECLARE @adminTabId int ");
            sql.AppendFormat("SET @adminTabId = (SELECT t.TabID FROM {0}Tabs t WHERE t.TabName = 'Admin' AND t.PortalId = @portalId) ", this.DnnPrefix);
            
            sql.AppendFormat("SELECT t.TabId, t.TabName FROM {0}vw_TabPermissions vtp ", this.DnnPrefix);
            sql.AppendFormat("INNER JOIN {0}Tabs t ON t.TabID = vtp.TabID ", this.DnnPrefix);
            sql.Append("WHERE vtp.PortalID = @portalId ");
            sql.Append("	AND vtp.PermissionId = @viewTabPermissionId ");
            sql.Append("	AND vtp.RoleId = @adminRoleId ");
            sql.Append("	AND NOT EXISTS (");
            sql.AppendFormat("SELECT t2.TabId, t2.TabName FROM {0}vw_TabPermissions vtp2 ", this.DnnPrefix);
            sql.AppendFormat("INNER JOIN {0}Tabs t2 ON t2.TabID = vtp2.TabID ", this.DnnPrefix);
            sql.Append("WHERE vtp2.PortalID = @portalId ");
            sql.Append("	AND vtp2.PermissionId = @viewTabPermissionId ");
            sql.Append("	AND vtp2.RoleId = @adminRoleId ");
            sql.Append("	AND t2.ParentID = @adminTabId ");
            sql.Append("	AND t.TabID = t2.TabID ");
            sql.Append("OR vtp2.PortalID = @portalId ");
            sql.Append("	AND vtp2.PermissionId = @viewTabPermissionId ");
            sql.Append("	AND vtp2.RoleId = @adminRoleId ");
            sql.Append("	AND t2.TabID = @adminTabId ");
            sql.Append("	AND t.TabID = t2.TabID ");
            sql.Append("OR vtp2.PortalID = @portalId ");
            sql.Append("	AND vtp2.PermissionId = @viewTabPermissionId ");
            sql.Append("	AND vtp2.RoleId <> @adminRoleId ");
            sql.Append("	AND t.TabID = t2.TabID");
            sql.Append(")");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets the backup history of the current site.
        /// </summary>
        /// <returns>the backup history of the current site</returns>
        public override IDataReader GetDatabaseBackupHistory()
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT TOP 10 ");
            sql.Append("	a.backup_finish_date AS 'Backup Date',");
            sql.Append("	a.[type] AS 'Backup Type',");
            sql.Append("    a.server_name AS 'Server Name',");
            sql.Append("	a.database_name AS 'Database',");
            if (this.SupportsSql2005Functionality())
            {
                sql.Append("	a.recovery_model AS 'Recovery Model',");
            }

            sql.Append("	b.physical_device_name AS 'Device' ");
            sql.Append("FROM msdb.dbo.backupset a");
            sql.Append("	LEFT JOIN msdb.dbo.backupmediafamily b ON a.media_set_id = b.media_set_id ");
            sql.Append("WHERE a.database_name = (SELECT DB_NAME()) ");
            sql.Append("ORDER BY a.backup_finish_date DESC");

            return SqlHelper.ExecuteReader(this.ConnectionString, CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// Gets the size of the data file for this database in MB.
        /// </summary>
        /// <returns>The size of the data file for this database in MB</returns>
        public override string GetSizeOfDatabase()
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("EXEC sp_helpdb [");
            sql.Append(this.GetDatabaseName());
            sql.Append("]");

            DataSet ds = SqlHelper.ExecuteDataset(this.ConnectionString, CommandType.Text, sql.ToString());
            foreach (DataRow dr in ds.Tables[1].Select("usage = 'data only'"))
            {
                return dr["size"].ToString();
            }

            return null;
        }

        /// <summary>
        /// Gets the size of the log file for the database.
        /// </summary>
        /// <returns>The size of the database log in MB.</returns>
        public override string GetDatabaseLogSize()
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("EXEC sp_helpdb [");
            sql.Append(this.GetDatabaseName());
            sql.Append("]");

            DataSet ds = SqlHelper.ExecuteDataset(this.ConnectionString, CommandType.Text, sql.ToString());
            foreach (DataRow dr in ds.Tables[1].Select("usage = 'log only'"))
            {
                return dr["size"].ToString();
            }

            return null;
        }

        /// <summary>
        /// Gets a list of the pages in the given portal that have no modules on them.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of the pages in the given portal that have no modules on them</returns>
        public override IDataReader GetEmptyPages(int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT t.TabID, t.TabName, t.TabPath ");
            sql.AppendFormat("FROM {0}Tabs t ", this.DnnPrefix);
            sql.Append("WHERE NOT EXISTS (SELECT t.TabID, t.TabName, t.TabPath ");
            sql.AppendFormat("	FROM {0}TabModules tm ", this.DnnPrefix);
            sql.Append("	WHERE t.TabID = tm.TabID) ");
            sql.Append("	AND TabName <> 'Admin' ");
            sql.Append("    AND PortalID = @portalId");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets the number of failed scheduler tasks in the given date span.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The number of failed scheduler tasks in the given date span</returns>
        public override int GetFailedSchedulesInDateSpan(DateTime startDate, DateTime endDate)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT COUNT(*) ");
            sql.AppendFormat("FROM {0}ScheduleHistory sh ", this.DnnPrefix);
            sql.Append("WHERE sh.EndDate < @endDate");
            sql.Append("	AND sh.EndDate > @startDate");
            sql.Append("	AND sh.Succeeded = 0");

            return Convert.ToInt32(
                SqlHelper.ExecuteScalar(
                    this.ConnectionString,
                    CommandType.Text,
                    sql.ToString(),
                    Utility.CreateDateTimeParam("@startDate", startDate),
                    Utility.CreateDateTimeParam("@endDate", endDate)), 
                CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets information about the HTML/Text modules in the given portal that don't have a search summary defined.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>Information about the HTML/Text modules in the given portal that don't have a search summary defined</returns>
        public override IDataReader GetHtmlTextModulesWithoutSummary(int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT tm.ModuleID, m.ModuleTitle, t.TabID, t.TabName, t.TabPath ");
            sql.AppendFormat("FROM {0}TabModules tm ", this.DnnPrefix);
            sql.AppendFormat("    INNER JOIN {0}HtmlText ht ON tm.ModuleID = ht.ModuleID ", this.DnnPrefix);
            sql.AppendFormat("    INNER JOIN {0}Modules m ON ht.ModuleID = m.ModuleID ", this.DnnPrefix);
            sql.AppendFormat("    INNER JOIN {0}Tabs t ON tm.TabID = t.TabID ", this.DnnPrefix);

            // using LIKE because we can't use = against DesktopSummary since it's an NText field
            sql.Append("WHERE ht.DesktopSummary LIKE ''");
            sql.Append("    AND m.PortalID = @portalId");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets a list of the Text/HTML modules with content matching the search string.
        /// </summary>
        /// <param name="searchValue">The search string.</param>
        /// <returns>The Text/HTML modules with content matching the search string</returns>
        public override IDataReader GetMatchingHtmlTextModules(string searchValue)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT ht.ModuleID, tm.TabID, ht.DesktopHtml, ht.DesktopSummary, m.ModuleTitle, t.TabName ");
            sql.AppendFormat("FROM {0}HtmlText ht ", this.DnnPrefix);
            sql.AppendFormat("	JOIN {0}TabModules tm ON (tm.ModuleID = ht.ModuleID)", this.DnnPrefix);
            sql.AppendFormat("	JOIN {0}Modules m ON (m.ModuleID = tm.ModuleID)", this.DnnPrefix);
            sql.AppendFormat("	JOIN {0}Tabs t ON (t.TabID = tm.TabID)", this.DnnPrefix);
            sql.Append("WHERE ht.DesktopHtml collate SQL_Latin1_General_CP1_CS_AS LIKE @searchValue");
            
            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateVarcharParam("@searchValue", "%" + searchValue + "%"));
        }

        /// <summary>
        /// Gets a list of the Text/HTML modules in the given portal with content matching the search string.
        /// </summary>
        /// <param name="searchValue">The search string.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The Text/HTML modules in the given portal with content matching the search string</returns>
        public override IDataReader GetMatchingHtmlTextModules(string searchValue, int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT ht.ModuleID, tm.TabID, ht.DesktopHtml, ht.DesktopSummary, m.ModuleTitle, t.TabName ");
            sql.AppendFormat("FROM {0}HtmlText ht ", this.DnnPrefix);
            sql.AppendFormat("	JOIN {0}TabModules tm on (tm.ModuleID = ht.ModuleID)", this.DnnPrefix);
            sql.AppendFormat("	JOIN {0}Modules m on (m.ModuleID = tm.ModuleID)", this.DnnPrefix);
            sql.AppendFormat("	JOIN {0}Tabs t on (t.TabID = tm.TabID)", this.DnnPrefix);
            sql.Append("WHERE ht.DesktopHtml collate SQL_Latin1_General_CP1_CS_AS LIKE @searchValue ");
            sql.Append(" AND m.PortalId = @portalId");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId),
                Utility.CreateVarcharParam("@searchValue", "%" + searchValue + "%"));
        }

        /// <summary>
        /// Gets the module settings for the given module.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <returns>The module settings for the given module</returns>
        public override IDataReader GetModuleSettings(int moduleId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT ms.SettingName, ms.SettingValue ");
            sql.AppendFormat("FROM {0}ModuleSettings ms ", this.DnnPrefix);
            sql.Append("WHERE ms.ModuleID = @moduleId");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@moduleId", moduleId));
        }

        /// <summary>
        /// Gets a list of the pages that are set not to be visible in the menu.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The pages that are set not to be visible in the menu</returns>
        public override IDataReader GetPagesNotVisible(int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT t.TabID, t.TabName, t.TabPath ");
            sql.AppendFormat("FROM {0}Tabs t ", this.DnnPrefix);
            sql.Append("WHERE t.IsVisible = 'False' ");
            sql.Append("    AND t.TabName <> 'Search Results' ");
            sql.Append("	AND t.PortalID = @portalId");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets a list of the pages that don't have a description defined.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of the pages that don't have a description defined</returns>
        public override IDataReader GetPagesWithoutDescription(int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("DECLARE @AdminTabID int ");
            sql.AppendFormat("SET @AdminTabID = (SELECT t.TabID FROM {0}Tabs t WHERE t.TabName = 'Admin' AND t.ParentID IS NULL AND t.Level = 0 AND t.PortalID = @portalId) ", this.DnnPrefix);
            
            sql.Append("SELECT t.TabID, t.TabName, t.TabPath ");
            sql.AppendFormat("FROM {0}Tabs t ", this.DnnPrefix);
            sql.Append("WHERE t.PortalID = @portalId ");
            sql.Append("	AND (t.ParentId <> @AdminTabID OR t.ParentId IS NULL) ");
            sql.Append("	AND t.TabName <> 'Admin' ");
            sql.Append("	AND t.Description = '' ");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets a list of the pages that don't have keywords defined.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of the pages that don't have keywords defined</returns>
        public override IDataReader GetPagesWithoutKeywords(int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("DECLARE @AdminTabID int ");
            sql.AppendFormat("SET @AdminTabID = (SELECT t.TabID FROM {0}Tabs t WHERE t.TabName = 'Admin' AND t.ParentID IS NULL AND t.Level = 0 AND t.PortalID = @portalId) ", this.DnnPrefix);
            
            sql.Append("SELECT t.TabID, t.TabName, t.TabPath ");
            sql.AppendFormat("FROM {0}Tabs t ", this.DnnPrefix);
            sql.Append("WHERE t.PortalID = @portalId");
            sql.Append("	AND (t.ParentId <> @AdminTabID OR t.ParentId IS NULL) ");
            sql.Append("	AND t.TabName <> 'Admin' ");
            sql.Append("	AND t.Keywords = ''");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets a list of Engage: Publish articles in the given portal that match the search string
        /// </summary>
        /// <param name="searchValue">The search string.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of Engage: Publish articles in the given portal that match the search string</returns>
        public override IDataReader GetPublishLinks(string searchValue, int portalId)
        {
            StringBuilder sql = new StringBuilder(128);

            sql.Append("SELECT va.name, va.ItemId, va.articletext, va.displaytabid, t.TabName ");
            sql.AppendFormat("FROM {0}publish_vwarticles va ", this.DnnPrefix);
            sql.AppendFormat("	JOIN {0}Tabs t on t.TabID = va.displaytabid ", this.DnnPrefix);
            sql.Append("WHERE va.articletext collate SQL_Latin1_General_CP1_CS_AS LIKE @searchValue ");
            sql.Append(" and va.IsCurrentVersion = 1 and va.PortalId = @portalId");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@portalId", portalId),
                Utility.CreateVarcharParam("@searchValue", "%" + searchValue + "%"));
        }

        /// <summary>
        /// Gets the number of rows in the given table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>The number of rows in the given table</returns>
        public override int GetRowCount(string tableName)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT COUNT(*) ");
            sql.AppendFormat("FROM {0}{1}", this.DnnPrefix, tableName);

            return (int)SqlHelper.ExecuteScalar(this.ConnectionString, CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// Gets a list of the tabs that the given module is on.
        /// </summary>
        /// <param name="desktopModuleId">The desktop module id.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of the tabs on which the given module is</returns>
        public override IDataReader GetTabsForModule(int desktopModuleId, int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT tm.ModuleID, tm.TabModuleID, tm.TabID, t.TabName, m.ModuleTitle, t.TabPath ");
            sql.AppendFormat("FROM {0}TabModules tm ", this.DnnPrefix);
            sql.AppendFormat("	INNER JOIN {0}Modules m ON m.ModuleId = tm.ModuleID ", this.DnnPrefix);
            sql.AppendFormat("	INNER JOIN {0}Tabs t ON tm.TabID = t.TabID ", this.DnnPrefix);
            sql.AppendFormat("	INNER JOIN {0}ModuleDefinitions md ON md.ModuleDefID = m.ModuleDefID ", this.DnnPrefix);
            sql.Append("WHERE m.portalId = @portalId ");
            sql.Append("	AND md.desktopModuleId = @desktopModuleId");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(), 
                Utility.CreateIntegerParam("@portalId", portalId),
                Utility.CreateIntegerParam("@desktopModuleId", desktopModuleId));
        }

        /// <summary>
        /// Gets the tab module settings for the given tab module id.
        /// </summary>
        /// <param name="tabModuleId">The tab module id.</param>
        /// <returns>The tab module settings for the given tab module id</returns>
        public override IDataReader GetTabModuleSettings(int tabModuleId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT tms.SettingName, tms.SettingValue ");
            sql.AppendFormat("FROM {0}TabModuleSettings tms ", this.DnnPrefix);
            sql.Append("WHERE tms.TabModuleID = @tabModuleId");

            return SqlHelper.ExecuteReader(
                this.ConnectionString, 
                CommandType.Text, 
                sql.ToString(),
                Utility.CreateIntegerParam("@tabModuleId", tabModuleId));
        }

        /// <summary>
        /// Gets a list of the installed modules that are not placed on any pages.
        /// </summary>
        /// <returns>A list of the installed modules that are not placed on any pages</returns>
        public override IDataReader GetUnusedModules()
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT TOP 100 PERCENT dm.FriendlyName, dm.Version, dm.IsPremium ");
            sql.AppendFormat("FROM {0}Modules m ", this.DnnPrefix);
            sql.AppendFormat("	INNER JOIN {0}TabModules tm ON m.ModuleID = tm.ModuleID ", this.DnnPrefix);
            sql.AppendFormat("	INNER JOIN {0}ModuleDefinitions md ON m.ModuleDefID = md.ModuleDefID ", this.DnnPrefix);
            sql.AppendFormat("	RIGHT OUTER JOIN {0}DesktopModules dm ON md.DesktopModuleID = dm.DesktopModuleID ", this.DnnPrefix);
            sql.Append("WHERE (dm.IsAdmin = 0) ");
            sql.Append("GROUP BY dm.DesktopModuleID, dm.Version, dm.IsPremium, dm.FriendlyName ");
            sql.Append("HAVING (COUNT(tm.TabID) = 0) ");
            sql.Append("ORDER BY dm.FriendlyName");

            return SqlHelper.ExecuteReader(this.ConnectionString, CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// Gets a list of the number of users registered each day over the given date span.
        /// </summary>
        /// <param name="startDate">The begin date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>
        /// A list of the number of users registered each day over the given date span
        /// </returns>
        public override IDataReader GetUserRegistrationsInDateSpan(DateTime startDate, DateTime endDate, int portalId)
        {
            StringBuilder sql = new StringBuilder(512);
            sql.Append("SELECT COUNT(*) AS 'Users Registered', DATEADD(dd, DATEDIFF(dd, 0, up.CreatedDate), 0) AS 'Date' ");
            sql.AppendFormat("FROM {0}Users u ", this.DnnPrefix);
            sql.AppendFormat(" INNER JOIN {0}UserPortals up ON u.UserID = up.UserId ", this.DnnPrefix);
            sql.Append("WHERE up.CreatedDate < @endDate + 1 ");
            sql.Append(" AND up.CreatedDate >= @startDate ");
            sql.Append(" AND up.PortalID = @portalId ");
            sql.Append("GROUP BY DATEADD(dd, DATEDIFF(dd, 0, up.CreatedDate), 0) ");

            return SqlHelper.ExecuteReader(
                this.ConnectionString,
                CommandType.Text,
                sql.ToString(),
                Utility.CreateDateTimeParam("@startDate", startDate),
                Utility.CreateDateTimeParam("@endDate", endDate),
                Utility.CreateIntegerParam("@portalId", portalId));
        }

        /// <summary>
        /// Gets the number of unique user logins for the given date span.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of unique user logins for the given date span</returns>
        public override int GetNumberOfUserLoginsInDateSpan(DateTime startDate, DateTime endDate, int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT COUNT(*) ");
            sql.AppendFormat("FROM {0}aspnet_Membership m ", this.DatabaseOwner);
            sql.AppendFormat("  INNER JOIN {0}aspnet_Users au ON au.UserId = m.UserId ", this.DatabaseOwner);
            sql.AppendFormat("  INNER JOIN {0}Users u on u.UserName = au.UserName ", this.DnnPrefix);
            sql.AppendFormat("  INNER JOIN {0}UserPortals up on up.UserId = u.UserId ", this.DnnPrefix);
            sql.Append("WHERE m.LastLoginDate < @endDate + 1 ");
            sql.Append("    AND m.LastLoginDate >= @startDate ");
            sql.Append("    AND up.PortalID = @portalId");

            return Convert.ToInt32(
                SqlHelper.ExecuteScalar(
                    this.ConnectionString, 
                    CommandType.Text, 
                    sql.ToString(), 
                    Utility.CreateDateTimeParam("@startDate", startDate), 
                    Utility.CreateDateTimeParam("@endDate", endDate), 
                    Utility.CreateIntegerParam("@portalId", portalId)), 
                CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the number of user registrations in a date span.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of user registrations in a date span</returns>
        public override int GetNumberOfUserRegistrationsInDateSpan(DateTime startDate, DateTime endDate, int portalId)
        {
            StringBuilder sql = new StringBuilder(128);
            sql.Append("SELECT COUNT(*) ");
            sql.AppendFormat("FROM {0}Users u ", this.DnnPrefix);
            sql.AppendFormat("  INNER JOIN {0}UserPortals up ON u.UserID = up.UserId ", this.DnnPrefix);
            sql.Append("WHERE up.CreatedDate < @endDate + 1 ");
            sql.Append("    AND up.CreatedDate >= @startDate ");
            sql.Append("    AND up.PortalID = @portalId");

            return Convert.ToInt32(
                SqlHelper.ExecuteScalar(
                    this.ConnectionString,
                    CommandType.Text,
                    sql.ToString(),
                    Utility.CreateDateTimeParam("@startDate", startDate),
                    Utility.CreateDateTimeParam("@endDate", endDate),
                    Utility.CreateIntegerParam("@portalId", portalId)), 
                CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Updates the given Text/HTML module with the given HTML and summary.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="desktopHtml">The desktop HTML.</param>
        /// <param name="desktopSummary">The desktop summary.</param>
        /// <param name="userId">The user id.</param>
        public override void ReplaceTextHtml(int moduleId, string desktopHtml, string desktopSummary, int userId)
        {
            SqlHelper.ExecuteNonQuery(
                this.ConnectionString, 
                this.DatabaseOwner + this.ObjectQualifier + "UpdateHtmlText", 
                Utility.CreateIntegerParam("@ModuleId", moduleId),
                Utility.CreateTextParam("@DesktopHtml", desktopHtml), 
                Utility.CreateTextParam("@DesktopSummary", desktopSummary), 
                Utility.CreateIntegerParam("@UserID", userId));
        }

        /// <summary>
        /// Whether the database supports SQL Server 2005 functionality.
        /// </summary>
        /// <returns><c>true</c> if the database supports SQL Server 2005 functionality; otherwise, <c>false</c></returns>
        public override bool SupportsSql2005Functionality()
        {
            string version = SqlHelper.ExecuteScalar(this.ConnectionString, CommandType.Text, "SELECT SERVERPROPERTY('ProductVersion')").ToString();
            List<int> versionNumbers = Utility.ParseIntegerList(version.Split('.'), CultureInfo.InvariantCulture);
            return versionNumbers.Count > 0 && versionNumbers[0] > SqlServer2000MajorVersionNumber;
        }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <returns>The name of the database</returns>
        private string GetDatabaseName()
        {
            if (!Utility.HasValue(this.databaseName))
            {
                this.databaseName = SqlHelper.ExecuteScalar(this.ConnectionString, CommandType.Text, "SELECT DB_NAME()").ToString();
            }

            return this.databaseName;
        }

        /////// <summary>
        /////// Gets the fully qualified name of an Engage: Dashboard database object.
        /////// </summary>
        /////// <param name="name">The unqualified database object name</param>
        /////// <returns>The fully qualified name of an Engage: Dashboard database object</returns>
        ////private string GetFullyQualifiedName(string name)
        ////{
        ////    return this.DatabaseOwner + this.ObjectQualifier + ModuleQualifier + name;
        ////}
    }
}