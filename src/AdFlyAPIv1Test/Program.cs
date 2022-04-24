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
            //UserID = Convert.ToUInt64(Console.ReadLine());
            UserID = 1503418;

            Console.WriteLine("Please Write Your Adfly API Key :\n");
            //PublicAPI = Convert.ToString(Console.ReadLine());
            PublicAPI = "4035c8e1d3931ac1fec5f8d1cec122c1";

            Console.Clear();

            AdflyApi adflyApi = new AdflyApi(PublicAPI, "6dace585-d394-42e5-9194-5902d4a0002e", UserID);

            List<string> LongURLs = new List<string>();

            LongURLs.Add("https://www.facebook.com/");
            LongURLs.Add("https://www.google.com/");
            LongURLs.Add("https://www.yahoo.com/");

            string Responce = adflyApi.Shorten(LongURLs, "j.gs", "int", 0);


            JObject JSONObject = JObject.Parse(Responce);

            JArray JSONArray = (JArray)JSONObject["data"];

            List<ResponseData> Response = JSONArray.ToObject<List<ResponseData>>();

            //Console.WriteLine(JSONObject);

            for (int i = 0; i < Response.Count; i++)
            {
                Console.WriteLine("===================== " + (i + 1) + " =====================");

                Console.WriteLine(Response[i].id);
                Console.WriteLine(Response[i].url);
                Console.WriteLine(Response[i].short_url);
            }

            string test = adflyApi.GetUrls();

            JObject Object = JObject.Parse(test);

            Console.WriteLine(Object);


            Console.ReadLine();
        }
    }
}
