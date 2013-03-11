using DMobile.Biz.Interface;
using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERUserEntity : AutoAssignValueDataBase, IUserBase
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string AliasName { get; set; }

        /// <summary>
        /// 活跃分数
        /// </summary>
        public int ActivePoints { get; set; }

        public string UserID { get; set; }

        public string Password { get; set; }

        public string Session { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERUserEntity))
            {
                return false;
            }

            return UserID.Equals((obj as ERUserEntity).UserID);
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode();
        }
    }
}
