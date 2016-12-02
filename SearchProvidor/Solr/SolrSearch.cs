using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;

namespace WebMining.SearchProvidor.Solr
{
   public class SolrSearch
   {
      private readonly ISolrOperations<SearchResult> _solr;
      public string IndexName { get; private set; }

      private static bool _initialized;

      public SolrSearch(string indexName)
      {
         IndexName = indexName;

         if (!_initialized)
         {
            Startup.Init<SearchResult>(string.Format("http://solr.<domain>:8080/solr/{0}/", IndexName));
            _initialized = true;
         }

         _solr = ServiceLocator.Current.GetInstance<ISolrOperations<SearchResult>>();
      }

      public SolrQueryResults<SearchResult> Search(
         string q,
         int start = 0,
         int count = 10)
      {
         var queryOptions = new QueryOptions
            {
               Start = start,
               Rows = count,
               Highlight = new HighlightingParameters()
                  {
                     Fields = new[] {"content"},
                     Snippets = 10,
                     Fragsize = 100,
                     HighlightMultiTerm = true,
                     MaxAnalyzedChars = 2000000,
                     MergeContiguous = true
                  }
            };

         return _solr.Query(new SolrQuery(q), queryOptions);
      }
   }
}
