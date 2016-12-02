using System.Collections.Generic;

namespace WebMining.SearchProvidor
{
   public class SearchResult
   {
      public int BatchOrdinal { get; set; }
      public int QueryOrdinal { get; set; }
      public string Url { get; set; }
      public string HtmlUrl { get; set; }
      public string CacheUrl { get; set; }
      public string Snippet { get; set; }
      public string HtmlSnippet { get; set; }
      public string Title { get; set; }
      public string HtmlTitle { get; set; }
      public bool GoogleBotBlocked { get; set; }
   }

   public class SearchResults : List<SearchResult>
   {
      public int QueryStart { get; private set; }
      public int QueryEnd { get; set; }
      public int TotalResults { get; set; }
      public string Query { get; private set; }
      public int NumDuplicateResults { get; set; }

      public SearchResults(string query, int count, int total, int start) : base(count)
      {
         Query = query;
         TotalResults = total;
         QueryStart = start;
         QueryEnd = start + count;
      }

      public SearchResults(string query, int total, int start, ICollection<SearchResult> items)
         : base(items)
      {
         Query = query;
         TotalResults = total;
         QueryStart = start;
         QueryEnd = start + items.Count;
      }

      public SearchResults(string query, int start, int capacity) : base(capacity)
      {
         Query = query;
         QueryStart = start;
      }
   }
}
