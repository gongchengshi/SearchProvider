using System;
using System.Collections.ObjectModel;
using System.Data.Services.Client;

namespace Bing
{
   public class ExpandableSearchResult
   {
      public Guid ID { get; set; }

      public Int64? WebTotal { get; set; }

      public Int64? WebOffset { get; set; }

      public Int64? ImageTotal { get; set; }

      public Int64? ImageOffset { get; set; }

      public Int64? VideoTotal { get; set; }

      public Int64? VideoOffset { get; set; }

      public Int64? NewsTotal { get; set; }

      public Int64? NewsOffset { get; set; }

      public Int64? SpellingSuggestionsTotal { get; set; }

      public String AlteredQuery { get; set; }

      public String AlterationOverrideQuery { get; set; }

      public Collection<WebResult> Web { get; set; }

      public Collection<ImageResult> Image { get; set; }

      public Collection<VideoResult> Video { get; set; }

      public Collection<NewsResult> News { get; set; }

      public Collection<RelatedSearchResult> RelatedSearch { get; set; }

      public Collection<SpellResult> SpellingSuggestions { get; set; }
   }

   public class WebResult
   {
      public Guid ID { get; set; }

      public String Title { get; set; }

      public String Description { get; set; }

      public String DisplayUrl { get; set; }

      public String Url { get; set; }

      //public String CacheUrl { get; set; }
   }

   public class ImageResult
   {
      public Guid ID { get; set; }

      public String Title { get; set; }

      public String MediaUrl { get; set; }

      public String SourceUrl { get; set; }

      public String DisplayUrl { get; set; }

      public Int32? Width { get; set; }

      public Int32? Height { get; set; }

      public Int64? FileSize { get; set; }

      public String ContentType { get; set; }

      public Thumbnail Thumbnail { get; set; }
   }

   public class VideoResult
   {
      public Guid ID { get; set; }

      public String Title { get; set; }

      public String MediaUrl { get; set; }

      public String DisplayUrl { get; set; }

      public Int32? RunTime { get; set; }

      public Thumbnail Thumbnail { get; set; }
   }

   public class NewsResult
   {
      public Guid ID { get; set; }

      public String Title { get; set; }

      public String Url { get; set; }

      public String Source { get; set; }

      public String Description { get; set; }

      public DateTime? Date { get; set; }
   }

   public class RelatedSearchResult
   {
      public Guid ID { get; set; }

      public String Title { get; set; }

      public String BingUrl { get; set; }
   }

   public class SpellResult
   {
      public Guid ID { get; set; }

      public String Value { get; set; }
   }

   public class Thumbnail
   {
      public String MediaUrl { get; set; }

      public String ContentType { get; set; }

      public Int32? Width { get; set; }

      public Int32? Height { get; set; }

      public Int64? FileSize { get; set; }
   }

   public class BingSearchContainer : DataServiceContext
   {
      public BingSearchContainer(Uri serviceRoot) :
         base(serviceRoot)
      {
      }

      /// <summary>
      /// </summary>
      /// <param name="Sources">Bing search sources Sample Values : web+image+video+news+spell</param>
      /// <param name="Query">Bing search query Sample Values : xbox</param>
      /// <param name="Options">Specifies options for this request for all Sources. Valid values are: DisableLocationDetection, EnableHighlighting. Sample Values : EnableHighlighting</param>
      /// <param name="WebSearchOptions">Specify options for a request to the Web SourceType. Valid values are: DisableHostCollapsing, DisableQueryAlterations. Sample Values : DisableQueryAlterations</param>
      /// <param name="Market">Market. Note: Not all Sources support all markets. Sample Values : en-US</param>
      /// <param name="Adult">Adult setting is used for filtering sexually explicit content Sample Values : Moderate</param>
      /// <param name="Latitude">Latitude Sample Values : 47.603450</param>
      /// <param name="Longitude">Longitude Sample Values : -122.329696</param>
      /// <param name="WebFileType">File extensions to return Sample Values : XLS</param>
      /// <param name="ImageFilters">Array of strings that filter the response the API sends based on size, aspect, color, style, face or any combination thereof. Valid values are: Size:Small, Size:Medium, Size:Large, Size:Width:[Width], Size:Height:[Height], Aspect:Square, Aspect:Wide, Aspect:Tall, Color:Color, Color:Monochrome, Style:Photo, Style:Graphics, Face:Face, Face:Portrait, Face:Other. Sample Values : Size:Small+Aspect:Square</param>
      /// <param name="VideoFilters">Array of strings that filter the response the API sends based on size, aspect, color, style, face or any combination thereof. Valid values are: Duration:Short, Duration:Medium, Duration:Long, Aspect:Standard, Aspect:Widescreen, Resolution:Low, Resolution:Medium, Resolution:High. Sample Values : Duration:Short+Resolution:High</param>
      /// <param name="VideoSortBy">The sort order of results returned Sample Values : Date</param>
      /// <param name="NewsLocationOverride">Overrides Bing location detection. This parameter is only applicable in en-US market. Sample Values : US.WA</param>
      /// <param name="NewsCategory">The category of news for which to provide results Sample Values : rt_Business</param>
      /// <param name="NewsSortBy">The sort order of results returned Sample Values : Date</param>
      public DataServiceQuery<ExpandableSearchResult> Composite(String Sources, String Query, String Options,
                                                                String WebSearchOptions, String Market, String Adult,
                                                                Double? Latitude, Double? Longitude, String WebFileType,
                                                                String ImageFilters, String VideoFilters,
                                                                String VideoSortBy, String NewsLocationOverride,
                                                                String NewsCategory, String NewsSortBy)
      {
         if ((Sources == null))
         {
            throw new ArgumentNullException("Sources", "Sources value cannot be null");
         }
         if ((Query == null))
         {
            throw new ArgumentNullException("Query", "Query value cannot be null");
         }
         DataServiceQuery<ExpandableSearchResult> query = base.CreateQuery<ExpandableSearchResult>("Composite");
         query = query.AddQueryOption("Sources", string.Concat("\'", Uri.EscapeDataString(Sources), "\'"));
         query = query.AddQueryOption("Query", string.Concat("\'", Uri.EscapeDataString(Query), "\'"));
         if ((Options != null))
         {
            query = query.AddQueryOption("Options", string.Concat("\'", Uri.EscapeDataString(Options), "\'"));
         }
         if ((WebSearchOptions != null))
         {
            query = query.AddQueryOption("WebSearchOptions",
                                         string.Concat("\'", Uri.EscapeDataString(WebSearchOptions), "\'"));
         }
         if ((Market != null))
         {
            query = query.AddQueryOption("Market", string.Concat("\'", Uri.EscapeDataString(Market), "\'"));
         }
         if ((Adult != null))
         {
            query = query.AddQueryOption("Adult", string.Concat("\'", Uri.EscapeDataString(Adult), "\'"));
         }
         if (((Latitude != null)))
         {
            query = query.AddQueryOption("Latitude", Latitude.Value);
         }
         if (((Longitude != null)))
         {
            query = query.AddQueryOption("Longitude", Longitude.Value);
         }
         if ((WebFileType != null))
         {
            query = query.AddQueryOption("WebFileType", string.Concat("\'", Uri.EscapeDataString(WebFileType), "\'"));
         }
         if ((ImageFilters != null))
         {
            query = query.AddQueryOption("ImageFilters", string.Concat("\'", Uri.EscapeDataString(ImageFilters), "\'"));
         }
         if ((VideoFilters != null))
         {
            query = query.AddQueryOption("VideoFilters", string.Concat("\'", Uri.EscapeDataString(VideoFilters), "\'"));
         }
         if ((VideoSortBy != null))
         {
            query = query.AddQueryOption("VideoSortBy", string.Concat("\'", Uri.EscapeDataString(VideoSortBy), "\'"));
         }
         if ((NewsLocationOverride != null))
         {
            query = query.AddQueryOption("NewsLocationOverride",
                                         string.Concat("\'", Uri.EscapeDataString(NewsLocationOverride), "\'"));
         }
         if ((NewsCategory != null))
         {
            query = query.AddQueryOption("NewsCategory", string.Concat("\'", Uri.EscapeDataString(NewsCategory), "\'"));
         }
         if ((NewsSortBy != null))
         {
            query = query.AddQueryOption("NewsSortBy", string.Concat("\'", Uri.EscapeDataString(NewsSortBy), "\'"));
         }
         return query;
      }

      /// <summary>
      /// </summary>
      /// <param name="Query">Bing search query Sample Values : xbox</param>
      /// <param name="Options">Specifies options for this request for all Sources. Valid values are: DisableLocationDetection, EnableHighlighting. Sample Values : EnableHighlighting</param>
      /// <param name="WebSearchOptions">Specify options for a request to the Web SourceType. Valid values are: DisableHostCollapsing, DisableQueryAlterations. Sample Values : DisableQueryAlterations</param>
      /// <param name="Market">Market. Note: Not all Sources support all markets. Sample Values : en-US</param>
      /// <param name="Adult">Adult setting is used for filtering sexually explicit content Sample Values : Moderate</param>
      /// <param name="Latitude">Latitude Sample Values : 47.603450</param>
      /// <param name="Longitude">Longitude Sample Values : -122.329696</param>
      /// <param name="WebFileType">File extensions to return Sample Values : XLS</param>
      public DataServiceQuery<WebResult> Web(String Query, String Options, String WebSearchOptions, String Market,
                                             String Adult, Double? Latitude, Double? Longitude, String WebFileType)
      {
         if ((Query == null))
         {
            throw new ArgumentNullException("Query", "Query value cannot be null");
         }
         DataServiceQuery<WebResult> query = base.CreateQuery<WebResult>("Web");
         query = query.AddQueryOption("Query", string.Concat("\'", Uri.EscapeDataString(Query), "\'"));
         if ((Options != null))
         {
            query = query.AddQueryOption("Options", string.Concat("\'", Uri.EscapeDataString(Options), "\'"));
         }
         if ((WebSearchOptions != null))
         {
            query = query.AddQueryOption("WebSearchOptions",
                                         string.Concat("\'", Uri.EscapeDataString(WebSearchOptions), "\'"));
         }
         if ((Market != null))
         {
            query = query.AddQueryOption("Market", string.Concat("\'", Uri.EscapeDataString(Market), "\'"));
         }
         if ((Adult != null))
         {
            query = query.AddQueryOption("Adult", string.Concat("\'", Uri.EscapeDataString(Adult), "\'"));
         }
         if (((Latitude != null)))
         {
            query = query.AddQueryOption("Latitude", Latitude.Value);
         }
         if (((Longitude != null)))
         {
            query = query.AddQueryOption("Longitude", Longitude.Value);
         }
         if ((WebFileType != null))
         {
            query = query.AddQueryOption("WebFileType", string.Concat("\'", Uri.EscapeDataString(WebFileType), "\'"));
         }
         return query;
      }

      /// <summary>
      /// </summary>
      /// <param name="Query">Bing search query Sample Values : xbox</param>
      /// <param name="Options">Specifies options for this request for all Sources. Valid values are: DisableLocationDetection, EnableHighlighting. Sample Values : EnableHighlighting</param>
      /// <param name="Market">Market. Note: Not all Sources support all markets. Sample Values : en-US</param>
      /// <param name="Adult">Adult setting is used for filtering sexually explicit content Sample Values : Moderate</param>
      /// <param name="Latitude">Latitude Sample Values : 47.603450</param>
      /// <param name="Longitude">Longitude Sample Values : -122.329696</param>
      /// <param name="ImageFilters">Array of strings that filter the response the API sends based on size, aspect, color, style, face or any combination thereof. Valid values are: Size:Small, Size:Medium, Size:Large, Size:Width:[Width], Size:Height:[Height], Aspect:Square, Aspect:Wide, Aspect:Tall, Color:Color, Color:Monochrome, Style:Photo, Style:Graphics, Face:Face, Face:Portrait, Face:Other. Sample Values : Size:Small+Aspect:Square</param>
      public DataServiceQuery<ImageResult> Image(String Query, String Options, String Market, String Adult,
                                                 Double? Latitude, Double? Longitude, String ImageFilters)
      {
         if ((Query == null))
         {
            throw new ArgumentNullException("Query", "Query value cannot be null");
         }
         DataServiceQuery<ImageResult> query = base.CreateQuery<ImageResult>("Image");
         query = query.AddQueryOption("Query", string.Concat("\'", Uri.EscapeDataString(Query), "\'"));
         if ((Options != null))
         {
            query = query.AddQueryOption("Options", string.Concat("\'", Uri.EscapeDataString(Options), "\'"));
         }
         if ((Market != null))
         {
            query = query.AddQueryOption("Market", string.Concat("\'", Uri.EscapeDataString(Market), "\'"));
         }
         if ((Adult != null))
         {
            query = query.AddQueryOption("Adult", string.Concat("\'", Uri.EscapeDataString(Adult), "\'"));
         }
         if (((Latitude != null)))
         {
            query = query.AddQueryOption("Latitude", Latitude.Value);
         }
         if (((Longitude != null)))
         {
            query = query.AddQueryOption("Longitude", Longitude.Value);
         }
         if ((ImageFilters != null))
         {
            query = query.AddQueryOption("ImageFilters", string.Concat("\'", Uri.EscapeDataString(ImageFilters), "\'"));
         }
         return query;
      }

      /// <summary>
      /// </summary>
      /// <param name="Query">Bing search query Sample Values : xbox</param>
      /// <param name="Options">Specifies options for this request for all Sources. Valid values are: DisableLocationDetection, EnableHighlighting. Sample Values : EnableHighlighting</param>
      /// <param name="Market">Market. Note: Not all Sources support all markets. Sample Values : en-US</param>
      /// <param name="Adult">Adult setting is used for filtering sexually explicit content Sample Values : Moderate</param>
      /// <param name="Latitude">Latitude Sample Values : 47.603450</param>
      /// <param name="Longitude">Longitude Sample Values : -122.329696</param>
      /// <param name="VideoFilters">Array of strings that filter the response the API sends based on size, aspect, color, style, face or any combination thereof. Valid values are: Duration:Short, Duration:Medium, Duration:Long, Aspect:Standard, Aspect:Widescreen, Resolution:Low, Resolution:Medium, Resolution:High. Sample Values : Duration:Short+Resolution:High</param>
      /// <param name="VideoSortBy">The sort order of results returned Sample Values : Date</param>
      public DataServiceQuery<VideoResult> Video(String Query, String Options, String Market, String Adult,
                                                 Double? Latitude, Double? Longitude, String VideoFilters,
                                                 String VideoSortBy)
      {
         if ((Query == null))
         {
            throw new ArgumentNullException("Query", "Query value cannot be null");
         }
         DataServiceQuery<VideoResult> query = base.CreateQuery<VideoResult>("Video");
         query = query.AddQueryOption("Query", string.Concat("\'", Uri.EscapeDataString(Query), "\'"));
         if ((Options != null))
         {
            query = query.AddQueryOption("Options", string.Concat("\'", Uri.EscapeDataString(Options), "\'"));
         }
         if ((Market != null))
         {
            query = query.AddQueryOption("Market", string.Concat("\'", Uri.EscapeDataString(Market), "\'"));
         }
         if ((Adult != null))
         {
            query = query.AddQueryOption("Adult", string.Concat("\'", Uri.EscapeDataString(Adult), "\'"));
         }
         if (((Latitude != null)))
         {
            query = query.AddQueryOption("Latitude", Latitude.Value);
         }
         if (((Longitude != null)))
         {
            query = query.AddQueryOption("Longitude", Longitude.Value);
         }
         if ((VideoFilters != null))
         {
            query = query.AddQueryOption("VideoFilters", string.Concat("\'", Uri.EscapeDataString(VideoFilters), "\'"));
         }
         if ((VideoSortBy != null))
         {
            query = query.AddQueryOption("VideoSortBy", string.Concat("\'", Uri.EscapeDataString(VideoSortBy), "\'"));
         }
         return query;
      }

      /// <summary>
      /// </summary>
      /// <param name="Query">Bing search query Sample Values : xbox</param>
      /// <param name="Options">Specifies options for this request for all Sources. Valid values are: DisableLocationDetection, EnableHighlighting. Sample Values : EnableHighlighting</param>
      /// <param name="Market">Market. Note: Not all Sources support all markets. Sample Values : en-US</param>
      /// <param name="Adult">Adult setting is used for filtering sexually explicit content Sample Values : Moderate</param>
      /// <param name="Latitude">Latitude Sample Values : 47.603450</param>
      /// <param name="Longitude">Longitude Sample Values : -122.329696</param>
      /// <param name="NewsLocationOverride">Overrides Bing location detection. This parameter is only applicable in en-US market. Sample Values : US.WA</param>
      /// <param name="NewsCategory">The category of news for which to provide results Sample Values : rt_Business</param>
      /// <param name="NewsSortBy">The sort order of results returned Sample Values : Date</param>
      public DataServiceQuery<NewsResult> News(String Query, String Options, String Market, String Adult,
                                               Double? Latitude, Double? Longitude, String NewsLocationOverride,
                                               String NewsCategory, String NewsSortBy)
      {
         if ((Query == null))
         {
            throw new ArgumentNullException("Query", "Query value cannot be null");
         }
         DataServiceQuery<NewsResult> query = base.CreateQuery<NewsResult>("News");
         query = query.AddQueryOption("Query", string.Concat("\'", Uri.EscapeDataString(Query), "\'"));
         if ((Options != null))
         {
            query = query.AddQueryOption("Options", string.Concat("\'", Uri.EscapeDataString(Options), "\'"));
         }
         if ((Market != null))
         {
            query = query.AddQueryOption("Market", string.Concat("\'", Uri.EscapeDataString(Market), "\'"));
         }
         if ((Adult != null))
         {
            query = query.AddQueryOption("Adult", string.Concat("\'", Uri.EscapeDataString(Adult), "\'"));
         }
         if (((Latitude != null)))
         {
            query = query.AddQueryOption("Latitude", Latitude.Value);
         }
         if (((Longitude != null)))
         {
            query = query.AddQueryOption("Longitude", Longitude.Value);
         }
         if ((NewsLocationOverride != null))
         {
            query = query.AddQueryOption("NewsLocationOverride",
                                         string.Concat("\'", Uri.EscapeDataString(NewsLocationOverride), "\'"));
         }
         if ((NewsCategory != null))
         {
            query = query.AddQueryOption("NewsCategory", string.Concat("\'", Uri.EscapeDataString(NewsCategory), "\'"));
         }
         if ((NewsSortBy != null))
         {
            query = query.AddQueryOption("NewsSortBy", string.Concat("\'", Uri.EscapeDataString(NewsSortBy), "\'"));
         }
         return query;
      }

      /// <summary>
      /// </summary>
      /// <param name="Query">Bing search query Sample Values : xbox</param>
      /// <param name="Options">Specifies options for this request for all Sources. Valid values are: DisableLocationDetection, EnableHighlighting. Sample Values : EnableHighlighting</param>
      /// <param name="Market">Market. Note: Not all Sources support all markets. Sample Values : en-US</param>
      /// <param name="Adult">Adult setting is used for filtering sexually explicit content Sample Values : Moderate</param>
      /// <param name="Latitude">Latitude Sample Values : 47.603450</param>
      /// <param name="Longitude">Longitude Sample Values : -122.329696</param>
      public DataServiceQuery<RelatedSearchResult> RelatedSearch(String Query, String Options, String Market,
                                                                 String Adult, Double? Latitude, Double? Longitude)
      {
         if ((Query == null))
         {
            throw new ArgumentNullException("Query", "Query value cannot be null");
         }
         DataServiceQuery<RelatedSearchResult> query = base.CreateQuery<RelatedSearchResult>("RelatedSearch");
         query = query.AddQueryOption("Query", string.Concat("\'", Uri.EscapeDataString(Query), "\'"));
         if ((Options != null))
         {
            query = query.AddQueryOption("Options", string.Concat("\'", Uri.EscapeDataString(Options), "\'"));
         }
         if ((Market != null))
         {
            query = query.AddQueryOption("Market", string.Concat("\'", Uri.EscapeDataString(Market), "\'"));
         }
         if ((Adult != null))
         {
            query = query.AddQueryOption("Adult", string.Concat("\'", Uri.EscapeDataString(Adult), "\'"));
         }
         if (((Latitude != null)))
         {
            query = query.AddQueryOption("Latitude", Latitude.Value);
         }
         if (((Longitude != null)))
         {
            query = query.AddQueryOption("Longitude", Longitude.Value);
         }
         return query;
      }

      /// <summary>
      /// </summary>
      /// <param name="Query">Bing search query Sample Values : xblox</param>
      /// <param name="Options">Specifies options for this request for all Sources. Valid values are: DisableLocationDetection, EnableHighlighting. Sample Values : EnableHighlighting</param>
      /// <param name="Market">Market. Note: Not all Sources support all markets. Sample Values : en-US</param>
      /// <param name="Adult">Adult setting is used for filtering sexually explicit content Sample Values : Moderate</param>
      /// <param name="Latitude">Latitude Sample Values : 47.603450</param>
      /// <param name="Longitude">Longitude Sample Values : -122.329696</param>
      public DataServiceQuery<SpellResult> SpellingSuggestions(String Query, String Options, String Market, String Adult,
                                                               Double? Latitude, Double? Longitude)
      {
         if ((Query == null))
         {
            throw new ArgumentNullException("Query", "Query value cannot be null");
         }
         DataServiceQuery<SpellResult> query = base.CreateQuery<SpellResult>("SpellingSuggestions");
         query = query.AddQueryOption("Query", string.Concat("\'", Uri.EscapeDataString(Query), "\'"));
         if ((Options != null))
         {
            query = query.AddQueryOption("Options", string.Concat("\'", Uri.EscapeDataString(Options), "\'"));
         }
         if ((Market != null))
         {
            query = query.AddQueryOption("Market", string.Concat("\'", Uri.EscapeDataString(Market), "\'"));
         }
         if ((Adult != null))
         {
            query = query.AddQueryOption("Adult", string.Concat("\'", Uri.EscapeDataString(Adult), "\'"));
         }
         if (((Latitude != null)))
         {
            query = query.AddQueryOption("Latitude", Latitude.Value);
         }
         if (((Longitude != null)))
         {
            query = query.AddQueryOption("Longitude", Longitude.Value);
         }
         return query;
      }
   }
}