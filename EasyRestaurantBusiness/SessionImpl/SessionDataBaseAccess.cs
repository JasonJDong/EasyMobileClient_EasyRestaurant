using System;
using DMobile.Biz.DataAccess.Manager;
using DMobile.Server.Session.Entity;
using DMobile.Server.Session.Interface;

namespace EasyRestaurantBusiness.SessionImpl
{
    public class SessionDataBaseAccess : ISessionEntry
    {
        #region Implementation of ISessionCommunication

        /// <summary>
        /// 用于判断使用保存Session的数据体的顺序
        /// </summary>
        public int UsePriority { get; set; }

        public bool NeedSynchronize { get; private set; }

        public bool FindExists(SessionBase session)
        {
            bool result = false;
            DataAccessManager.ExecuteCommand("IsSessionExists", cmd =>
                {
                    var exists = cmd.ExecuteScalar<String>(new {Session = session.SessionText});
                    bool.TryParse(exists, out result);
                });
            return result;
        }

        public bool InsertOne(SessionBase session)
        {
            throw new NotSupportedException();
        }

        #endregion

        public SessionDataBaseAccess()
        {
            NeedSynchronize = false;
            UsePriority = 0;
        }
    }
}
