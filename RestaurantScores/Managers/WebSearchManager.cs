using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using RestaurantScores.Managers.Interfaces;
using RestaurantScores.Models;

namespace RestaurantScores.Managers
{
	public class WebSearchManager : IWebSearchManager
	{
		//Note
		private static readonly string accessKey = Environment.GetEnvironmentVariable("BingWebSearchApi");
		const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";

		public List<Review> BingWebSearch(string searchQuery)
		{
			var uriQuery1 = uriBase + "?q=" + Uri.EscapeDataString(searchQuery) + "+restaurant+review+" + "&count=50&offset=0&mkt=en-GB";
			var uriQuery2 = uriBase + "?q=" + Uri.EscapeDataString(searchQuery) + "+restaurant+review" + "&count=50&offset=50&mkt=en-GB";
			var uriQuery3 = uriBase + "?q=" + Uri.EscapeDataString(searchQuery) + "restaurant+review+" + "&count=50&offset=100&mkt=en-GB";
			var uriQuery4 = uriBase + "?q=" + Uri.EscapeDataString(searchQuery) + "restaurant+review+" + "&count=50&offset=150&mkt=en-GB";
			HttpWebResponse response1 = SearchBing(uriQuery1);
			HttpWebResponse response2 = SearchBing(uriQuery2);
			HttpWebResponse response3 = SearchBing(uriQuery3);
			HttpWebResponse response4 = SearchBing(uriQuery4);

			List<Review> restaurants1 = ParseJsonWebResponse(response1);
			List<Review> restaurants2 = ParseJsonWebResponse(response2);
			List<Review> restaurants3 = ParseJsonWebResponse(response3);
			List<Review> restaurants4 = ParseJsonWebResponse(response4);

			var allRestaurantReviews = restaurants1.Concat(restaurants2)
				.Concat(restaurants3)
				.Concat(restaurants4)
				.ToList();

			return allRestaurantReviews;
		}

		private static List<Review> ParseJsonWebResponse(HttpWebResponse response)
		{
			string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

			var joResponse = JObject.Parse(json);
			var ojObject = (JObject)joResponse["webPages"];
			var array = (JArray)ojObject["value"];

			List<Review> Restaurants = array.Select(x => new Review
			{
				Name = (string)x["name"],
				Url = (string)x["url"],
			}).GroupBy(r => r.Url.Substring(0,18)).Select(d => d.First()).ToList();

			return Restaurants;
		}

		private static HttpWebResponse SearchBing(string uriQuery)
		{
			var request = WebRequest.Create(uriQuery);
			request.Headers["Ocp-Apim-Subscription-Key"] = accessKey;
			var response = (HttpWebResponse)request.GetResponseAsync().Result;

			return response;
		}
	}
}
