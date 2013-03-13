using System;
using System.Reflection;

namespace DMobile.Biz.DataAccess.DALFactory
{
    /// <summary>
    /// 数据访问器的工厂类。
    /// </summary>
    public static class VisitorFactory
    {
        /// <summary>
        /// 创建 Custom.DataAccess.DbUtility 的新实例。
        /// </summary>
        /// <returns>返回 Custom.DataAccess.DbUtility 的新实例</returns>
        public static DbUtility CreateComDataVisitor(string provider)
        {
            DbUtility returnValue = null;

            try
            {
                string className = string.Format("DMobile.Biz.DataAccess.{0}DAL.ComDataVisitor", provider);
                Type classType = Type.GetType(className);
                ConstructorInfo constructorInf = classType.GetConstructor(Type.EmptyTypes);
                returnValue = (DbUtility) constructorInf.Invoke(null);
            }
            catch
            {
                throw new Exception("创建数据库访问器失败");
            }

            return returnValue;
        }
    }
}