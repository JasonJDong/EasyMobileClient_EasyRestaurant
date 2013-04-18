using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using EasyRestaurantBusiness;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BizTest
{
    [TestClass]
    public class EasyRestaurantTest
    {
        private EasyRestaurant easyRestaurant = new EasyRestaurant();
        [TestMethod]
        public void TestLoginForSession()
        {
            var user = easyRestaurant.LoginForSession("dong", "12345");
            Assert.IsNotNull(user.Session);
        }
    }
}
