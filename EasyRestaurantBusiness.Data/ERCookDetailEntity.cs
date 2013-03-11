using EasyRestaurantBusiness.Data.Base;

namespace EasyRestaurantBusiness.Data
{
    public class ERCookDetailEntity : AutoAssignValueDataBase
    {
        public int TopParent { get; set; }
        public int SubParent { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MajorMaterials { get; set; }
        public string Attributes { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ERCookDetailEntity))
            {
                return false;
            }

            return Code.Equals((obj as ERCookDetailEntity).Code);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}
