using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERUserPointsEntity : AutoAssignValueDataBase
    {
        public string UserID { get; set; }
        public int Points { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERUserPointsEntity))
            {
                return false;
            }

            return UserID.Equals((obj as ERUserPointsEntity).UserID);
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode();
        }
    }
}
