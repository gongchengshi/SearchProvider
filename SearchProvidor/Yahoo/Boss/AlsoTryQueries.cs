using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebMining.SearchProvidor.Yahoo.Boss
{
   public class AlsoTryQueries : BossBase
   {
      public AlsoTryQueries(string consumerKey, string consumerSecret) 
         : base("related", consumerKey, consumerSecret)
      {}

      public ICollection<string> GetSuggestions(string q, int count = 4)
      {
         var request = CreateRequest(q, count);
         using (var response = request.GetResponse())
         {
            return ParseResponse(response);
         }
      }

      private WebRequest CreateRequest(string q, int count)
      {
         var uri = new Uri(string.Format(_baseUrl + "&q={0}&count={1}", q, count));
         return CreateRequest(uri);
      }

      private ICollection<string> ParseResponse(WebResponse response)
      {
         using (var responseStream = new StreamReader(response.GetResponseStream()))
         {
            var root = JObject.Parse(responseStream.ReadToEnd());

            var suggestions = root["bossresponse"]["related"]["results"].Select(item => (string) item["suggestion"]).ToList();
            return suggestions;
         }
      }
   }
}