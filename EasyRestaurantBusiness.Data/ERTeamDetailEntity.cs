using System;
using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    /// <summary>
    /// 队伍实体
    /// </summary>
    public class ERTeamDetailEntity : AutoAssignValueDataBase
    {
        public string TeamName { get; set; }

        public string TeamID { get; set; }

        public string HolderID { get; set; }

        public string TeamUseRestaurantID { get; set; }

        public MealTogetherStyle TeamAffordStyle { get; set; }

        public DateTime MealTime { get; set; }

        public int MaxPeopleNumber { get; set; }

        public TeamAliveStatus AliveStatus { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERTeamDetailEntity))
            {
                return false;
            }

            return TeamID.Equals((obj as ERTeamDetailEntity).TeamID);
        }

        public override int GetHashCode()
        {
            return TeamID.GetHashCode();
        }
    }

    public enum MealTogetherStyle
    {
        /// <summary>
        /// AA制
        /// </summary>
        AA = 0,
        /// <summary>
        /// 为自己付费
        /// </summary>
        AffordSelf,
        /// <summary>
        /// 请客
        /// </summary>
        AffordTeamCreater
    }

    public enum TeamAliveStatus
    {
        /// <summary>
        /// 激活
        /// </summary>
        Alive = 0,
        /// <summary>
        /// 关闭
        /// </summary>
        Shutdown,
        /// <summary>
        /// 冻结
        /// </summary>
        Freezen
    }
}
