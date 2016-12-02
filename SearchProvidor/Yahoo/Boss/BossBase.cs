using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using OAuth;

namespace WebMining.SearchProvidor.Yahoo.Boss
{
   public abstract class BossBase
   {
      protected readonly string _baseUrl;
      private readonly string _consumerKey;
      private readonly string _consumerSecret;

      protected BossBase(string serviceName, string consumerKey, string consumerSecret)
      {
         _baseUrl = string.Format("http://yboss.yahooapis.com/ysearch/{0}?format=json", serviceName);
         _consumerKey = consumerKey;
         _consumerSecret = consumerSecret;
      }

      private readonly OAuthBase _oAuth = new OAuthBase();

      protected WebRequest CreateRequest(Uri uri)
      {
         string url, param;
         var nonce = _oAuth.GenerateNonce();
         var timeStamp = _oAuth.GenerateTimeStamp();
         var signature = _oAuth.GenerateSignature(uri, _consumerKey, _consumerSecret,
            string.Empty, string.Empty, "GET", timeStamp, nonce, OAuthBase.SignatureTypes.HMACSHA1,
            out url, out param);

         var request = WebRequest.Create(string.Format("{0}?{1}&oauth_signature={2}", url, param, signature));
         return request;
      }

      private static Dictionary<char, string> EscapeValues = new Dictionary<char, string>()
         {
            {'/', "%2F"},
            {'?', "%3F"},
            {'&', "%26"},
            {';', "%3B"},
            {':', "%3A"},
            {'@', "%40"},
            {',', "%2C"},
            {'$', "%24"},
            {'=', "%3D"},
            {' ', "%20"},
            {'%', "%25"},
            {'"', "%22"},
            {'+', "%2B"},
            {'#', "%23"},
            {'*', "%2A"},
            {'<', "%3C"},
            {'>', "%3E"},
            {'{', "%7B"},
            {'}', "%7D"},
            {'|', "%7C"},
            {'[', "%5B"},
            {']', "%5D"},
            {'^', "%5E"},
            {'\\', "%5C"},
            {'`', "%60"},
            {'(', "%28"},
            {')', "%29"}
         };


      protected static string EscapeText(string input)
      {
         var output = new StringBuilder(input.Length*3);
         foreach (var c in input)
         {
            string escapeValue;
            if (EscapeValues.TryGetValue(c, out escapeValue))
            {
               output.Append(escapeValue);
            }
            else
            {
               output.Append(c);
            }
         }

         return output.ToString();
      }
   }

   public enum FileType
   {
      // ReSharper disable InconsistentNaming         
      html,
      text,
      pdf,
      xl, // Microsoft Excel: xls, xla, xl
      msword, // Microsoft Word
      ppt, // Microsoft Power Point
      msoffice, // xl, msword, ppt
      nonhtml, // text, pdf, xl, msword, ppt
      // ReSharper restore InconsistentNaming
   }

   public enum Language
   {
      //todo: ca, zh-hans, zh-hant, cs, da, nl, en, fi, fr, de, he, hu, it, id, ja, ko, ms, no, pt, ro, ru, es, sv, tl, th, tr, vi, unknown
   }
}