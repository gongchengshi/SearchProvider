using System;
using System.Collections.Generic;

namespace WebMining.SearchProvidor.Yahoo.Boss
{
   public class BossResponse
   {
      public int StatusCode;
   }

   public class BossWebSearchResponse : BossResponse
   {
      public int Start;
      public int Count;
      public int TotalResults;
      public ICollection<BossWebSearchResult> Results;
   }

   public class BossWebSearchResult
   {
      public DateTime Date;
      public string ClickUrl;
      public string Url;
      public string DisplayURl;
      public string Title;
      public string Abstract;
   }
}