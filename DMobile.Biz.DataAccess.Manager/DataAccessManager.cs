using System;
using System.Collections.Generic;
using System.Data;
using DMobile.Biz.DataAccess.Manager.DataEntity;

namespace DMobile.Biz.DataAccess.Manager
{
    public static class DataAccessManager
    {
        #region Public Method

        public static T ExecuteDataReader<T>(string commandName, IDataParameter[] dps, Func<IDataReader, T> executeRead)
        {
            return TryDo(() =>
                {
                    DataCommand dataCommand = DataCommandDetector.GetDataCommand(commandName);

                    BuilderCommandParame(dps, dataCommand);

                    if (executeRead != null)
                    {
                        using (IDataReader reader = dataCommand.ExecuteDataReader())
                        {
                            return executeRead(reader);
                        }
                    }
                    return default(T);
                });
        }

        public static int ExecuteNonQuery(string commandName, IDataParameter[] dps)
        {
            return TryDo(() =>
                {
                    DataCommand dataCommand = DataCommandDetector.GetDataCommand(commandName);
                    BuilderCommandParame(dps, dataCommand);
                    return dataCommand.ExecuteNonQuery();
                });
        }

        public static T ExecuteScalar<T>(string commandName, IDataParameter[] dps)
        {
            return TryDo(() =>
                {
                    T result = default(T);
                    DataCommand dataCommand = DataCommandDetector.GetDataCommand(commandName);
                    BuilderCommandParame(dps, dataCommand);
                    object obj = dataCommand.ExecuteScalar();
                    try
                    {
                        if (obj != null)
                            result = (T) obj;
                    }
                    catch
                    {
                        result = default(T);
                    }
                    return result;
                });
        }

        public static void ExecuteCommand(string commandName, Action<DataCommand> exec)
        {
            TryDo<DataCommand>(() =>
                {
                    exec(DataCommandDetector.GetDataCommand(commandName));
                    return null;
                });
        }

        #endregion Public Method

        #region Private Method

        private static void BuilderCommandParame(IEnumerable<IDataParameter> dps, DataCommand dataCommand)
        {
            if (dps != null)
            {
                foreach (IDataParameter dp in dps)
                {
                    dataCommand.SetParameterValue(dp.ParameterName, dp.Value);
                }
            }
        }

        private static T TryDo<T>(Func<T> action)
        {
            T t = default(T);
            if (action != null)
            {
                try
                {
                    t = action();
                }
                catch
                {
                    throw;
                }
            }

            return t;
        }

        #endregion Private Method
    }
}