using Google.Apis.Customsearch.v1;

namespace WebMining.SearchProvidor.Google
{
   // Example URL
   // https://www.googleapis.com/customsearch/v1?key=AIzaSyBpSJoc9SWciC9960FPjFJ3Jzos4CWOdBY&cx=009465036018407574794:aibhbi8fj30&q=alligators
   public class CustomSearch
   {
      private readonly CustomsearchService _cse;
      private readonly string _cx;

      public CustomSearch(string key, string cx)
      {
         _cx = cx;
         _cse = new CustomsearchService { Key = key };
      }

      public CseResource.ListRequest CreateRequest(string query)
      {
         return new CseResource.ListRequest(_cse, query) { Cx = _cx };
      }
   }
}
