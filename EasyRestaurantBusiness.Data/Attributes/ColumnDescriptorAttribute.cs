using System;

namespace EasyRestaurantBusiness.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnDescriptorAttribute : Attribute
    {
        public String ColumnName { get; set; }
    }
}
