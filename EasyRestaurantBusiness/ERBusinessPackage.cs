using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using Business.Data;
using Utilities;

namespace Business
{
    public class ERBusinessPackage
    {
        private readonly DataContractJsonSerializer JsonForUser = new DataContractJsonSerializer(typeof(List<ERUserEntity>));
        private readonly DataContractJsonSerializer JsonForTeam = new DataContractJsonSerializer(typeof(List<ERTeamDetailEntity>));
        private readonly DataContractJsonSerializer JsonForRestaurant = new DataContractJsonSerializer(typeof(List<ERRestaurantEntity>));

        public string GetUserEntityJson(List<ERUserEntity> instance)
        {
            try
            {
                return SerializerProvider.SerializeToJson(JsonForUser, instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetTeamEntityJson(List<ERTeamDetailEntity> instance)
        {
            try
            {
                return SerializerProvider.SerializeToJson(JsonForTeam, instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetRestaurantEntityJson(List<ERRestaurantEntity> instance)
        {
            try
            {
                return SerializerProvider.SerializeToJson(JsonForRestaurant, instance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ERTeamDetailEntity> GetTeamEntityObject(string json)
        {
            try
            {
                return SerializerProvider.DeserializeByJson(JsonForRestaurant, json) as List<ERTeamDetailEntity>;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
