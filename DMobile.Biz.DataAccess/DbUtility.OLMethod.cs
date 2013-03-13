using System;
using System.Data;
using System.Data.Common;

namespace DMobile.Biz.DataAccess
{
    partial class DbUtility
    {
        #region CreateDbParameter 方法的重载

        /// <summary>
        /// 创建实现 System.Data.Common.DbParameter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>System.Data.Common.DbParameter 类的提供程序的类的新实例。</returns>
        public DbParameter CreateDbParameter()
        {
            try
            {
                DbParameter dbParam = null;

                if (_dbFactory != null)
                {
                    dbParam = _dbFactory.CreateParameter();
                }

                return dbParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbParameter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="parameterName">要映射的参数的名称。</param>
        /// <param name="value">一个 System.Object，它是 System.Data.Common.DbParameter 的值。</param>
        /// <returns>System.Data.Common.DbParameter 类的提供程序的类的新实例。</returns>
        public DbParameter CreateDbParameter(string parameterName, object value)
        {
            try
            {
                DbParameter dbParam = null;

                dbParam = CreateDbParameter();
                if (dbParam != null)
                {
                    dbParam.ParameterName = parameterName;
                    dbParam.Value = value;
                }

                return dbParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbParameter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="parameterName">要映射的参数的名称。</param>
        /// <param name="dbType">System.Data.DbType 值之一。</param>
        /// <returns>System.Data.Common.DbParameter 类的提供程序的类的新实例。</returns>
        public DbParameter CreateDbParameter(string parameterName, DbType dbType)
        {
            try
            {
                DbParameter dbParam = null;

                dbParam = CreateDbParameter();
                if (dbParam != null)
                {
                    dbParam.ParameterName = parameterName;
                    dbParam.DbType = dbType;
                }

                return dbParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbParameter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="parameterName">要映射的参数的名称。</param>
        /// <param name="dbType">System.Data.DbType 值之一。</param>
        /// <param name="size">参数的长度。</param>
        /// <returns>System.Data.Common.DbParameter 类的提供程序的类的新实例。</returns>
        public DbParameter CreateDbParameter(string parameterName, DbType dbType, int size)
        {
            try
            {
                DbParameter dbParam = null;

                dbParam = CreateDbParameter(parameterName, dbType);
                if (dbParam != null)
                {
                    dbParam.Size = size;
                }

                return dbParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbParameter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="parameterName">要映射的参数的名称。</param>
        /// <param name="dbType">System.Data.DbType 值之一。</param>
        /// <param name="size">参数的长度。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <returns>System.Data.Common.DbParameter 类的提供程序的类的新实例。</returns>
        public DbParameter CreateDbParameter(string parameterName, DbType dbType, int size, string sourceColumn)
        {
            try
            {
                DbParameter dbParam = null;

                dbParam = CreateDbParameter(parameterName, dbType, size);
                if (dbParam != null)
                {
                    dbParam.SourceColumn = sourceColumn;
                }

                return dbParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbParameter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="parameterName">要映射的参数的名称。</param>
        /// <param name="dbType">System.Data.DbType 值之一。</param>
        /// <param name="size">参数的长度。</param>
        /// <param name="direction">System.Data.ParameterDirection 值之一。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">System.Data.DataRowVersion 值之一。</param>
        /// <param name="value">一个 System.Object，它是 System.Data.Common.DbParameter 的值。</param>
        /// <returns>System.Data.Common.DbParameter 类的提供程序的类的新实例。</returns>
        public DbParameter CreateDbParameter(string parameterName, DbType dbType, int size, ParameterDirection direction,
                                             string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            try
            {
                DbParameter dbParam = null;

                dbParam = CreateDbParameter(parameterName, dbType, size, sourceColumn);
                if (dbParam != null)
                {
                    dbParam.Direction = direction;
                    dbParam.SourceVersion = sourceVersion;
                    dbParam.Value = value;
                }

                return dbParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbParameter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="parameterName">要映射的参数的名称。</param>
        /// <param name="dbType">System.Data.DbType 值之一。</param>
        /// <param name="size">参数的长度。</param>
        /// <param name="direction">System.Data.ParameterDirection 值之一。</param>
        /// <param name="sourceColumn">源列的名称。</param>
        /// <param name="sourceVersion">System.Data.DataRowVersion 值之一。</param>
        /// <param name="sourceColumnNullMapping">如果源列可为空，则为true；如果不可为空，则为false。</param>
        /// <param name="value">一个 System.Object，它是 System.Data.Common.DbParameter 的值。</param>
        /// <returns>System.Data.Common.DbParameter 类的提供程序的类的新实例。</returns>
        public DbParameter CreateDbParameter(string parameterName, DbType dbType, int size, ParameterDirection direction,
                                             string sourceColumn, DataRowVersion sourceVersion,
                                             bool sourceColumnNullMapping, object value)
        {
            try
            {
                DbParameter dbParam = null;

                dbParam = CreateDbParameter(parameterName, dbType, size, direction, sourceColumn, sourceVersion, value);
                if (dbParam != null)
                {
                    dbParam.SourceColumnNullMapping = sourceColumnNullMapping;
                }

                return dbParam;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion CreateDbParameter 方法的重载

        #region CreateDbConnection 方法的重载

        /// <summary>
        /// 创建实现 System.Data.Common.DbConnection 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>System.Data.Common.DbConnection 类的提供程序的类的新实例。</returns>
        public DbConnection CreateDbConnection()
        {
            try
            {
                DbConnection dbConn = null;

                if (_dbFactory != null && !string.IsNullOrEmpty(ConnectionString))
                {
                    dbConn = _dbFactory.CreateConnection();
                    dbConn.ConnectionString = ConnectionString;
                }

                return dbConn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbConnection 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="connectionString">用于打开数据库的连接。</param>
        /// <returns>System.Data.Common.DbConnection 类的提供程序的类的新实例。</returns>
        public DbConnection CreateDbConnection(string connectionString)
        {
            try
            {
                DbConnection dbConn = null;

                dbConn = CreateDbConnection();
                if (dbConn != null)
                {
                    dbConn.ConnectionString = connectionString;
                }

                return dbConn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion CreateDbConnection 方法的重载

        #region CreateDbCommand 方法的重载

        /// <summary>
        /// 创建实现 System.Data.Common.DbCommand 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>System.Data.Common.DbCommand 类的提供程序的类的新实例。</returns>
        public DbCommand CreateDbCommand()
        {
            try
            {
                DbCommand dbCmd = null;

                if (_dbFactory != null)
                {
                    dbCmd = _dbFactory.CreateCommand();
                }

                return dbCmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbCommand 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="cmdText">查询的文本。</param>
        /// <param name="dbParams">语句中要使用的 DbParameter 集合。</param>
        /// <returns>System.Data.Common.DbCommand 类的提供程序的类的新实例。</returns>
        public DbCommand CreateDbCommand(string cmdText, params DbParameter[] dbParams)
        {
            try
            {
                DbCommand dbCmd = null;

                dbCmd = CreateDbCommand();
                if (dbCmd != null)
                {
                    dbCmd.CommandText = cmdText;
                    if (dbParams != null)
                    {
                        dbCmd.Parameters.AddRange(dbParams);
                    }
                }

                return dbCmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbCommand 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="cmdText">查询的文本。</param>
        /// <param name="connection">一个 System.Data.Common.DbConnection，它表示到数据库实例的连接。</param>
        /// <param name="dbParams">语句中要使用的 DbParameter 集合。</param>
        /// <returns>System.Data.Common.DbCommand 类的提供程序的类的新实例。</returns>
        public DbCommand CreateDbCommand(string cmdText, DbConnection connection, params DbParameter[] dbParams)
        {
            try
            {
                DbCommand dbCmd = null;

                dbCmd = CreateDbCommand(cmdText, dbParams);
                if (dbCmd != null)
                {
                    dbCmd.Connection = connection;
                }

                return dbCmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbCommand 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="cmdText">查询的文本。</param>
        /// <param name="cmdType">System.Data.CommandType 值之一。</param>
        /// <param name="dbParams">语句中要使用的 DbParameter 集合。</param>
        /// <returns>System.Data.Common.DbCommand 类的提供程序的类的新实例。</returns>
        public DbCommand CreateDbCommand(string cmdText, CommandType cmdType, params DbParameter[] dbParams)
        {
            try
            {
                DbCommand dbCmd = null;

                dbCmd = CreateDbCommand(cmdText, dbParams);
                if (dbCmd != null)
                {
                    dbCmd.CommandType = cmdType;
                }

                return dbCmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbCommand 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="cmdText">查询的文本。</param>
        /// <param name="cmdType">System.Data.CommandType 值之一。</param>
        /// <param name="connection">一个 System.Data.Common.DbConnection，它表示到数据库实例的连接。</param>
        /// <param name="dbParams">语句中要使用的 DbParameter 集合。</param>
        /// <returns>System.Data.Common.DbCommand 类的提供程序的类的新实例。</returns>
        public DbCommand CreateDbCommand(string cmdText, CommandType cmdType, DbConnection connection,
                                         params DbParameter[] dbParams)
        {
            try
            {
                DbCommand dbCmd = null;

                dbCmd = CreateDbCommand(cmdText, connection, dbParams);
                if (dbCmd != null)
                {
                    dbCmd.CommandType = cmdType;
                }

                return dbCmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbCommand 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="cmdText">查询的文本。</param>
        /// <param name="connection">一个 System.Data.Common.DbConnection，它表示到数据库实例的连接。</param>
        /// <param name="transaction">将在其中执行 System.Data.Common.DbCommand 的 System.Data.Common.DbTransaction。</param>
        /// <param name="dbParams">语句中要使用的 DbParameter 集合。</param>
        /// <returns>System.Data.Common.DbCommand 类的提供程序的类的新实例。</returns>
        public DbCommand CreateDbCommand(string cmdText, DbConnection connection, DbTransaction transaction,
                                         params DbParameter[] dbParams)
        {
            try
            {
                DbCommand dbCmd = null;

                dbCmd = CreateDbCommand(cmdText, connection, dbParams);
                if (dbCmd != null)
                {
                    dbCmd.Transaction = transaction;
                }

                return dbCmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbCommand 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="cmdText">查询的文本。</param>
        /// <param name="cmdType">System.Data.CommandType 值之一。</param>
        /// <param name="connection">一个 System.Data.Common.DbConnection，它表示到数据库实例的连接。</param>
        /// <param name="transaction">将在其中执行 System.Data.Common.DbCommand 的 System.Data.Common.DbTransaction。</param>
        /// <param name="dbParams">语句中要使用的 DbParameter 集合。</param>
        /// <returns>System.Data.Common.DbCommand 类的提供程序的类的新实例。</returns>
        public DbCommand CreateDbCommand(string cmdText, CommandType cmdType, DbConnection connection,
                                         DbTransaction transaction, params DbParameter[] dbParams)
        {
            try
            {
                DbCommand dbCmd = null;

                dbCmd = CreateDbCommand(cmdText, connection, transaction, dbParams);
                if (dbCmd != null)
                {
                    dbCmd.CommandType = cmdType;
                }

                return dbCmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion CreateDbCommand 方法的重载

        #region CreateDbDataAdapter 方法的重载

        /// <summary>
        /// 创建实现 System.Data.Common.DbDataAdapter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>System.Data.Common.DbDataAdapter 类的提供程序的类的新实例。</returns>
        public DbDataAdapter CreateDbDataAdapter()
        {
            try
            {
                DbDataAdapter dbAdapter = null;

                if (_dbFactory != null)
                {
                    dbAdapter = _dbFactory.CreateDataAdapter();
                }

                return dbAdapter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbDataAdapter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="selectCommand">一个 System.Data.Common.DbCommand（可以是 Transact-SQL SELECT 语句或存储过程），已设置为 System.Data.Common.DbDataAdapter 的 System.Data.Common.DbDataAdapter.SelectCommand 属性。</param>
        /// <returns>System.Data.Common.DbDataAdapter 类的提供程序的类的新实例。</returns>
        public DbDataAdapter CreateDbDataAdapter(DbCommand selectCommand)
        {
            try
            {
                DbDataAdapter dbAdapter = null;

                dbAdapter = CreateDbDataAdapter();
                if (dbAdapter != null)
                {
                    dbAdapter.SelectCommand = selectCommand;
                }

                return dbAdapter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbDataAdapter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="selectCommandText">一个 System.String，它是将要由 System.Data.Common.DbDataAdapter 的 System.Data.Common.DbDataAdapter.SelectCommand 属性使用的 Transact-SQL SELECT 语句或存储过程。</param>
        /// <param name="selectConnectionString">连接字符串。</param>
        /// <returns>System.Data.Common.DbDataAdapter 类的提供程序的类的新实例。</returns>
        public DbDataAdapter CreateDbDataAdapter(string selectCommandText, string selectConnectionString)
        {
            try
            {
                DbDataAdapter dbAdapter = null;

                dbAdapter = CreateDbDataAdapter();
                if (dbAdapter != null)
                {
                    DbConnection dbConn = CreateDbConnection(selectConnectionString);
                    if (dbConn == null)
                    {
                        return null;
                    }
                    DbCommand dbCmd = CreateDbCommand(selectCommandText, dbConn);
                    if (dbCmd == null)
                    {
                        return null;
                    }
                    dbAdapter.SelectCommand = dbCmd;
                }

                return dbAdapter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建实现 System.Data.Common.DbDataAdapter 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="selectCommandText">一个 System.String，它是将要由 System.Data.Common.DbDataAdapter 的 System.Data.Common.DbDataAdapter.SelectCommand 属性使用的 Transact-SQL SELECT 语句或存储过程。</param>
        /// <param name="selectConnection">表示该连接的 System.Data.Common.DbConnection。</param>
        /// <returns>System.Data.Common.DbDataAdapter 类的提供程序的类的新实例。</returns>
        public DbDataAdapter CreateDbDataAdapter(string selectCommandText, DbConnection selectConnection)
        {
            try
            {
                DbDataAdapter dbAdapter = null;

                dbAdapter = CreateDbDataAdapter();
                if (dbAdapter != null)
                {
                    DbCommand dbCmd = CreateDbCommand(selectCommandText, selectConnection);
                    if (dbCmd == null)
                    {
                        return null;
                    }
                    dbAdapter.SelectCommand = dbCmd;
                }

                return dbAdapter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion CreateDbDataAdapter 方法的重载

        /// <summary>
        /// 执行没有返回集合的 Transact-SQL 语句或存储过程。
        /// </summary>
        /// <param name="dbCmd">使用的DbCommand。</param>
        /// <returns>所影响的行数。</returns>
        public int ExecuteNonQuery(DbCommand dbCmd)
        {
            try
            {
                if (dbCmd == null)
                {
                    return 0;
                }
                int rowNum = 0;
                using (DbConnection dbConn = CreateDbConnection())
                {
                    if (dbConn == null)
                    {
                        return 0;
                    }
                    dbCmd.Connection = dbConn;
                    dbConn.Open();
                    rowNum = dbCmd.ExecuteNonQuery();
                    dbCmd.Dispose();
                }

                return rowNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回实现 System.Data.Common.DbDataReader 类的提供程序的类的一个新实例。
        /// </summary>
        /// <param name="dbCmd">使用的DbCommand。</param>
        /// <returns>System.Data.Common.DbDataReader 类的提供程序的类的新实例。</returns>
        public DbDataReader ExecuteReader(DbCommand dbCmd)
        {
            try
            {
                DbDataReader dbReader = null;

                if (dbCmd == null)
                {
                    return null;
                }
                DbConnection dbConn = CreateDbConnection();
                if (dbConn == null)
                {
                    return null;
                }
                dbCmd.Connection = dbConn;
                dbConn.Open();
                dbReader = dbCmd.ExecuteReader(CommandBehavior.CloseConnection);

                return dbReader;
            }
            catch (Exception ex)
            {
                if (dbCmd != null && dbCmd.Connection != null)
                {
                    dbCmd.Connection.Close();
                }
                throw ex;
            }
        }

        /// <summary>
        /// 返回第一行第一列的值。
        /// </summary>
        /// <param name="dbCmd">使用的DbCommand。</param>
        /// <returns>包含数据的object对象。</returns>
        public object ExecuteScalar(DbCommand dbCmd)
        {
            if (dbCmd == null)
            {
                return null;
            }
            object value = null;
            try
            {
                using (DbConnection dbConn = CreateDbConnection())
                {
                    if (dbConn == null)
                    {
                        return null;
                    }

                    dbCmd.Connection = dbConn;
                    dbConn.Open();
                    value = dbCmd.ExecuteScalar();
                    dbCmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        /// <summary>
        /// 返回一个数据集
        /// </summary>
        /// <param name="dbCmd">使用的DbCommand。</param>
        /// <returns>System.Data 类的提供程序的类的新实例</returns>
        public DataSet ExecuteDataSet(DbCommand dbCmd)
        {
            try
            {
                if (dbCmd == null)
                {
                    return null;
                }
                var set = new DataSet();
                using (DbConnection dbConn = CreateDbConnection())
                {
                    if (dbConn == null)
                    {
                        return null;
                    }
                    dbCmd.Connection = dbConn;
                    dbConn.Open();

                    using (DbDataAdapter adapter = CreateDbDataAdapter(dbCmd))
                    {
                        adapter.Fill(set);
                    }
                    dbCmd.Dispose();
                }

                return set;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}