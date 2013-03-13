using System;
using System.Data;

namespace DMobile.Biz.DataAccess.Manager.DataEntity
{
    public class Identity : IEquatable<Identity>
    {
        /// <summary>
        /// The command type 
        /// </summary>
        public readonly CommandType? commandType;

        /// <summary>
        /// 
        /// </summary>
        public readonly string connectionString;

        /// <summary>
        /// 
        /// </summary>
        public readonly int gridIndex;

        /// <summary>
        /// 
        /// </summary>
        public readonly int hashCode;

        /// <summary>
        /// 
        /// </summary>
        public readonly Type parametersType;

        /// <summary>
        /// The sql
        /// </summary>
        public readonly string sql;

        /// <summary>
        /// 
        /// </summary>
        public readonly Type type;

        internal Identity(string sql, CommandType? commandType, IDbConnection connection, Type type, Type parametersType,
                          Type[] otherTypes)
            : this(sql, commandType, connection.ConnectionString, type, parametersType, otherTypes, 0)
        {
        }

        private Identity(string sql, CommandType? commandType, string connectionString, Type type, Type parametersType,
                         Type[] otherTypes, int gridIndex)
        {
            this.sql = sql;
            this.commandType = commandType;
            this.connectionString = connectionString;
            this.type = type;
            this.parametersType = parametersType;
            this.gridIndex = gridIndex;
            unchecked
            {
                hashCode = 17; // we *know* we are using this in a dictionary, so pre-compute this
                hashCode = hashCode*23 + commandType.GetHashCode();
                hashCode = hashCode*23 + gridIndex.GetHashCode();
                hashCode = hashCode*23 + (sql == null ? 0 : sql.GetHashCode());
                hashCode = hashCode*23 + (type == null ? 0 : type.GetHashCode());
                if (otherTypes != null)
                {
                    foreach (Type t in otherTypes)
                    {
                        hashCode = hashCode*23 + (t == null ? 0 : t.GetHashCode());
                    }
                }
                hashCode = hashCode*23 + (connectionString == null ? 0 : connectionString.GetHashCode());
                hashCode = hashCode*23 + (parametersType == null ? 0 : parametersType.GetHashCode());
            }
        }

        #region IEquatable<Identity> Members

        /// <summary>
        /// Compare 2 Identity objects
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Identity other)
        {
            return
                other != null &&
                gridIndex == other.gridIndex &&
                type == other.type &&
                sql == other.sql &&
                commandType == other.commandType &&
                connectionString == other.connectionString &&
                parametersType == other.parametersType;
        }

        #endregion

        /// <summary>
        /// Create an identity for use with DynamicParameters, internal use only
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Identity ForDynamicParameters(Type type)
        {
            return new Identity(sql, commandType, connectionString, this.type, type, null, -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Identity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}