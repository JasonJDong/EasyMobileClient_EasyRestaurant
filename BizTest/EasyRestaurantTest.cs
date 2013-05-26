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
            //var user = easyRestaurant.LoginForSession("admin", "");
            //Assert.IsNotNull(user.Session);
            var s = "{"+
  "\"MealTime\" : \"2013/05/22 10:56:56\","+
  "\"HolderID\" : \"admin\","+
  "\"TeamUseRestaurantID\" : \"rrrr\"," +
            "\"TeamName\" : \"我的队伍\"," +
            "\"TeamID\" : \"00000000000000000000000000000000\"," +
            "\"MaxPeopleNumber\" : \"8\"," +
            "\"AliveStatus\" : \"0\"," +
            "\"TeamAffordStyle\" : \"0\"" +
            "}";
            Console.WriteLine(s.Length);
        }
    }
}
