using System;
using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class AdvicesEntity : AutoAssignValueDataBase
    {
        public int UniqueID { get; set; }
        public string Content { get; set; }
        public string UserID { get; set; }
        public AdviceType Type { get; set; }
        public DateTime Time { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AdvicesEntity))
            {
                return false;
            }

            return UniqueID.Equals((obj as AdvicesEntity).UniqueID);
        }

        public override int GetHashCode()
        {
            return UniqueID.GetHashCode();
        }
    }

    public enum AdviceType
    {
        ApplicationBugs = 0,
        UIPerformance,
        AbilityImprovement,
        AbilityAddition
    }
}
