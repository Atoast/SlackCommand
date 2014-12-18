using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlackCommand.Omdb.Commands
{
    public class SearchSingleResultCommand
    {
        public Model.SlackWebhookResponse Execute(string searchTerm, string channel, string userName)
        {
            var omdb = new Omdb.Integration.OmdbQuery();
            var response = new Model.SlackWebhookResponse();
            var result = omdb.SearchSingleResult(searchTerm);

            var foundResult = string.IsNullOrEmpty(result.Title) ? false : true;
            if (foundResult)
            {
                response = new Model.SlackWebhookResponse().FromOmdbTitle(result);
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