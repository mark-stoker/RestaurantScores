﻿using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using RestaurantScores.Managers.Interfaces;

namespace RestaurantScores.Managers
{
	public class RecaptchaManager : IRecaptchaManager
	{
		public bool ReCaptchaPassed(string gRecaptchaResponse, string secret)
		{
			HttpClient httpClient = new HttpClient();
			var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
			if (res.StatusCode != HttpStatusCode.OK)
			{
				return false;
			}

			string JSONres = res.Content.ReadAsStringAsync().Result;
			dynamic JSONdata = JObject.Parse(JSONres);
			if (JSONdata.success != "true")
			{
				return false;
			}

			return true;
		}
	}
}
