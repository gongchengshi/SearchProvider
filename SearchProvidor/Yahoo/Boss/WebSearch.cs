using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WebMining.SearchProvidor.Yahoo.Boss
{
   public class WebSearch : BossBase
   {
      public bool LongAbstract { get; set; }

      public WebSearch(string consumerKey, string consumerSecret) 
         : base("web", consumerKey, consumerSecret)
      {
         // Set defaults
         LongAbstract = true;
      }

      public BossWebSearchResponse Search(
         string q,
         int start = 0,
         int count = 10,
         bool filterPorn = false,
         bool filterHate = false,
         string inTitle = null,
         string inUrl = null,
         IEnumerable<FileType> fileTypes = null,
         IEnumerable<FileType> excludeFileTypes = null)
      {
         var request = CreateRequest(
            q, start, count, 
            filterPorn, filterHate, 
            inTitle, inUrl, 
            fileTypes, excludeFileTypes);
         using (var response = request.GetResponse())
         {
            return ParseResponse(response);
         }
      }

      private WebRequest CreateRequest(
         string q,
         int start,
         int count,
         bool filterPorn,
         bool filterHate, // I'm not sure if this does anything.
         string inTitle,
         string inUrl,
         IEnumerable<FileType> fileTypes,
         IEnumerable<FileType> excludeFileTypes)
      {
         var urlBuilder = new StringBuilder(_baseUrl, 1024);
         AppendKeyValue("q", EscapeText(q), ref urlBuilder);
         AppendKeyValue("count", count, ref urlBuilder);
         if (start > 0)
         {
            AppendKeyValue("start", start, ref urlBuilder);
         }
         if (LongAbstract)
         {
            AppendKeyValue("abstract", "long", ref urlBuilder);
         }
         // The filter parameter seems to have been removed from the API sometime in September 2013.
         //if (filterPorn | filterHate)
         //{
         //   var filter = string.Format("{0}{1}{2}", filterPorn ? "-porn" : string.Empty,
         //                              (filterPorn & filterHate) ? "," : string.Empty,
         //                              filterHate ? "-hate" : string.Empty);
         //   AppendKeyValue("filter", filter, ref urlBuilder);
         //}
         var fileTypesParam = (fileTypes != null) ? string.Join(",", fileTypes) : null;

         if (excludeFileTypes != null)
         {
            var builder = new StringBuilder(fileTypesParam ?? string.Empty, 50);
            foreach (var fileType in excludeFileTypes)
            {
               if (builder.Length > 0)
               {
                  builder.Append(',');
               }
               builder.Append('-');
               builder.Append(fileType);
            }
            fileTypesParam = builder.ToString();
         }

         if (fileTypesParam != null)
         {
            AppendKeyValue("type", fileTypesParam, ref urlBuilder);
         }

         if (inTitle != null)
         {
            AppendKeyValue("title", EscapeText(inTitle), ref urlBuilder);
         }
         if (inUrl != null)
         {
            AppendKeyValue("url", EscapeText(inUrl), ref urlBuilder);
         }

         return CreateRequest(new Uri(urlBuilder.ToString()));
      }

      private static void AppendKeyValue<T>(string key, T value, ref StringBuilder builder)
      {
         builder.AppendFormat("&{0}={1}", key, value);
      }

      private static BossWebSearchResponse ParseResponse(WebResponse response)
      {
         using (var responseStream = new StreamReader(response.GetResponseStream()))
         {
            var root = JObject.Parse(responseStream.ReadToEnd());

            var bossResponse = root["bossresponse"];
            var web = bossResponse["web"];

            var results = new BossWebSearchResponse
            {
               StatusCode = int.Parse((string)bossResponse["responsecode"]),
               Start = int.Parse((string)web["start"]),
               Count = int.Parse((string)web["count"]),
               TotalResults = int.Parse((string)web["totalresults"]),
               Results = new List<BossWebSearchResult>()
            };

            var resultsArray = (JArray)web["results"];
            if (resultsArray != null)
            {
               foreach (var item in resultsArray)
               {
                  var result = new BossWebSearchResult
                     {
                        ClickUrl = (string) item["clickurl"],
                        Url = (string) item["url"],
                        DisplayURl = (string) item["dispurl"],
                        Title = (string) item["title"],
                        Abstract = (string) item["abstract"]
                     };

                  DateTime.TryParse((string) item["date"], out result.Date);
                  results.Results.Add(result);
               }
            }

            return results;
         }
      }
   }
}
