using EasyRestaurantBusiness.Data;

namespace EasyRestaurantBusiness.Interface
{
    public interface ILoginEntry
    {
        ERUserEntity LoginForSession(string uid, string pwd);
        string CreateUser(string uid, string pwd);
        bool UpdateUser(string userEntity);
        bool DeleteUser(string uid);
        bool IsUserExists(string uid);
        bool CheckSession(string uid, string session);
    }
}
