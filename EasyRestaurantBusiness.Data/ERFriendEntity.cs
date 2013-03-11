using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERFriendEntity : AutoAssignValueDataBase
    {
        public string UserID { get; set; }

        public string FriendId { get; set; }

        public FriendStatus FriendStatus { get; set; }

        public string CommunicationLog { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERFriendEntity))
            {
                return false;
            }

            return UserID.Equals((obj as ERFriendEntity).UserID) &
                    FriendId.Equals((obj as ERFriendEntity).FriendId);
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode() ^ FriendId.GetHashCode();
        }
    }

    public enum FriendStatus
    {
        Applying,
        Friend,
        BosomFriend
    }
}
