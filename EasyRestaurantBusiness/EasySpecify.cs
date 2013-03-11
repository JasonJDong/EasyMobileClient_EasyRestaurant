using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Interface;
using Business.Session;
using Common.DataAccess.IDAL;
using Common.DataAccess.DALFactory;
using System.Data;
using System.Data.Common;
using Utilities;

namespace Business
{
    public class EasySpecify : IBusiness
    {
        private readonly IComDataVisitor ComDataVisitor;
        #region Implementation of ILoginEntry

        public string LoginForSession(string uid, string pwd)
        {
            try
            {
                string simpleSql = "SELECT SESSION FROM UserBaseInformation WHERE USERNAME=@UID AND PASSWORD=@PWD";
                var pName = CreateParameter("@UID", uid);
                var pPwd = CreateParameter("@PWD", pwd);

                ComDataVisitor.ComCommand.Parameters.AddRange(new[] { pName, pPwd });
                if (!ComDataVisitor.OpenConnection())
                {
                    throw new Exception("DataBase can not access.");
                }
                ComDataVisitor.ComConnection.ChangeDatabase("EasySpecify");
                var Obj = ComDataVisitor.GetFirstValue(simpleSql);
                if (Obj != null)
                {
                    return Obj.ToString();
                }
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                ComDataVisitor.ComCommand.Parameters.Clear();
                ComDataVisitor.CloseConnection();
            }
            return "";
        }

        public string CreateUser(string uid, string pwd)
        {
            try
            {
                string simpleSql = "INSERT INTO UserBaseInformation(IDX,USERNAME,PASSWORD,SESSION) VALUES(@IDX,@USERNAME,@PWD,@SESSION)";
                var idx = Guid.NewGuid().ToString().Replace("-", "");
                var session = Guid.NewGuid().ToString().Replace("-", "");

                var pIdx = CreateParameter("@IDX", idx);
                var pName = CreateParameter("@USERNAME", uid);
                var pPwd = CreateParameter("@PWD", pwd);
                var pSession = CreateParameter("@SESSION", session);

                ComDataVisitor.ComCommand.Parameters.AddRange(new[] { pIdx, pName, pPwd, pSession });
                if (!ComDataVisitor.OpenConnection())
                {
                    throw new Exception("DataBase can not access.");
                }
                ComDataVisitor.ComConnection.ChangeDatabase("EasySpecify");
                var execute = ComDataVisitor.ExecuteSQL(simpleSql);
                if (execute == 1)
                {
                    return session;
                }
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                ComDataVisitor.ComCommand.Parameters.Clear();
                ComDataVisitor.CloseConnection();
            }
            return "";
        }

        public bool UpdateUser(string uid, string pwd)
        {
            return true;
        }

        public bool DeleteUser(string uid)
        {
            return true;
        }

        public bool IsUserExists(string uid)
        {
            return true;
        }

        public bool CheckSession(string session)
        {
            return true;
        }

        #endregion

        #region Implementation of ISessionMethod

        public bool FindExists(string session)
        {
            return true;
        }

        public bool InsertOne(string session)
        {
            return true;
        }

        public List<ISessionEntry> GetSessionEntries()
        {
            var entries = new List<ISessionEntry> { new SessionCache(), new SessionDataBaseAccess { SessionMethod = this } };
            return entries;
        }

        #endregion

        public EasySpecify()
        {
            ComDataVisitor = VisitorFactory.CreateComDataVisitor();
            if (ComDataVisitor == null || !ComDataVisitor.Initialize())
            {
                throw new Exception("业务实体创建失败");
            }
        }

        private DbParameter CreateParameter(string name, object value, DbType type = DbType.String, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = ComDataVisitor.ComCommand.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = type;
            parameter.Value = value;
            parameter.Direction = direction;
            return parameter;
        }
    }
}
