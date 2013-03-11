using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using EasyRestaurantBusiness.Data;

namespace EasyRestaurantBusiness
{
    public partial class EasyRestaurant
    {
        private readonly Dictionary<string, DbOperationType> OperationDic = new Dictionary<string, DbOperationType>
                                                                                {
                                                                                    {"0", DbOperationType.Delete},
                                                                                    {"1", DbOperationType.Update},
                                                                                    {"2", DbOperationType.Insert}
                                                                                };
        private enum DbOperationType
        {
            Delete = 0,
            Update = 1,
            Insert = 2
        }
    }
}
