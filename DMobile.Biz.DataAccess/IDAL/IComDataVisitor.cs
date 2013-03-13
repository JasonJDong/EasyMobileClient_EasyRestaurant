using System;
using System.Data.Common;

namespace DMobile.Biz.DataAccess.IDAL
{
    /// <summary>
    /// 数据访问抽象接口
    /// </summary>
    public interface IComDataVisitor : IDisposable
    {
        #region 属性声明

        /// <summary>
        /// 获取 DbConnection 对象。
        /// </summary>
        DbConnection ComConnection { get; }

        /// <summary>
        /// 获取 DbCommand 对象。
        /// </summary>
        DbCommand ComCommand { get; }

        /// <summary>
        /// 获取 DbDataAdapter 对象。
        /// </summary>
        DbDataAdapter ComDataAdapter { get; }

        /// <summary>
        /// 获取系统日期。
        /// </summary>
        DateTime SysDate { get; }

        #endregion 属性声明

        #region 方法声明

        /// <summary>
        /// 初始化的方法。
        /// </summary>
        /// <returns>true: 成功  false: 失败</returns>
        bool Initialize();

        /// <summary>
        /// 打开连接的方法。
        /// </summary>
        /// <returns>true: 成功  false: 失败</returns>
        bool OpenConnection();

        /// <summary>
        /// 关闭连接的方法。
        /// </summary>
        void CloseConnection();

        #endregion 方法声明
    }
}