using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlackCommand.Omdb.Commands
{
    public class SearchResultCommand
    {
        public Model.SlackWebhookResponse Execute(string searchTerm, string channel, string userName)
        {
            var omdb = new Omdb.Integration.OmdbQuery();
            var response = new Model.SlackWebhookResponse();
            var results = omdb.Search(searchTerm);

            var foundResult = results.Search.Count > 0;
            if (foundResult)
            {
                response = new Model.SlackWebhookResponse().FromOmdbSearchResult(results);
                response.payload.channel = "#" + channel;
            }
            else
            {
                var searchResults = omdb.Search(searchTerm);
                response = new Model.SlackWebhookResponse().EmptyResultResponse(searchTerm, channel, userName);
            }

            return response;
        }
    }
}