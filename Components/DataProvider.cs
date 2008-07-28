// <copyright file="DataProvider.cs" company="Engage Software">
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
    using DotNetNuke.Framework;

    /// <summary>
    /// An abstract class for the data access layer
    /// </summary>
    public abstract class DataProvider
    {
        #region Shared/Static Methods

        /// <summary>
        /// Singleton reference to the instantiated object 
        /// </summary>
        private static readonly DataProvider provider = (DataProvider)Reflection.CreateObject("data", "Engage.Dnn.Dashboard", string.Empty);

        /// <summary>
        /// Gets the concrete instance of the <see cref="DataProvider"/>.
        /// </summary>
        /// <returns>The concrete instance of the <see cref="DataProvider"/></returns>
        public static new DataProvider Instance()
        {
            return provider;
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Gets the number of pages in the given portal.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of pages in the given portal</returns>
        public abstract int CountPages(int portalId);

        /// <summary>
        /// Gets the number of deleted module and pages in the recycle bin fir the given portal.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of deleted module and pages in the recycle bin for the given portal</returns>
        public abstract int CountRecycleBin(int portalId);

        /// <summary>
        /// Gets the number of roles in the given portal.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of roles in the given portal</returns>
        public abstract int CountRoles(int portalId);

        /// <summary>
        /// Gets the modules in the current portal that are only visible to administrators.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A DataTable of the modules in the current portal that are only visible to administrators</returns>
        public abstract IDataReader GetAdminOnlyModules(int portalId);

        /// <summary>
        /// Gets the pages in the given portal that are only visible to administrators.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A DataTable of the pages in the given portal that are only visible to administrators</returns>
        public abstract IDataReader GetAdminOnlyPages(int portalId);

        /// <summary>
        /// Gets the backup history of the current site.
        /// </summary>
        /// <returns>A DataTable of the backup history of the current site</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The method performs a time-consuming operation. The method is perceivably slower than the time it takes to set or get a field's value.")]
        public abstract IDataReader GetDatabaseBackupHistory();

        /// <summary>
        /// Gets the size of the data file for this database in MB.
        /// </summary>
        /// <returns>The size of the data file for this database in MB</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The method performs a time-consuming operation. The method is perceivably slower than the time it takes to set or get a field's value.")]
        public abstract string GetSizeOfDatabase();

        /// <summary>
        /// Gets a list of the pages in the given portal that have no modules on them.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of the pages in the given portal that have no modules on them</returns>
        public abstract IDataReader GetEmptyPages(int portalId);

        /// <summary>
        /// Gets the number of failed scheduler tasks in the given date span.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>The number of failed scheduler tasks in the given date span</returns>
        public abstract int GetFailedSchedulesInDateSpan(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets information about the HTML/Text modules in the given portal that don't have a search summary defined.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>Information about the HTML/Text modules in the given portal that don't have a search summary defined</returns>
        public abstract IDataReader GetHtmlTextModulesWithoutSummary(int portalId);
        
        /// <summary>
        /// Gets a list of the Text/HTML modules in the given portal with content matching the search string.
        /// </summary>
        /// <param name="searchValue">The search string.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A DataTable of the Text/HTML modules in the given portal with content matching the search string</returns>
        public abstract IDataReader GetMatchingHtmlTextModules(string searchValue, int portalId);

        /// <summary>
        /// Gets a list of the Text/HTML modules with content matching the search string.
        /// </summary>
        /// <param name="searchValue">The search string.</param>
        /// <returns>A DataTable of the Text/HTML modules with content matching the search string</returns>
        public abstract IDataReader GetMatchingHtmlTextModules(string searchValue);

        /// <summary>
        /// Gets the size of the log file for the database.
        /// </summary>
        /// <returns>The size of the database log in MB.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The method performs a time-consuming operation. The method is perceivably slower than the time it takes to set or get a field's value.")]
        public abstract string GetDatabaseLogSize();

        /// <summary>
        /// Gets the module settings for the given module.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <returns>A DataTable of the module settings for the given module</returns>
        public abstract IDataReader GetModuleSettings(int moduleId);

        /// <summary>
        /// Gets a list of the pages that are set not to be visible in the menu.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A DataTable of the pages that are set not to be visible in the menu</returns>
        public abstract IDataReader GetPagesNotVisible(int portalId);

        /// <summary>
        /// Gets a list of the pages that don't have a description defined.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of the pages that don't have a description defined</returns>
        public abstract IDataReader GetPagesWithoutDescription(int portalId);

        /// <summary>
        /// Gets a list of the pages that don't have keywords defined.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of the pages that don't have keywords defined</returns>
        public abstract IDataReader GetPagesWithoutKeywords(int portalId);

        /// <summary>
        /// Gets a list of Engage: Publish articles in the given portal that match the search string
        /// </summary>
        /// <param name="searchValue">The search string.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of Engage: Publish articles in the given portal that match the search string</returns>
        public abstract IDataReader GetPublishLinks(string searchValue, int portalId);

        /// <summary>
        /// Gets the number of rows in the given table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>The number of rows in the given table</returns>
        public abstract int GetRowCount(string tableName);

        /// <summary>
        /// Gets a list of the tabs that the given module is on.
        /// </summary>
        /// <param name="desktopModuleId">The desktop module id.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>A list of the tabs on which the given module is</returns>
        public abstract IDataReader GetTabsForModule(int desktopModuleId, int portalId);

        /// <summary>
        /// Gets the tab module settings for the given tab module id.
        /// </summary>
        /// <param name="tabModuleId">The tab module id.</param>
        /// <returns>Tthe tab module settings for the given tab module id</returns>
        public abstract IDataReader GetTabModuleSettings(int tabModuleId);

        /// <summary>
        /// Gets a list of the installed modules that are not placed on any pages.
        /// </summary>
        /// <returns>A list of the installed modules that are not placed on any pages</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The method performs a time-consuming operation. The method is perceivably slower than the time it takes to set or get a field's value.")]
        public abstract IDataReader GetUnusedModules();

        /// <summary>
        /// Gets the number of unique user logins for the given date span.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of unique user logins for the given date span</returns>
        public abstract int GetUserLoginsInDateSpan(DateTime startDate, DateTime endDate, int portalId);

        /// <summary>
        /// Gets the number of user registrations in a date span.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="portalId">The portal id.</param>
        /// <returns>The number of user registrations in a date span</returns>
        public abstract int GetNumberOfUserRegistrationsInDateSpan(DateTime startDate, DateTime endDate, int portalId);

        /// <summary>
        /// Updates the given Text/HTML module with the given HTML and summary.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <param name="desktopHtml">The desktop HTML.</param>
        /// <param name="desktopSummary">The desktop summary.</param>
        /// <param name="userId">The user id.</param>
        public abstract void ReplaceTextHtml(int moduleId, string desktopHtml, string desktopSummary, int userId);
        
        /// <summary>
        /// Whether the database supports SQL Server 2005 functionality.
        /// </summary>
        /// <returns><c>true</c> if the database supports SQL Server 2005 functionality; otherwise, <c>false</c></returns>
        public abstract bool SupportsSql2005Functionality();

        #endregion
    }
}