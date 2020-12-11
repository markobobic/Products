using Newtonsoft.Json;
using Products.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Products.Helpers
{
    public static class JsonCRUD
    {
        public static void UpdateJson(Product toSerelize, string path)
        {
            var deserilizeData = LoadJson(path);
            deserilizeData.RemoveWhere(x => x.Id == toSerelize.Id);
            deserilizeData.Add(toSerelize);
            try
            {
                string json = JsonConvert.SerializeObject(deserilizeData, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public static void AddToJson(Product toSerialize, string path)
        {

            var deserilizeData = LoadJson(path);
            deserilizeData.Add(toSerialize);
            try
            {
                string json = JsonConvert.SerializeObject(deserilizeData, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {

                throw e;
            }



        }
        private static List<Product> LoadJson(string path)
        {
            try
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(System.IO.File.ReadAllText(path));
                return products ?? new List<Product>();
            }
            catch (Exception e)
            {

                throw e;
            }

        }

    }
}