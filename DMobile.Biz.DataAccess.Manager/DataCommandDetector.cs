using System;
using System.Collections.Generic;
using System.IO;
using DMobile.Biz.DataAccess.Manager.DataEntity;
using DMobile.Server.Utilities;
using DMobile.Server.Utilities.IO;

namespace DMobile.Biz.DataAccess.Manager
{
    public static class DataCommandDetector
    {
        #region Field Define

        private static readonly FileSystemChangeEventHandler s_FileChangeHandler;
        private static readonly Object s_CommandSyncObject;
        private static readonly Object s_CommandFileListSyncObject;
        private static readonly FileSystemWatcher s_Watcher;
        private static Dictionary<string, DataCommand> s_DataCommands;
        private static readonly string s_DataFileFolder;
        private static Dictionary<string, IList<string>> s_FileCommands;

        #endregion

        #region constructor

        static DataCommandDetector()
        {
            s_FileChangeHandler = new FileSystemChangeEventHandler(Configer.FileChangeInteravl);
            s_FileChangeHandler.ActualHandler += Watcher_Changed;

            s_DataFileFolder = Path.GetDirectoryName(Configer.DataCommandFileListConfigFile);

            s_CommandSyncObject = new object();
            s_CommandFileListSyncObject = new object();

            if (s_DataFileFolder != null)
            {
                s_Watcher = new FileSystemWatcher(s_DataFileFolder);
                s_Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
                s_Watcher.Changed += s_FileChangeHandler.ChangeEventHandler;
                s_Watcher.EnableRaisingEvents = true;

                UpdateAllCommandFiles();
            }
        }

        #endregion

        #region Public Function

        public static DataCommand GetDataCommand(string name)
        {
            return s_DataCommands[name].Clone();
        }

        #endregion

        #region Public Event

        #endregion

        #region Private Function

        private static void UpdateCommandFile(string fileName)
        {
            IList<string> commandNames;
            if (s_FileCommands.ContainsKey(fileName))
            {
                commandNames = s_FileCommands[fileName];
            }
            else
            {
                commandNames = null;
            }

            lock (s_CommandSyncObject)
            {
                var newCommands = new Dictionary<string, DataCommand>(s_DataCommands);

                if (commandNames != null)
                {
                    foreach (string commandName in commandNames)
                    {
                        newCommands.Remove(commandName);
                    }
                }

                DataOperations commands = SerializerProvider.SerializerHelper<DataOperations>.LoadFromXmlFile(fileName);
                if (commands == null)
                {
                    throw new DataCommandFileLoadException(fileName);
                }
                if (commands.DataCommand != null && commands.DataCommand.Length > 0)
                {
                    foreach (DataOperationsDataCommand cmd in commands.DataCommand)
                    {
                        try
                        {
                            string databaseName = cmd.Database ?? commands.DefaultSetting.Database;
                            DbUtility database = DatabaseDetector.GetDatabase(databaseName);
                            if (database != null)
                            {
                                DataCommand dataCmd = cmd.GetDataCommand(databaseName, database);
                                newCommands.Add(cmd.Name, dataCmd);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Command:" + cmd.Name + " has exists.", ex);
                        }
                    }
                    s_DataCommands = newCommands;
                }

                s_FileCommands[fileName] = commands.GetCommandNames();
            }
        }

        private static void UpdateAllCommandFiles()
        {
            lock (s_CommandFileListSyncObject)
            {
                DataCommandFiles fileList =
                    SerializerProvider.SerializerHelper<DataCommandFiles>.LoadFromXmlFile(
                        Configer.DataCommandFileListConfigFile);
                if (fileList == null || fileList.Files == null || fileList.Files.Length == 0)
                {
                    throw new DataCommandFileNotSpecifiedException();
                }

                s_FileCommands = new Dictionary<string, IList<string>>();

                s_DataCommands = new Dictionary<string, DataCommand>();


                foreach (FileMajor commandFile in fileList.Files)
                {
                    string fileName = Path.Combine(s_DataFileFolder, commandFile.Location);
                    UpdateCommandFile(fileName);
                }
            }
        }

        #endregion

        #region Private Event

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (
                String.Compare(e.FullPath, Configer.DataCommandFileListConfigFile, StringComparison.OrdinalIgnoreCase) ==
                0)
            {
                UpdateAllCommandFiles();
            }
            else
            {
                lock (s_CommandFileListSyncObject)
                {
                    foreach (string file in s_FileCommands.Keys)
                    {
                        if (String.Compare(file, e.FullPath, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            UpdateCommandFile(file);
                            break;
                        }
                    }
                }
            }
        }

        #endregion
    }
}