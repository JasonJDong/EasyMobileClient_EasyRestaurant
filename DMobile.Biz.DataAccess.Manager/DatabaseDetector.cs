using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using DMobile.Biz.DataAccess.DALFactory;
using DMobile.Biz.DataAccess.Manager.DataEntity;
using DMobile.Server.Utilities;
using DMobile.Server.Utilities.IO;

namespace DMobile.Biz.DataAccess.Manager
{
    internal static class DatabaseDetector
    {
        #region Field Define

        private static Dictionary<string, DbUtility> s_DatabaseHashtable;
        private static readonly FileSystemWatcher s_Watcher;
        private static readonly FileSystemChangeEventHandler s_FileChangeHandler;

        #endregion

        #region constructor

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        static DatabaseDetector()
        {
            string databaseFolder = String.Empty;
            string databaseFile = String.Empty;

            databaseFolder = Path.GetDirectoryName(Configer.ServerListFileLocation);
            databaseFile = Path.GetFileName(Configer.ServerListFileLocation);

            s_DatabaseHashtable = new Dictionary<string, DbUtility>();
            s_FileChangeHandler = new FileSystemChangeEventHandler(Configer.FileChangeInteravl);
            s_FileChangeHandler.ActualHandler += OnFileChanged;

            if (databaseFolder != null)
            {
                s_Watcher = new FileSystemWatcher(databaseFolder);

                s_Watcher.Filter = databaseFile;
                s_Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
                s_Watcher.Changed += s_FileChangeHandler.ChangeEventHandler;
                s_Watcher.EnableRaisingEvents = true;

                s_DatabaseHashtable = LoadDatabaseList();
            }
        }

        #endregion

        #region Public Function

        public static DbUtility GetDatabase(string name)
        {
            DbUtility dataBase = null;

            name = name.ToUpper();

            if (s_DatabaseHashtable.ContainsKey(name))
            {
                dataBase = s_DatabaseHashtable[name];
            }

            return dataBase;
        }

        #endregion

        #region Private Function

        private static Dictionary<string, DbUtility> LoadDatabaseList()
        {
            DatabaseList list =
                SerializerProvider.SerializerHelper<DatabaseList>.LoadFromXmlFile(Configer.ServerListFileLocation);

            if (list == null || list.Databases == null || list.Databases.Length == 0)
            {
                throw new DatabaseNotSpecifiedException();
            }

            var hashtable = new Dictionary<string, DbUtility>(list.Databases.Length);

            foreach (DatabaseMajor instance in list.Databases)
            {
                DbUtility database = VisitorFactory.CreateComDataVisitor(instance.Connection.DataProvider.Value);
                database.Provider = instance.Connection.DataProvider.Value;
                database.DataServer = instance.Connection.DataServer.Value;
                database.DataSource = instance.Connection.DataSource.Value;
                database.UserID = instance.Connection.DataUserID.Value;
                database.Password = instance.Connection.DataPassword.Value;
                database.Initialize();
                //TODO:此处应解析Password
                hashtable.Add(instance.Name.ToUpper(), database);
            }

            return hashtable;
        }

        #endregion

        #region Private Event

        private static void OnFileChanged(Object sender, FileSystemEventArgs e)
        {
            s_DatabaseHashtable = LoadDatabaseList();
        }

        #endregion
    }
}