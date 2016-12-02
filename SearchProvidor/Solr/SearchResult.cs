using System;
using SolrNet.Attributes;

namespace WebMining.SearchProvidor.Solr
{
   public class SearchResult
   {
      [SolrUniqueKey("id")]
      public string Id { get; set; }

      [SolrField("url")]
      public string Url { get; set; }
      [SolrField("title")]
      public string[] Title { get; set; }
      [SolrField("last_modified")]
      public DateTime LastModified { get; set; }
      [SolrField("last_crawled")]
      public DateTime LastCrawled { get; set; }
   }
}
