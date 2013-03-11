namespace EasyRestaurantBusiness.Interface
{
    public interface ISessionMethod
    {
        bool FindExists(string session);
        bool InsertOne(string session);
    }
}
