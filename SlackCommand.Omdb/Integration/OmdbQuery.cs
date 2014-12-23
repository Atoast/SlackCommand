using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SlackCommand.Omdb.Model;
using RestSharp;
using Newtonsoft.Json;

namespace SlackCommand.Omdb.Integration
{
    public class OmdbQuery
    {
        private const string OMDB_URL = "http://www.omdbapi.com/";
        public OmdbSearchResultList Search(string searchTerm)
        {
            var client = new RestClient(OMDB_URL);
            var result = new OmdbSearchResultList();

            var request = GetSearchRequest(searchTerm);
            
            IRestResponse response = client.Execute(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<Model.OmdbSearchResultList>(response.Content) ?? result;
            }

            return result;
        }

        public OmdbTitle SearchSingleResult(string title)
        {
            var client = new RestClient(OMDB_URL);
            var result = new OmdbTitle();

            var request = GetTitleRequest(title);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = JsonConvert.DeserializeObject<Model.OmdbTitle>(response.Content) ?? result;
            }

            return result;
        }

        public IRestRequest GetSearchRequest(string searchTerm)
        {
            var searchTermParam = "s";
            var plotParam = "plot";
            var formatParam = "r";

            var encodedSearchTerm = HttpUtility.UrlEncode(searchTerm);
            var format = "json";
            var plot = "short";

            var request = new RestRequest(Method.GET)
                                .AddQueryParameter(searchTermParam, encodedSearchTerm)
                                .AddQueryParameter(plotParam, plot)
                                .AddQueryParameter(formatParam, format);
            return request;
        }

        public OmdbCommand ParseCommand(string text)
        {
            throw new NotImplementedException();
        }

        public IRestRequest GetTitleRequest(string title)
        {
            var titleParam = "t";
            var plotParam = "plot";
            var formatParam = "r";

            var encodedSearchTerm = HttpUtility.UrlEncode(title);
            var format = "json";
            var plot = "short";

            var request = new RestRequest(Method.GET)
                                .AddQueryParameter(titleParam, encodedSearchTerm)
                                .AddQueryParameter(plotParam, plot)
                                .AddQueryParameter(formatParam, format);
            return request;
        }
    }
}