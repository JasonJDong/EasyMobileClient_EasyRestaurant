using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace DMobile.Biz.DataAccess.Manager.DataEntity
{
    public class DataCommand
    {
        private static readonly Dictionary<Type, PropertyInfo[]> CacheProperties =
            new Dictionary<Type, PropertyInfo[]>();

        #region Field Define

        private string m_CommandText = String.Empty;
        protected string m_DatabaseName = String.Empty;
        protected DbCommand m_DbCommand = null;

        #endregion Field Define

        #region constructor

        internal DataCommand(string databaseName, DbCommand command)
        {
            m_DatabaseName = databaseName;
            m_DbCommand = command;
        }

        private DataCommand()
        {
        }

        #endregion constructor

        #region Attribute

        public string CommandText
        {
            get
            {
                m_CommandText = m_DbCommand.CommandText;
                return m_CommandText;
            }
            set { m_DbCommand.CommandText = value; }
        }

        protected DbUtility ActualDatabase
        {
            get { return DatabaseDetector.GetDatabase(m_DatabaseName); }
        }

        #endregion Attribute

        #region Public Function

        public DataCommand Clone()
        {
            var cmd = new DataCommand();

            if (m_DbCommand != null)
            {
                if (m_DbCommand is ICloneable)
                {
                    cmd.m_DbCommand = ((ICloneable) m_DbCommand).Clone() as DbCommand;
                }
                else
                {
                    throw new ApplicationException("A class that implements IClonable is expected.");
                }
            }

            cmd.m_DatabaseName = m_DatabaseName;

            return cmd;
        }

        public Object GetParameterValue(string paramName)
        {
            return m_DbCommand.Parameters[paramName].Value;
        }

        public void SetParameterValue(string paramName, Object val)
        {
            m_DbCommand.Parameters[paramName].Value = val;
        }

        public void AddParameter(IDbDataParameter parame)
        {
            m_DbCommand.Parameters.Add(parame);
        }

        public void ReplaceParameterValue(string paramName, string paramValue)
        {
            if (null != m_DbCommand) m_DbCommand.CommandText = m_DbCommand.CommandText.Replace(paramName, paramValue);
        }

        public T ExecuteScalar<T>()
        {
            try
            {
                return (T) ActualDatabase.ExecuteScalar(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T ExecuteScalar<T>(Object entity)
        {
            try
            {
                SetParameterValue(entity);
                return (T) ActualDatabase.ExecuteScalar(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ExecuteScalar()
        {
            try
            {
                return ActualDatabase.ExecuteScalar(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ExecuteScalar(Object entity)
        {
            try
            {
                SetParameterValue(entity);
                return ActualDatabase.ExecuteScalar(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteNonQuery()
        {
            try
            {
                return ActualDatabase.ExecuteNonQuery(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteNonQuery(Object entity)
        {
            try
            {
                SetParameterValue(entity);
                return ActualDatabase.ExecuteNonQuery(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DbDataReader ExecuteDataReader()
        {
            try
            {
                return ActualDatabase.ExecuteReader(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DbDataReader ExecuteDataReader(Object entity)
        {
            try
            {
                SetParameterValue(entity);
                return ActualDatabase.ExecuteReader(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteDataSet()
        {
            try
            {
                return ActualDatabase.ExecuteDataSet(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteDataSet(Object entity)
        {
            try
            {
                SetParameterValue(entity);
                return ActualDatabase.ExecuteDataSet(m_DbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public Function

        #region Protected Funciton

        protected void SetParameterValue(Object val)
        {
            //TODO:未来版本应解决性能问题
            if (val == null)
            {
                return;
            }
            const string paramFormat = "@{0}";
            Type type = val.GetType();

            PropertyInfo[] properties;

            if (!CacheProperties.TryGetValue(type, out properties))
            {
                properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                CacheProperties.Add(type, properties);
            }

            foreach (PropertyInfo property in properties)
            {
                try
                {
                    string param = string.Format(paramFormat, property.Name);
                    object specVal = property.GetValue(val, null);
                    if (m_DbCommand.Parameters.Contains(param))
                    {
                        m_DbCommand.Parameters[param].Value = specVal ?? DBNull.Value;
                        m_DbCommand.Parameters[param].DbType = SetTypeFromValue(specVal);
                    }
                    else
                    {
                        if (!property.PropertyType.IsValueType && property.PropertyType != typeof (string))
                        {
                            SetParameterValue(specVal);
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        protected DbType SetTypeFromValue(Object paramValue)
        {
            var dbType = DbType.Object;
            if (paramValue == null || paramValue == DBNull.Value)
                return dbType;
            if (paramValue is Guid)
                dbType = DbType.Guid;
            else if (paramValue is TimeSpan)
                dbType = DbType.Time;
            else if (paramValue is bool)
            {
                dbType = DbType.Byte;
            }
            else
            {
                switch (Type.GetTypeCode(paramValue.GetType()))
                {
                    case TypeCode.SByte:
                        dbType = DbType.SByte;
                        break;
                    case TypeCode.Byte:
                        dbType = DbType.Byte;
                        break;
                    case TypeCode.Int16:
                        dbType = DbType.Int16;
                        break;
                    case TypeCode.UInt16:
                        dbType = DbType.UInt16;
                        break;
                    case TypeCode.Int32:
                        dbType = DbType.Int32;
                        break;
                    case TypeCode.UInt32:
                        dbType = DbType.UInt32;
                        break;
                    case TypeCode.Int64:
                        dbType = DbType.Int64;
                        break;
                    case TypeCode.UInt64:
                        dbType = DbType.UInt64;
                        break;
                    case TypeCode.Single:
                        dbType = DbType.Single;
                        break;
                    case TypeCode.Double:
                        dbType = DbType.Double;
                        break;
                    case TypeCode.Decimal:
                        dbType = DbType.Decimal;
                        break;
                    case TypeCode.DateTime:
                        dbType = DbType.DateTime;
                        break;
                    case TypeCode.String:
                        dbType = DbType.String;
                        break;
                    default:
                        dbType = DbType.Object;
                        break;
                }
            }
            return dbType;
        }

        #endregion
    }
}