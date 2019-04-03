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
		//TODO need to set up online tool for secrets
		private static readonly string accessKey = Environment.GetEnvironmentVariable("BingWebSearchApi");
		const string uriBase = "https://api.cognitive.microsoft.com/bing/v7.0/search";

		public List<Review> BingWebSearch(string searchQuery)
		{
			var uriQuery = uriBase + "?q=" + Uri.EscapeDataString(searchQuery) + "&count=200";
			HttpWebResponse response = SearchBing(uriQuery);
			List<Review> restaurants = ParseJsonWebResponse(response);

			return restaurants;
		}

		private static List<Review> ParseJsonWebResponse(HttpWebResponse response)
		{
			string json = new StreamReader(response.GetResponseStream()).ReadToEnd();

			JObject joResponse = JObject.Parse(json);
			JObject ojObject = (JObject)joResponse["webPages"];
			JArray array = (JArray)ojObject["value"];

			List<Review> Restaurants = array.Select(x => new Review
			{
				Name = (string)x["name"],
				Url = (string)x["url"],
			}).GroupBy(r => r.Url.Substring(0,20)).Select(d => d.First()).ToList();

			return Restaurants;
		}

		private static HttpWebResponse SearchBing(string uriQuery)
		{
			WebRequest request = HttpWebRequest.Create(uriQuery);
			request.Headers["Ocp-Apim-Subscription-Key"] = accessKey;
			HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

			return response;
		}
	}


}
