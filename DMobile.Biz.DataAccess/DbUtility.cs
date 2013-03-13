using System;
using System.Collections;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DMobile.Biz.DataAccess
{
    /// <summary>
    /// 数据访问器。
    /// </summary>
    public abstract partial class DbUtility
    {
        #region 静态变量

        protected static readonly SortedList CacheParamList;

        #endregion 静态变量

        #region 私有变量

        private DbProviderFactory _dbFactory;

        #endregion 私有变量

        #region 属性读写

        private string _providerName;

        /// <summary>
        /// 获取或设置数据提供者的命名空间的名称。
        /// </summary>
        public string ProviderName
        {
            get { return _providerName; }
            set
            {
                _providerName = value;
                switch (_providerName)
                {
                    case "MySql.Data.MySqlClient":
                        _dbFactory = new MySqlClientFactory();
                        break;
                    default:
                        _dbFactory = DbProviderFactories.GetFactory(_providerName);
                        break;
                }
            }
        }

        /// <summary>
        /// 获取或设置连接字符串。
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 获取连接用户名。
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 获取连接密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取连接数据源。
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 获取连接服务器。
        /// </summary>
        public string DataServer { get; set; }

        /// <summary>
        /// 获取或设置数据提供者。
        /// </summary>
        public string Provider { get; set; }

        #endregion 属性读写

        #region 构造函数

        /// <summary>
        /// 静态构造函数。
        /// </summary>
        static DbUtility()
        {
            CacheParamList = SortedList.Synchronized(new SortedList());
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="providerName">connectionStrings 的名称。</param>
        protected DbUtility(string providerName)
        {
            ProviderName = providerName;
        }

        #endregion 构造函数

        #region 公共方法

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <returns></returns>
        public abstract void Initialize();


        /// <summary>
        /// 将参数数组添加到缓存。
        /// </summary>
        /// <param name="cacheKey">参数缓存的Key值。</param>
        /// <param name="cacheParams">要缓存的参数数组。</param>
        public void CacheParameters(string cacheKey, params DbParameter[] cacheParams)
        {
            try
            {
                CacheParamList[cacheKey] = cacheParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将参数数组添加到缓存。
        /// </summary>
        /// <param name="cacheKey">参数缓存的Key值。</param>
        /// <param name="cacheParams">要缓存的参数数组。</param>
        public void CacheParameters<TParam>(string cacheKey, params TParam[] cacheParams)
        {
            try
            {
                var dbParams = new DbParameter[cacheParams.Length];
                for (int i = 0; i < cacheParams.Length; i++)
                {
                    dbParams[i] = cacheParams[i] as DbParameter;
                }
                CacheParamList[cacheKey] = dbParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取缓存中的参数数组。
        /// </summary>
        /// <param name="cacheKey">参数缓存的Key值。</param>
        /// <returns>缓存中的参数数组。</returns>
        public DbParameter[] GetCachedParameters(string cacheKey)
        {
            try
            {
                var cacheParams = (DbParameter[]) CacheParamList[cacheKey];
                if (cacheParams == null)
                {
                    return null;
                }

                var cloneParams = new DbParameter[cacheParams.Length];
                for (int i = 0; i < cacheParams.Length; i++)
                {
                    cloneParams[i] = ((ICloneable) cacheParams[i]).Clone() as DbParameter;
                }

                return cloneParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取缓存中的参数数组。
        /// </summary>
        /// <param name="cacheKey">参数缓存的Key值。</param>
        /// <returns>缓存中的参数数组。</returns>
        public TParam[] GetCachedParameters<TParam>(string cacheKey)
        {
            try
            {
                var dbParams = (DbParameter[]) CacheParamList[cacheKey];
                if (dbParams == null)
                {
                    return null;
                }

                var cloneParams = new TParam[dbParams.Length];
                for (int i = 0; i < dbParams.Length; i++)
                {
                    cloneParams[i] = (TParam) ((ICloneable) dbParams[i]).Clone();
                }

                return cloneParams;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion 公共方法
    }
}