using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERRawMaterialEntity : AutoAssignValueDataBase
    {
        public string MaterialCode { get; set; }
        public string MajorType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERRawMaterialEntity))
            {
                return false;
            }

            return MaterialCode.Equals((obj as ERRawMaterialEntity).MaterialCode);
        }

        public override int GetHashCode()
        {
            return MaterialCode.GetHashCode();
        }
    }
}
