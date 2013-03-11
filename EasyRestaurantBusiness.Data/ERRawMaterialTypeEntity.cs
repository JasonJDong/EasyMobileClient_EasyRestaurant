using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERRawMaterialTypeEntity : AutoAssignValueDataBase
    {
       public ushort TypeCode { get; set; }
       public string Name { get; set; }
       public ushort ParentCode { get; set; }

       public override bool Equals(object obj)
       {
           if (obj == null || !(obj is ERRawMaterialTypeEntity))
           {
               return false;
           }

           return TypeCode.Equals((obj as ERRawMaterialTypeEntity).TypeCode);
       }

       public override int GetHashCode()
       {
           return TypeCode.GetHashCode();
       }
    }
}
