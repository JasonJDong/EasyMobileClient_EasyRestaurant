using System;
using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERCookCommentEntity : AutoAssignValueDataBase
    {
        public string CookCode { get; set; }
        public string RestaurantID { get; set; }
        public string CommentContent { get; set; }
        public string CommentUserID { get; set; }
        public DateTime CommentTime { get; set; }
        public short EvaluationRate { get; set; }
        public int CommentUseful { get; set; }
        public int CommentUnuseful { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERCookCommentEntity))
            {
                return false;
            }

            return CookCode.Equals((obj as ERCookCommentEntity).CookCode);
        }

        public override int GetHashCode()
        {
            return CookCode.GetHashCode();
        }
    }
}
