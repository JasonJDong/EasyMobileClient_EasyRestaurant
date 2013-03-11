using System.Runtime.Serialization;
using EasyRestaurantBusiness.Data.Base;
namespace EasyRestaurantBusiness.Data
{
    [KnownType(typeof(NetWorkType))]
    [KnownType(typeof(UserOnlineStatus))]
    [KnownType(typeof(AutoAssignValueDataBase))]
    public class ERUserLocationEntity : AutoAssignValueDataBase
    {
        public string UserID { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public NetWorkType NetWorkType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int City { get; set; }
        public UserOnlineStatus OnlineStatus { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERUserLocationEntity))
            {
                return false;
            }

            return UserID.Equals((obj as ERUserLocationEntity).UserID);
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode();
        }
    }

    public enum NetWorkType
    {
        WIFI,
        G2,
        G3,
        G4,
    }

    public enum UserOnlineStatus
    {
        Online,
        Offline,
        Waiting,
        RefuseInvite
    }
}
