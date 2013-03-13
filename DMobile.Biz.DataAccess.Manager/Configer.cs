using System;
using System.Configuration;
using System.IO;

namespace DMobile.Biz.DataAccess.Manager
{
    public class Configer
    {
        private static string m_ServerListFileLocation;

        public static string m_DataCommandFileListConfigFile;

        private static int m_FileChangeInteravl = -1;

        public static string ServerListFileLocation
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_ServerListFileLocation))
                {
                    m_ServerListFileLocation = ConfigurationManager.AppSettings["ServerList"];

                    m_ServerListFileLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                            m_ServerListFileLocation.Replace('/', '\\').TrimStart('\\'));
                }

                return m_ServerListFileLocation;
            }
        }

        public static string DataCommandFileListConfigFile
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_DataCommandFileListConfigFile))
                {
                    m_DataCommandFileListConfigFile = ConfigurationManager.AppSettings["DataCommandFiles"];

                    m_DataCommandFileListConfigFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                                   m_DataCommandFileListConfigFile.Replace('/', '\\').
                                                                       TrimStart('\\'));
                }

                return m_DataCommandFileListConfigFile;
            }
        }

        public static int FileChangeInteravl
        {
            get
            {
                if (m_FileChangeInteravl == -1)
                {
                    object objInterval = ConfigurationManager.AppSettings["FileChangeInteravl"];

                    if (objInterval == null)
                    {
                        m_FileChangeInteravl = 500;
                    }
                    else
                    {
                        try
                        {
                            m_FileChangeInteravl = Convert.ToInt32(objInterval);
                        }
                        catch
                        {
                            m_FileChangeInteravl = 500;
                        }
                    }
                }

                return m_FileChangeInteravl;
            }
        }
    }
}
