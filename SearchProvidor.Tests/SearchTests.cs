using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebMining.SearchProvidor;
using WebMining.SearchProvidor.Bing;
using WebMining.SearchProvidor.Google;
using WebMining.SearchProvidor.Solr;

namespace WebMining.SearchProvidor.Tests
{
   [TestClass]
   public class SearchTests
   {
      [TestMethod]
      public void SolrTest()
      {
         var target = new SolrSearch("siemens17042013");
         var results = target.Search("siemens");
         Debugger.Break();
      }

      //[TestMethod]
      //public void SearchTest()
      //{
      //   var results = CustomSearch.Search("alligators", 1);
      //}

      //[TestMethod]
      //public void TestGoogleCustomSearch()
      //{
      //   var results = CustomSearch.Search("alligators");

      //   var serializer = new DataContractSerializer(results.GetType());

      //   using (var file = File.OpenWrite("alligators.xml"))
      //   {
      //      serializer.WriteObject(file, results);
      //   }

      //}

      //[TestMethod]
      //public void TestBingSearch()
      //{
      //   var bingResults = BingSearch.Search("alligators");
      //   var normalizedResults = NormalizeSearchResults.FromBing(bingResults, 0);
      //}

      //[TestMethod]
      //public void TestYahooBossSearch()
      //{
      //   var yahooResults = BossApi.WebSearch.Search("hotdogs -choking -site:wikipedia.org");
      //}

      //[TestMethod]
      //public void TestAlsoTryQueries()
      //{
      //   var suggestions = BossApi.AlsoTryQueries.GetSuggestions("alligator");
      //}
   }
}
