using System;
using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERTeamOperationEntity : AutoAssignValueDataBase
    {
        public string TeamID { get; set; }
        public string UserID { get; set; }
        public TeamOperationType OperationType { get; set; }
        public DateTime OperationTime { get; set; }
        public string OperationNote { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERTeamOperationEntity))
            {
                return false;
            }

            return TeamID.Equals((obj as ERTeamOperationEntity).TeamID) &
                   UserID.Equals((obj as ERTeamOperationEntity).UserID);
        }

        public override int GetHashCode()
        {
            return TeamID.GetHashCode() ^ UserID.GetHashCode();
        }
    }

    public enum TeamOperationType
    {
        Applying,
        Joined,
        Refuse
    }
}
