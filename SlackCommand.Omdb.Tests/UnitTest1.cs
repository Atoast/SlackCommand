using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlackCommand.Omdb;

namespace SlackCommand.Omdb.Tests
{
    [TestClass]
    public class Basic
    {
        [TestMethod]
        public void SearchWithResult()
        {
            var searchTerm = "Warehouse";
            var omdb = new OmdbIntegration();
            var result = omdb.Search(searchTerm);
            var containsSearchResults = result.Search.Count > 0;
            Assert.IsTrue(containsSearchResults);
        }

        [TestMethod]
        public void SearchWithNoResult()
        {
            var searchTerm = "ZZZZZXXXXXXXYYYYY";
            var omdb = new OmdbIntegration();
            var result = omdb.Search(searchTerm);
            var noSearchResults = result.Search.Count == 0;
            Assert.IsTrue(noSearchResults);
        }

        [TestMethod]
        public void TitleSearchWithResult()
        {
            var title = "Warehouse 13";
            var omdb = new OmdbIntegration();
            var result = omdb.SearchSingleResult(title);
            var foundTitle = string.IsNullOrEmpty(result.Title) ? false : true;
            Assert.IsTrue(foundTitle);
        }

        [TestMethod]
        public void TitleSearchWithNoResult()
        {
            var title = "Warehouse 13121212121212";
            var omdb = new OmdbIntegration();
            var result = omdb.SearchSingleResult(title);
            var foundTitle = string.IsNullOrEmpty(result.Title) ? false : true;
            Assert.IsFalse(foundTitle);
        }

    }
}
