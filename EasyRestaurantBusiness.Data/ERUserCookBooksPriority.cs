using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERUserCookBooksPriority : AutoAssignValueDataBase
    {
        public string UserID { get; set; }

        public string Priorty { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is ERUserCookBooksPriority))
            {
                return false;
            }
            return string.Equals(UserID, (obj as ERUserCookBooksPriority).UserID);
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode();
        }
    }
}