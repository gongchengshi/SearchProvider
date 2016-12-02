using System;
using System.Collections.Generic;
using System.Net;
using Bing;
using System.Linq;

namespace WebMining.SearchProvidor.Bing
{
   public class BingSearch
   {
      private const string _azureKey = "9x5BxC2x3Lj3NkcM6JFHemC6+3e2CweF2x+kOhRnCag=";

      private static readonly BingSearchContainer _bing = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/SearchWeb/"))
         {
            Credentials = new NetworkCredential(_azureKey, _azureKey),

         };

      public enum SaveSearchLevel
      {
         Strict,
         Moderate,
         Off
      }

      public enum WebFileType
      {
         // ReSharper disable InconsistentNaming         
         DOC,
         DWF,
         FEED, // RSS feed
         HTM,
         HTML,
         PDF,
         PPT,
         RTF,
         TEXT,
         TXT,
         XLS
         // ReSharper restore InconsistentNaming
      }

      public static ICollection<WebResult> Search(
         string q,
         int numResults = 50,
         int start = 0, 
         WebFileType? fileType = null,
         SaveSearchLevel? saveSearchLevel = null,
         bool disableLocationDetection = false, 
         bool enableHighlighting = false, 
         bool disableHostCollapsing = false,
         bool disableQueryAlterations = false)
      {
         var options = disableLocationDetection ? "DisableLocationDetection" : null;
         options = enableHighlighting ? (options == null? string.Empty : options + "+") + "EnableHighlighting" : options;

         var webSearchOptions = disableHostCollapsing ? "DisableHostCollapsing" : null;
         webSearchOptions = disableQueryAlterations ? (webSearchOptions == null ? string.Empty : webSearchOptions + "+") + "DisableQueryAlterations" : webSearchOptions;

         string market = "es-US";

         var adult = saveSearchLevel.HasValue ? saveSearchLevel.Value.ToString() : null;

         double? latitude = null;
         double? longitude = null;

         var webFileType = fileType.HasValue ? fileType.ToString() : null;

         var query = _bing.Web(q, options, webSearchOptions, market, adult, latitude, longitude, webFileType);

         query.AddQueryOption("$top", numResults);
         query.AddQueryOption("$skip", start);

         query.IncludeTotalCount();
         return query.Execute().ToArray(); // It can only be enumerated once so lets do it here.
      }
   }
}
