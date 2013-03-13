using System.Text;

namespace DMobile.Biz.DataAccess.MySqlDAL
{
    /// <summary>
    /// SqlServer数据库的访问器类。
    /// </summary>
    internal class ComDataVisitor : DbUtility
    {
        #region 构造函数

        /// <summary>
        /// 初始化 Custom.DataAccess.SqlServerDAL.ComDataVisitor 类的新实例。
        /// </summary>
        public ComDataVisitor()
            : base("MySql.Data.MySqlClient")
        {
        }

        #endregion 构造函数

        /// <summary>
        /// 初始化的方法。
        /// </summary>
        /// <returns>true: 成功  false: 失败</returns>
        public override void Initialize()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                var dbConnStr = new StringBuilder();
                dbConnStr.Append("Persist Security Info=True;");
                dbConnStr.Append("Pooling=True;");
                if (!string.IsNullOrEmpty(DataServer))
                {
                    dbConnStr.AppendFormat("Server={0};", DataServer);
                }
                dbConnStr.AppendFormat("Database={0};", DataSource);
                dbConnStr.AppendFormat("User ID={0};", UserID);
                dbConnStr.AppendFormat("Password={0};", Password);
                ConnectionString = dbConnStr.ToString();
            }
        }
    }
}