using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using DMobile.Server.Common.Entity;
using EasyRestaurantBusiness.Data.Attributes;
using EasyRestaurantBusiness.Data.ErrorImpl;

namespace EasyRestaurantBusiness.Data.Base
{
    [KnownType(typeof(BusinessEntityErrorHandle))]
    public abstract class AutoAssignValueDataBase
    {
        private static readonly Dictionary<Type, Dictionary<string, string>> TypeParameterMappingCache = new Dictionary<Type, Dictionary<string, string>>();

        private readonly List<String> CurrentTableSchemaColumnsName = new List<string>();

        private static readonly Type ConvertibleType = typeof (IConvertible);
        private static readonly Type EnumType = typeof(Enum);
        private static readonly Type StringType = typeof(string);


        [NotColumn]
        internal BusinessEntityErrorHandle Error { get; set; }

        protected AutoAssignValueDataBase()
        {
            Error = new BusinessEntityErrorHandle();
        }

        public void Build(DataRow row)
        {
            BuildHandle((colName, property) =>
                {
                    Type propertyType = property.PropertyType;
                    String exists = String.Empty;
                    foreach (DataColumn column in row.Table.Columns)
                    {
                        if (string.Equals(colName, column.ColumnName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            exists = column.ColumnName;
                            break;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(exists))
                    {
                        return false;
                    }
                    try
                    {
                        var value = ChangeType(row[exists], propertyType);
                        property.SetValue(this, value, null);
                    }
                    catch (Exception ex)
                    {
                        Error.ErrorCode = ErrorMapping.BUSINESS_FFFF;
                        Error.SimpleErrorDescription = ex.Message;
                        return false;
                    }
                    return true;
                });
        }

        public void Build(DbDataReader reader)
        {
            var tableSchema = reader.GetSchemaTable();
            if (tableSchema == null)
            {
                return;
            }
            FetchTableColumnsName(tableSchema);
            //reader.Read();
            BuildHandle((colName, property) =>
                {
                    var propertyType = property.PropertyType;
                    var exists = CurrentTableSchemaColumnsName.Find(s => String.Equals(s, colName, StringComparison.InvariantCultureIgnoreCase));
                    if (String.IsNullOrWhiteSpace(exists))
                    {
                        return false;
                    }
                    var index = reader.GetOrdinal(exists);
                    try
                    {
                        if (reader.IsDBNull(index))
                        {
                            return false;
                        }
                        var value = ChangeType(reader.GetValue(index), propertyType);
                        property.SetValue(this, value, null);
                    }
                    catch (Exception ex)
                    {
                        Error.ErrorCode = ErrorMapping.BUSINESS_FFFF;
                        Error.SimpleErrorDescription = ex.Message;
                        return false;
                    }
                    return true;
                });
        }


        private void BuildHandle(Func<string, PropertyInfo, bool> handle)
        {
            var type = GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                #region 缓存找到列名

                var colName = GetTypeParameterColumnName(type, property.Name);
                if (!string.IsNullOrWhiteSpace(colName))
                {
                    handle(colName, property);
                    continue;
                }

                #endregion

                #region 缓存未找到列名

                var attrs = property.GetCustomAttributes(true);
                var notCol = attrs.FirstOrDefault(a => a is NotColumnAttribute) as NotColumnAttribute;
                if (notCol == null)
                {
                    colName = property.Name;
                    var colDes = attrs.FirstOrDefault(a => a is ColumnDescriptorAttribute) as ColumnDescriptorAttribute;
                    if (colDes != null)
                    {
                        colName = colDes.ColumnName;
                    }
                    if (handle(colName, property))
                    {
                        AddTypeParameterCache(type, property.Name, colName);
                    }
                }

                #endregion

            }
        }

        private object ChangeType(object value, Type conversionType)
        {
            if (value == null)
                return null;

            var valueType = value.GetType();
            if (valueType == conversionType)
            {
                return value;
            }

            if (conversionType == StringType)
                return value.ToString();

            try
            {
                if (ConvertibleType.IsAssignableFrom(valueType))
                {
                    if (conversionType.BaseType == EnumType)
                    {
                        return Enum.Parse(conversionType, value.ToString());
                    }
                    return Convert.ChangeType(value, conversionType);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private void AddTypeParameterCache(Type type, string param, string colName)
        {
            Dictionary<string, string> cache;
            if (!TypeParameterMappingCache.TryGetValue(type, out cache))
            {
                var paramMapping = new Dictionary<string, string> { { param, colName } };
                TypeParameterMappingCache.Add(type, paramMapping);
                return;
            }
            cache.Add(param, colName);
        }

        private string GetTypeParameterColumnName(Type type, string param)
        {

            Dictionary<string, string> cache;
            if (!TypeParameterMappingCache.TryGetValue(type, out cache))
            {
                return string.Empty;
            }
            string columName;
            if (cache.TryGetValue(param, out columName))
            {
                return columName;
            }
            return string.Empty;
        }

        private void FetchTableColumnsName(DataTable schemaTable)
        {
            foreach (DataRow row in schemaTable.Rows)
            {
                CurrentTableSchemaColumnsName.Add(row["ColumnName"].ToString());
            }
        }
    }
}
