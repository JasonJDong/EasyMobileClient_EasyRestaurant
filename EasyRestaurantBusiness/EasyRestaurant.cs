using System;
using System.Collections.Generic;
using System.Text;
using DMobile.Biz.DataAccess.Manager;
using DMobile.Biz.Interface;
using DMobile.Server.Session;
using DMobile.Server.Session.Entity;
using DMobile.Server.Session.Interface;
using DMobile.Server.Utilities;
using EasyRestaurantBusiness.Data;
using EasyRestaurantBusiness.Data.Base;
using EasyRestaurantBusiness.SessionImpl;

namespace EasyRestaurantBusiness
{
    public partial class EasyRestaurant : IBusiness
    {
        #region Implementation of ILoginEntry

        public IUserBase LoginForSession(string uid, string pwd)
        {
            var encryptPwd = CommonUtilities.Encrypt(pwd);
            var user = new ERUserEntity { UserID = uid, Password = encryptPwd };
            DataAccessManager.ExecuteCommand("GetUserByIDAndPassword", cmd =>
            {
                using (var reader = cmd.ExecuteDataReader(user))
                {
                    reader.Read();
                    user.Build(reader);
                }
            });
            return user;
        }

        public string CreateUser(string uid, string pwd)
        {
            var session = SessionBuilder.BuildSession(uid, uid, TimeSpan.FromDays(10));
            var execute = false;
            var encryptPwd = CommonUtilities.Encrypt(pwd);
            DataAccessManager.ExecuteCommand("CreateUser", cmd =>
            {
                dynamic parameters =
                    new { UserID = uid, Password = encryptPwd, Session = session.SessionText };
                execute = cmd.ExecuteNonQuery(parameters) != 0;
            });

            return execute ? session.SessionText : String.Empty;
        }

        public bool UpdateUser(IUserBase userEntity)
        {
            var fullUser = userEntity as ERUserEntity;
            var execute = false;
            if (fullUser != null)
            {
                DataAccessManager.ExecuteCommand("UpdateUser", cmd =>
                {
                    execute = cmd.ExecuteNonQuery(userEntity) != 0;
                });
            }
            return execute;
        }

        public bool DeleteUser(string uid)
        {
            var execute = false;
            DataAccessManager.ExecuteCommand("DeleteUser", cmd =>
            {
                execute = cmd.ExecuteNonQuery(new { UserID = uid }) != 0;
            });
            return execute;
        }

        public bool IsUserExists(string uid)
        {
            var reslut = false;
            DataAccessManager.ExecuteCommand("IsUserExists", cmd =>
            {
                reslut = Boolean.TryParse(cmd.ExecuteScalar(new { UserID = uid }).ToString(), out reslut) & reslut;
            });
            return reslut;
        }

        public bool CheckSession(string uid, string session)
        {
            var reslut = false;
            DataAccessManager.ExecuteCommand("CheckSession", cmd =>
            {
                reslut = Boolean.TryParse(cmd.ExecuteScalar(new { UserID = uid, Session = session }).ToString(), out reslut) & reslut;
            });
            return reslut;
        }

        #endregion

        #region Implementation of ISessionMethod

        public bool SessionAccess(SessionBase session)
        {
            SessionManager.Instance.Session = session;
            SessionManager.Instance.ValidateSession();
            return SessionManager.Instance.IsPassed;
        }

        #endregion

        #region 用户方法
        /// <summary>
        /// 获取用户位置
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public ERUserLocationEntity GetUserLocation(string userIdx)
        {
            var locations = GetUsersLocation(new List<string> { userIdx });
            return locations.Count == 0 ? null : locations[0];
        }

        /// <summary>
        /// 获取多个用户位置
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public List<ERUserLocationEntity> GetUsersLocation(List<string> userIds)
        {
            var usersLocation = new List<ERUserLocationEntity>();
            var sb = new StringBuilder();
            foreach (string id in userIds)
            {
                sb.AppendFormat("'{0}',", id);
            }
            DataAccessManager.ExecuteCommand("GetUsersLocation", cmd =>
            {
                cmd.CommandText = cmd.CommandText.Replace("#IDs#", sb.ToString().TrimEnd(','));
                using (var reader = cmd.ExecuteDataReader())
                {
                    while (reader.Read())
                    {
                        var userLocation = new ERUserLocationEntity();
                        userLocation.Build(reader);
                        usersLocation.Add(userLocation);
                    }
                }
            });
            return usersLocation;
        }

        /// <summary>
        /// 获取用户菜肴权重
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public string GetUserCookPriority(string userIdx)
        {
            string priorityJson = string.Empty;
            DataAccessManager.ExecuteCommand("GetUserCookPriority", cmd => priorityJson = cmd.ExecuteScalar<string>(new { UserID = userIdx }));
            return priorityJson;
        }

        public string OperationUserLocation(ERUserLocationEntity location, string operationType)
        {
            string returnValue = string.Empty;
            DataAccessManager.ExecuteCommand("OperationUserLocation", cmd =>
            {
                dynamic o = new { Entity = location, OpType = operationType };
                returnValue = cmd.ExecuteNonQuery(o) != 0 ? location.UserID : string.Empty;
            });
            return returnValue;
        }

        public string OperationUserPoints(ERUserEntity entity, string operationType)
        {
            var success = OperationInvoke("OperationUserPoints", entity, operationType);
            return success ? entity.UserID : string.Empty;
        }

        public string OperationUserPriority(ERUserCookBooksPriority entity, string operationType)
        {
            var success = OperationInvoke("OperationUserPriority", entity, operationType);
            return success ? entity.UserID : string.Empty;
        }

        public string OperationUserConfig(ERUserConfig entity, string operationType)
        {
            var success = OperationInvoke("OperationUserConfig", entity, operationType);
            return success ? entity.UserID : string.Empty;
        }
        #endregion

        #region 队伍方法

        /// <summary>
        /// 增删改:队伍
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public string OperationTeam(ERTeamDetailEntity entity, string operationType)
        {
            if (string.Equals(operationType, "2"))
            {
                entity.TeamID = UniqueIDProvider.GetUniqueID();
            }
            var success = OperationInvoke("OperationTeamDetail", entity, operationType);

            DataAccessManager.ExecuteCommand("IsTeamsExists", cmd =>
            {
                var exists = cmd.ExecuteScalar<String>(new { TeamID = entity.TeamID });
                bool.TryParse(exists, out success);
            });
            return success ? entity.TeamID : string.Empty;
        }

        /// <summary>
        /// 模糊查询：通过队伍名称获取队伍
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public List<ERTeamDetailEntity> GetTeamsByTeamName(string teamName)
        {
            return GetEntityList<ERTeamDetailEntity>("GetTeamsByTeamName", new { TeamName = teamName });
        }

        /// <summary>
        /// 获取队伍操作信息
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ERTeamOperationEntity GetTeamOperation(string teamId, string userId)
        {
            ERTeamOperationEntity teamOp = null;

            DataAccessManager.ExecuteCommand("GetTeamOperation", cmd =>
            {
                using (var reader = cmd.ExecuteDataReader(new { TeamID = teamId, UserID = userId }))
                {
                    while (reader.Read())
                    {
                        teamOp = new ERTeamOperationEntity();
                        teamOp.Build(reader);
                    }
                }
            });
            return teamOp;
        }

        public string OperationTeamOperation(ERTeamOperationEntity entity, string operationType)
        {
            var success = OperationInvoke("OperationTeamOperation", entity, operationType);
            return success ? entity.TeamID : string.Empty;
        }

        public string OperationTeamAdvanced(ERTeamDetailEntity entity, string operationType)
        {
            var success = OperationInvoke("OperationTeamAdvanced", entity, operationType);
            return success ? entity.TeamID : string.Empty;
        }
        #endregion

        #region 餐厅方法
        /// <summary>
        /// 获取餐厅评价
        /// </summary>
        /// <param name="resId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ERRestaurantComment> GetRestaurantComments(string resId, string userId)
        {
            return GetEntityList<ERRestaurantComment>("GetRestaurantComments", new { RestrauntID = resId, USerID = userId });
        }

        /// <summary>
        /// 获取餐厅菜谱
        /// </summary>
        /// <param name="resId"></param>
        /// <returns></returns>
        public List<ERRestaurantCookEntity> GetRestaurantCooks(string resId)
        {
            return GetEntityList<ERRestaurantCookEntity>("GetRestaurantCooks", new { RestrauntID = resId });
        }

        public string OperationRestaurantBase(ERRestaurantEntity entity, string operationType)
        {
            if (string.Equals(operationType, "2"))
            {
                entity.RestaurantID = UniqueIDProvider.GetUniqueID();
            }
            var success = OperationInvoke("OperationRestaurantBase", entity, operationType);
            return success ? entity.RestaurantID : string.Empty;
        }

        /// <summary>
        /// 增删改:餐厅菜肴
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public string OperationRestaurantCook(ERRestaurantCookEntity entity, string operationType)
        {
            var success = OperationInvoke("OperationRestaurantCook", entity, operationType);
            return success ? entity.RestaurantID : string.Empty;
        }

        public string OperationRestaurantComment(ERRestaurantComment entity, string operationType)
        {
            var success = OperationInvoke("OperationRestaurantComment", entity, operationType);
            return success ? entity.RestaurantID : string.Empty;
        }
        #endregion

        #region 好友方法
        /// <summary>
        /// 获取所有好友
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<ERFriendEntity> GetAllFriends(string userId)
        {
            return GetEntityList<ERFriendEntity>("GetAllFriends", new { UserID = userId });
        }

        /// <summary>
        /// 增删改:好友
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public string OperationFriend(ERFriendEntity entity, string operationType)
        {
            var success = OperationInvoke("OperationFriend", entity, operationType);
            return success ? entity.FriendId : string.Empty;
        }

        /// <summary>
        /// 模糊查询好友
        /// </summary>
        /// <param name="friendName"></param>
        /// <returns></returns>
        public List<ERUserEntity> VagueSearchFriends(string friendName)
        {
            return GetEntityList<ERUserEntity>("VagueSearchFriends", new { FriendName = friendName });
        }
        #endregion

        #region 菜谱方法
        /// <summary>
        /// 增删改:菜肴
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public string OperationCookDetail(ERCookDetailEntity entity, string operationType)
        {
            if (string.Equals(operationType, "2"))
            {
                entity.Code = UniqueIDProvider.GetUniqueID();
            }
            var success = OperationInvoke("OperationCookDetail", entity, operationType);
            return success ? entity.Code : string.Empty;
        }

        public string OperationCookComment(ERCookCommentEntity entity, string operationType)
        {
            var success = OperationInvoke("OperationCookComment", entity, operationType);
            return success ? entity.CookCode : string.Empty;
        }

        /// <summary>
        /// 获取菜肴评价
        /// </summary>
        /// <param name="cookCode"></param>
        /// <returns></returns>
        public List<ERCookCommentEntity> GetCookComments(string cookCode)
        {
            return GetEntityList<ERCookCommentEntity>("GetCookComments", new { CookCode = cookCode });
        }

        /// <summary>
        /// 获取菜肴详细信息
        /// </summary>
        /// <param name="cookCode"></param>
        /// <returns></returns>
        public List<ERCookDetailEntity> GetCooksDetail(string cookCode)
        {
            return GetEntityList<ERCookDetailEntity>("GetCooksDetail", new { CookCode = cookCode });
        }

        /// <summary>
        /// 获取原料
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public List<ERRawMaterialEntity> GetRawMaterials(string materialCode)
        {
            return GetEntityList<ERRawMaterialEntity>("GetRawMaterials", new { MaterialCode = materialCode });
        }

        /// <summary>
        /// 获取原料类型
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public List<ERRawMaterialTypeEntity> GetRawMaterialTypes(string materialCode)
        {
            return GetEntityList<ERRawMaterialTypeEntity>("GetRawMaterialTypes", new { MaterialCode = materialCode });
        }
        #endregion

        #region 建议反馈
        /// <summary>
        /// 增删改:建议
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        public string OperationAdvice(AdvicesEntity entity, string operationType)
        {
            var success = OperationInvoke("OperationAdvice", entity, operationType);
            return success ? entity.UserID : string.Empty;
        }

        public List<AdvicesEntity> GetAdvicesFromRangeDate(DateTime from, DateTime end, string userId, string pwd)
        {
            if (!string.IsNullOrWhiteSpace(LoginForSession(userId, pwd).Session))
            {
                return GetEntityList<AdvicesEntity>("GetAdvicesFromRangeDate", new { From = from, End = end });
            }
            return null;
        }

        public List<AdvicesEntity> GetAllAdvices(string userId, string pwd)
        {
            if (!string.IsNullOrWhiteSpace(LoginForSession(userId, pwd).Session))
            {
                return GetEntityList<AdvicesEntity>("GetAdvicesFromRangeDate", null);
            }
            return null;
        }
        #endregion

        #region 获取方法名称
        public string GetAllMethodsName(string token)
        {
            return CommonUtilities.GetClassMethodsNames(typeof(EasyRestaurant));
        }
        #endregion

        #region 构造方法
        public EasyRestaurant()
        {
            var entries = new List<ISessionEntry> { new SessionCache(), new SessionDataBaseAccess() };
            SessionManager.Instance.SessionWorker.AddSessionEntry(entries);
        }
        #endregion

        #region Private Method

        private bool OperationInvoke<T>(string cmdName, T entity, string operationType)
        {
            var opSuccess = true;
            DataAccessManager.ExecuteCommand(cmdName, cmd =>
            {
                try
                {
                    dynamic o = new { Entity = entity, OpType = operationType };
                    cmd.ExecuteNonQuery(o);
                }
                catch (Exception)
                {
                    opSuccess = false;
                }

            });

            return opSuccess;
        }

        private List<T> GetEntityList<T>(string cmdName, dynamic param) where T : AutoAssignValueDataBase
        {
            var entities = new List<T>();
            Type entityTpye = typeof(T);
            DataAccessManager.ExecuteCommand(cmdName, cmd =>
            {
                using (var reader = cmd.ExecuteDataReader(param))
                {
                    while (reader.Read())
                    {
                        var entity = (T)Activator.CreateInstance(entityTpye);
                        entity.Build(reader);
                        entities.Add(entity);
                    }
                }
            });
            return entities;
        }

        #endregion

    }
}
