using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;


namespace SlackCommand.Omdb.Tests
{
    [TestClass]
    public class Web
    {
        [TestMethod]
        public void DebugRoute()
        {
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);


            var requestBody = "{\"token\":\"fWLgDuiSNnxt2RYDhljCTbs0\",\"team_id\":\"T02FQR5EX\",\"channel_id\":\"C036W3BN3\",\"channel_name\":\"debug\",\"user_id\":\"U02NAKD4K\",\"user_name\":\"casperandersen\",\"command\":\" / imdb\",\"text\":\"test\"}";
            var slackCommand = new Model.SlackCommandRequest()
            {
                token = "fWLgDuiSNnxt2RYDhljCTbs0",
                team_id = "T02FQR5EX",
                channel_id = "C036W3BN3",
                channel_name = "debug",
                user_id = "U02NAKD4K",
                user_name = "casperandersen",
                command = "/imdb",
                text = "test"
            };

            // When
            var result = browser.Post("/imdb/debug", with => {
                with.HttpRequest();
                with.JsonBody<Model.SlackCommandRequest>(slackCommand);
                with.Body(requestBody);
            });

            // Then
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }


        [TestMethod]
        public void WebhookResponse()
        {
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);


            var requestBody = "{\"token\":\"fWLgDuiSNnxt2RYDhljCTbs0\",\"team_id\":\"T02FQR5EX\",\"channel_id\":\"C036W3BN3\",\"channel_name\":\"debug\",\"user_id\":\"U02NAKD4K\",\"user_name\":\"casperandersen\",\"command\":\" / imdb\",\"text\":\"test\"}";
            var slackCommand = new Model.SlackCommandRequest()
            {
                token = "fWLgDuiSNnxt2RYDhljCTbs0",
                team_id = "T02FQR5EX",
                channel_id = "C036W3BN3",
                channel_name = "debug",
                user_id = "U02NAKD4K",
                user_name = "casperandersen",
                command = "/imdb",
                text = "test"
            };

            // When
            var result = browser.Post("/imdb", with => {
                with.HttpRequest();
                with.JsonBody<Model.SlackCommandRequest>(slackCommand);
                with.Body(requestBody);
            });

            // Then
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void WebhookSearchResultResponse()
        {
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);

            var slackCommand = new Model.SlackCommandRequest()
            {
                token = "fWLgDuiSNnxt2RYDhljCTbs0",
                team_id = "T02FQR5EX",
                channel_id = "C036W3BN3",
                channel_name = "debug",
                user_id = "U02NAKD4K",
                user_name = "casperandersen",
                command = "/imdb",
                text = "test"
            };

            // When
            var result = browser.Post("/imdb", with => {
                with.HttpRequest();
                with.JsonBody<Model.SlackCommandRequest>(slackCommand);
            });

            // Then
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void WebhookNoSearchResultResponse()
        {
            var bootstrapper = new DefaultNancyBootstrapper();
            var browser = new Browser(bootstrapper);

            var searchTerm = "XXYYBBBOOOO";
            var slackCommand = new Model.SlackCommandRequest()
            {
                token = "fWLgDuiSNnxt2RYDhljCTbs0",
                team_id = "T02FQR5EX",
                channel_id = "C036W3BN3",
                channel_name = "debug",
                user_id = "U02NAKD4K",
                user_name = "casperandersen",
                command = "/imdb",
                text = searchTerm
            };

            // When
            var result = browser.Post("/imdb", with => {
                with.HttpRequest();
                with.JsonBody<Model.SlackCommandRequest>(slackCommand);
            });

            // Then
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

    }
}
