using System;

namespace DMobile.Biz.DataAccess.Manager.DataEntity
{
    public class DatabaseNotSpecifiedException : Exception
    {
    }

    public class DataCommandFileNotSpecifiedException : Exception
    {
    }

    public class DataCommandFileLoadException : Exception
    {
        public DataCommandFileLoadException(string fileName)
            : base("DataCommand file " + fileName + " not found or is invalid.")
        {
        }
    }
}