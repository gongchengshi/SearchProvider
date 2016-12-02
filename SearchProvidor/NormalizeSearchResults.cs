using System;
using System.Collections.Generic;
using Bing;
using SolrNet;

namespace WebMining.SearchProvidor
{
   public static class NormalizeSearchResults
   {
      public static SearchResults FromBing(ICollection<WebResult> bingResults, int start)
      {
         var results = new SearchResults(
            string.Empty, // Todo: get this from the XML Results.  This probably requires changing BingSearchContainer
            bingResults.Count,
            -1, // Bing doesn't send this
            start);

         var batchOrdinal = 0;
         var queryOrdinal = start;

         foreach (var bingResult in bingResults)
         {
            var result = new SearchResult
               {
                  BatchOrdinal = batchOrdinal++,
                  QueryOrdinal = queryOrdinal++,
                  Url = bingResult.Url,
                  HtmlUrl = bingResult.DisplayUrl,
                  // Example: http://cc.bingj.com/cache.aspx?d=4932603268761578&w=idN5AP2KwZrDFuQAPQWTBAV6-Yehr461
                  CacheUrl = string.Empty, // Todo: Figure out how to get the Cache URL
                  Title = bingResult.Title,
                  HtmlTitle = bingResult.Title,
                  Snippet = bingResult.Description,
                  HtmlSnippet = bingResult.Description
               };

            results.Add(result);
         }

         return results;
      }

      public static SearchResults FromGoogleCustomSearch(global::Google.Apis.Customsearch.v1.Data.Search gcsResults)
      {
         var request = gcsResults.Queries["request"][0];
         var results = new SearchResults(
            request.ExactTerms, 
            gcsResults.Items.Count, 
            int.Parse(gcsResults.SearchInformation.TotalResults), 
            Convert.ToInt32(request.StartIndex.Value));

         var batchOrdinal = 0;
         var queryOrdinal = results.QueryStart;

         foreach (var gcsResult in gcsResults.Items)
         {
            var result = new SearchResult
               {
                  BatchOrdinal = batchOrdinal++,
                  QueryOrdinal = queryOrdinal++,
                  Url = gcsResult.Link,
                  HtmlUrl = gcsResult.HtmlFormattedUrl,
                  CacheUrl = string.Format("http://webcache.googleusercontent.com/search?q=cache:{0}:{1}", 
                     gcsResult.CacheId, gcsResult.Link),
                  Title = gcsResult.Title,
                  HtmlTitle = gcsResult.HtmlTitle,
                  Snippet = gcsResult.Snippet,
                  HtmlSnippet = gcsResult.HtmlSnippet
               };

            results.Add(result);
         }

         return results;
      }

      public static SearchResults FromYahooBoss(Yahoo.Boss.BossWebSearchResponse bossResults, string query)
      {
         var results = new SearchResults(
            query,
            bossResults.Count,
            bossResults.TotalResults,
            bossResults.Start
            );

         var batchOrdinal = 0;
         var queryOrdinal = results.QueryStart;

         foreach (var bossResult in bossResults.Results)
         {
            var result = new SearchResult
               {
                  BatchOrdinal = batchOrdinal++,
                  QueryOrdinal = queryOrdinal++,
                  Url = bossResult.Url,
                  HtmlUrl = bossResult.DisplayURl,
                  // example:
                  // http://74.6.116.71/search/srpcache?ei=UTF-8&p=c%23+strip+html&vm=r&fr=crmas&u=http://cc.bingj.com/cache.aspx?q=c%23+strip+html&d=4714388897793329&w=iYNpm0Wkcl9f2yCFkMI5wRa72IFiWm_0&icp=1&.intl=us&sig=DHDDZxF3Q1kJw4JCRf2m4w--
                  // The sig parameter seems to be a checksum of the URL.  Perhaps to prevent manufacturing the URL?  I don't know the checksum algorithm yet.
                  CacheUrl = string.Empty, // Todo: set CacheUrl.  It is mostly the same as Bing.  See above.
                  Title = HtmlRemoval.StripTags(bossResult.Title),
                  HtmlTitle = bossResult.Title,
                  Snippet = HtmlRemoval.StripTags(bossResult.Abstract),
                  HtmlSnippet = bossResult.Abstract
               };

            results.Add(result);
         }

         return results;
      }

      public static SearchResults FromSolr(SolrQueryResults<Solr.SearchResult> solrResults, string query, int start)
      {
         var results = new SearchResults(
            query,
            solrResults.Count,
            solrResults.NumFound,
            start);

         var batchOrdinal = 0;
         var queryOrdinal = start;

         foreach (var solrResult in solrResults)
         {
            string snippet = string.Empty;
            foreach (var snippetsPerKeyword in solrResults.Highlights[solrResult.Id].Snippets.Values)
            {
               foreach (var snippets in snippetsPerKeyword)
               {
                  foreach (var snippetPerKeyword in snippets)
                  {
                     snippet += "[" + snippetPerKeyword + "]";
                  }
               }
            }

            var title = (solrResult.Title == null) ? "No Title" : solrResult.Title[0];
            var url = "https://mimeo.<domain>.com/crawled-content.<crawl-job>/" + solrResult.Id;

            var result = new SearchResult
               {
                  BatchOrdinal = batchOrdinal++,
                  QueryOrdinal = queryOrdinal++,
                  Url = url,
                  HtmlUrl = solrResult.Url,
                  CacheUrl = string.Empty,
                  Title = title,
                  HtmlTitle = title,
                  Snippet = HtmlRemoval.StripTags(snippet),
                  HtmlSnippet = snippet
               };

            results.Add(result);
         }

         return results;
      }
   }
}