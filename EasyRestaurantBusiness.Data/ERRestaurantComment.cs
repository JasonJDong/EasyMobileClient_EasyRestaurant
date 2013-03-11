using System;
using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERRestaurantComment : AutoAssignValueDataBase
    {
        public string RestaurantID { get; set; }
        public string CommentContent { get; set; }
        public string FromUserID { get; set; }
        public DateTime CommentTime { get; set; }
        public int CommentUseful { get; set; }
        public int CommentUnuseful { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERRestaurantComment))
            {
                return false;
            }

            return RestaurantID.Equals((obj as ERRestaurantComment).RestaurantID) &
                   FromUserID.Equals((obj as ERRestaurantComment).FromUserID) &
                   CommentContent.Equals((obj as ERRestaurantComment).CommentContent) &
                   CommentTime.Equals((obj as ERRestaurantComment).CommentTime);
        }

        public override int GetHashCode()
        {
            return RestaurantID.GetHashCode() ^
                   FromUserID.GetHashCode() ^
                   CommentContent.GetHashCode() ^
                   CommentTime.GetHashCode();
        }
    }
}
