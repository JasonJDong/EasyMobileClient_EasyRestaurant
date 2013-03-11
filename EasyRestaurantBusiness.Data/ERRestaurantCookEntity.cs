using System;
using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERRestaurantCookEntity : AutoAssignValueDataBase
    {
        public string RestaurantID { get; set; }
        public string CookCode { get; set; }
        public short EvaluationRate { get; set; }
        public int BookedNumber { get; set; }
        public double ReferPrice { get; set; }
        public string PriceUpdaterID { get; set; }
        public DateTime PriceUpdateTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERRestaurantCookEntity))
            {
                return false;
            }

            return CookCode.Equals((obj as ERRestaurantCookEntity).CookCode) &
                RestaurantID.Equals((obj as ERRestaurantCookEntity).RestaurantID);
        }

        public override int GetHashCode()
        {
            return CookCode.GetHashCode() ^ RestaurantID.GetHashCode();
        }
    }
}
