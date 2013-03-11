using System;

namespace EasyRestaurantBusiness.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NotColumnAttribute : Attribute
    {
    }
}
