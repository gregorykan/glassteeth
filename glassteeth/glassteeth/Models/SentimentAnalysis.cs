using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json.Linq;

namespace glassteeth.Models
{
    public class SentimentAnalysis
    {
        public string Analyze(string input)
        {
            string sentiment = "";
            string stripped = Strip(input);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://twinword-sentiment-analysis.p.mashape.com/analyze/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Mashape-Key", "N4MPgxvq15mshZ8tt68Jq3JN6lsfp1mDN9OjsniaiLvFJ4cDAZ");
                HttpResponseMessage response = client.GetAsync("?text=" + stripped).Result;
                if (response.IsSuccessStatusCode)
                {
                    JObject queryResult = response.Content.ReadAsAsync<JObject>().Result;
                    sentiment = queryResult["type"].ToString();
                    return sentiment;
                }
            }
            return sentiment;
        }

        public string Strip(string text)
        {
            string tweetText = text;
            string[] tweetTextSplit = tweetText.Split(
                new char[] { '.', '?', '!', ' ', ';', ':', ',', '/', '\\', '@', '#', '_', '-', '\'', '&', '"' }, StringSplitOptions.RemoveEmptyEntries);
            string tweetTextWithPluses = "";

            for (int i = 0; i < tweetTextSplit.Length; i++)
            {
                if (i < tweetTextSplit.Length - 1)
                {
                    tweetTextWithPluses += tweetTextSplit[i] + "+";
                }
                else
                {
                    tweetTextWithPluses += tweetTextSplit[i];
                }
            }
            return tweetTextWithPluses;
        }
    }
}