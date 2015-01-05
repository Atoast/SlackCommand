using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlackCommand.Omdb.Integration;

namespace SlackCommand.Omdb.Tests
{
    [TestClass]
    public class Basic
    {
        [TestMethod]
        [TestCategory("SearchResults")]
        public void SearchWithResult()
        {
            var searchTerm = "Warehouse";
            var omdb = new OmdbQuery();
            var result = omdb.Search(searchTerm);
            var containsSearchResults = result.Search.Count > 0;
            Assert.IsTrue(containsSearchResults);
        }

        [TestMethod]
        [TestCategory("SearchResults")]
        public void SearchWithNoResult()
        {
            var searchTerm = "ZZZZZXXXXXXXYYYYY";
            var omdb = new OmdbQuery();
            var result = omdb.Search(searchTerm);
            var noSearchResults = result.Search.Count == 0;
            Assert.IsTrue(noSearchResults);
        }

        [TestMethod]
        [TestCategory("SearchResults")]
        public void TitleSearchWithResult()
        {
            var title = "Warehouse 13";
            var omdb = new OmdbQuery();
            var result = omdb.SearchSingleResult(title);
            var foundTitle = string.IsNullOrEmpty(result.Title) ? false : true;
            Assert.IsTrue(foundTitle);
        }

        [TestMethod]
        [TestCategory("SearchResults")]
        public void TitleSearchWithNoResult()
        {
            var title = "Warehouse 13121212121212";
            var omdb = new OmdbQuery();
            var result = omdb.SearchSingleResult(title);
            var foundTitle = string.IsNullOrEmpty(result.Title) ? false : true;
            Assert.IsFalse(foundTitle);
        }


        [TestMethod]
        [TestCategory("WebhookResponses")]
        public void WebhookResponseNoResults()
        {
            var searchTerm = "WebhookResponse No Result";
            var channel = "WebhookTestChannel";
            var userName = "Casper";
            var responseText = string.Format("Psssst @{0} I couldn't find a title named \"{1}\".", userName, searchTerm);

            var response = new Model.SlackWebhookResponse().EmptyResultResponse(searchTerm, channel, userName);
            var hasCorrectResponse = response.payload.text == responseText;
            Assert.IsTrue(hasCorrectResponse);
        }

        [TestMethod]
        [TestCategory("WebhookResponses")]
        public void WebhookResponseWithResult()
        {
            var searchTerm = "The 100";
            var channel = "WebhookTestChannel";
            var userName = "Casper";
            var responseText = string.Format("Psssst @{0} I couldn't find a title named \"{1}\".", userName, searchTerm);
            var omdbSearch = new OmdbQuery();
            var searchResult = omdbSearch.SearchSingleResult(searchTerm);
            var imdbId = searchResult.imdbId;
            var response = new Model.SlackWebhookResponse().FromOmdbTitle(searchResult);
            var hasImdbId = response.payload.attachments[0].pretext.Contains(imdbId);
            Assert.IsTrue(hasImdbId);
        }
    }
}
