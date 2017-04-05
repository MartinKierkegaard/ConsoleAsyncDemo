using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAsyncDemo
{
    class Program
    {
        static List<Guest> list = new List<Guest>();

        static void Main(string[] args)
        {

            const string serverUrl = "http://webservicehotelguestasyncdemo20170403082435.azurewebsites.net/";

            Console.WriteLine("START");

            PrintGuestsAsync(serverUrl);

            for (int i = 0; i < 800; i++)
            {
                Console.WriteLine("*****" + i);
            }
            //PostGuestAsAsync(serverUrl);

            Console.ReadLine();


        }

        public static async Task PostGuestAsAsync(string serverUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                for (int i = 0; i < 1999; i++)
                {
                   
                    string urlString = "api/Guests/";
                    try
                    {
                        var g = new Guest();
                        g.Guest_No = 5000 + i;
                        g.Address = "Vej " + g.Guest_No;
                        g.Name = "navn " + g.Guest_No;

                        
                        //var response =  client.GetAsync(urlString).Result;
                        var response = await client.PostAsJsonAsync<Guest>(urlString,g);
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("oprettet " + g.Name);
                            //var guestList = response.Content.ReadAsAsync<List<Guest>>().Result;

                            ////return guestList;

                        }
                        //messageError = response.StatusCode.ToString();
                        //return null;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.InnerException);
                        //messageError = "Der er sket en fejl : " + e.Message;
                        //return null;
                    }
                }
             
            }
        }

        //public static async Task PrintGuestsAsync(string serverUrl)
        public async static Task PrintGuestsAsync(string serverUrl)
        {
            //list = GetAllGuestAsync(serverUrl).Result;
            list = await GetAllGuestAsync(serverUrl);

            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static async Task<List<Guest>> GetAllGuestAsync(string serverUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string urlString = "api/Guests/";
                try
                {
                    //var response =  client.GetAsync(urlString).Result;
                    var response = await client.GetAsync(urlString);
                    if (response.IsSuccessStatusCode)
                    {

                        var guestList = response.Content.ReadAsAsync<List<Guest>>().Result;

                        return guestList;

                    }
                    //messageError = response.StatusCode.ToString();
                    return null;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                    //messageError = "Der er sket en fejl : " + e.Message;
                    return null;
                }
            }
        }

    }
}
