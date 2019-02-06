using ENoticeBoard.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Http;

namespace ENoticeBoard.Controllers
{
    public class TicketsApiController : ApiController
    {
        //static HttpClient client= new HttpClient();


        private static readonly string url = "https://vapp01:9676/api/tickets.json";
        private static readonly string api = "/api/tickets.json";

        private static readonly string email = "ken.le@oneharvest.com.au";
        private static readonly string password = "Diploma#1";
        private static readonly string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(email + ":" + password));

        //public static List<string> GetTickets()
        //{
        //    Dictionary<string, string> tokensDetails = null;
        //    HttpClient client = new HttpClient { BaseAddress = new Uri(url) };
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //    var login = new Dictionary<string, string>
        //    {
        //        {"username",email},
        //        {"password",password}
        //    };

        //    //HttpResponseMessage response = client.GetAsync("ticket").Result;
        //    //if (response.IsSuccessStatusCode)
        //    //{
        //    //    var dataObjects = response.Content.ReadAsAsync<IEnumerable<TicketFormViewModel>>().Result;
        //    //    foreach (var t in dataObjects)
        //    //    {
        //    //        Console.WriteLine("{0}", t.Id);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);

        //    //}
        //    //client.Dispose();

        //    HttpResponseMessage response = client.PostAsync(api, new FormUrlEncodedContent(login)).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        tokensDetails =
        //            JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content.ReadAsStringAsync()
        //                .Result);
        //        if (tokensDetails != null & tokensDetails.Any())
        //        {
        //            var tokenNO = tokensDetails.FirstOrDefault().Value;
        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " +tokenNO);
        //            HttpResponseMessage ticketMessage = client.GetAsync(api).Result;
        //            if (ticketMessage.IsSuccessStatusCode)
        //            {
        //                var data = ticketMessage.Content.ReadAsStringAsync();
        //                List<string> ticketsList = JsonConvert.DeserializeObject<List<string>>(data.Result);
        //                return ticketsList;
        //            }

        //        }
        //    }

        //    return null;
        //}
        public static List<Ticket> GetTickets()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Basic " + encoded);
            request.AllowAutoRedirect = false;
            var response = request.GetResponse() as HttpWebResponse;
            if (response != null)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        StreamReader reader = new StreamReader(responseStream);
                        string jsonString = reader.ReadToEnd();
                        List<Ticket> tickets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Ticket>>(jsonString);
                        return tickets;
                    }

                    return null;
                }
            }
            return null;
        }
        
    }
}
