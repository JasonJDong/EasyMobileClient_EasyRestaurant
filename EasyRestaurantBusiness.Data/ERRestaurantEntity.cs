using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERRestaurantEntity : AutoAssignValueDataBase
    {
        public string RestaurantID { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Description { get; set; }

        public string RestaurantName { get; set; }

        public int CityCode { get; set; }

        public int CountryCode { get; set; }

        public double FavoriteRate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERRestaurantEntity))
            {
                return false;
            }

            return RestaurantID.Equals((obj as ERRestaurantEntity).RestaurantID);
        }

        public override int GetHashCode()
        {
            return RestaurantID.GetHashCode();
        }
    }
}
