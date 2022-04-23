using AdFlyAPIv1;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace AdFlyAPIv1Test
{
    class Program
    {
        /// <summary>
        /// This Code Only For Testing The AdFlyAPIv1.dll 
        /// </summary>
        static void Main(string[] args)
        {
            ulong UserID;
            string PublicAPI;

            Console.WriteLine("Please Write Your Adfly User ID :\n");
            UserID = Convert.ToUInt64(Console.ReadLine());

            Console.WriteLine("Please Write Your Adfly API Key :\n");
            PublicAPI = Convert.ToString(Console.ReadLine());

            Console.Clear();

            AdflyApi adflyApi = new AdflyApi(PublicAPI, UserID);

            List<string> LongURLs = new List<string>();

            LongURLs.Add("https://www.facebook.com/");
            LongURLs.Add("https://www.google.com/");
            LongURLs.Add("https://www.yahoo.com/");

            string Responce = adflyApi.Shorten(LongURLs, "j.gs", "int", 0);


            JObject JSONObject = JObject.Parse(Responce);

            JArray JSONArray = (JArray)JSONObject["data"];

            List<ResponseData> Response = JSONArray.ToObject<List<ResponseData>>();

            //Console.WriteLine(JSONObject);

            //for (int i = 0; i < Response.Count; i++)
            //{
            //    Console.WriteLine("===================== " + (i + 1) + " =====================");

            //    Console.WriteLine(Response[i].id);
            //    Console.WriteLine(Response[i].url);
            //    Console.WriteLine(Response[i].short_url);
            //}




            Console.ReadLine();
        }
    }
}
