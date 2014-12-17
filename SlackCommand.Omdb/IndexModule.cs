namespace SlackCommand.Omdb
{
    using Nancy;
    using Nancy.ModelBinding;
    using RestSharp;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };
        }
    }

    public class OmdbModule : NancyModule
    {
        public OmdbModule()
        {
            var omdb = new Omdb.Integration.OmdbQuery();

            Post["/imdb/debug"] = parameters =>
            {
                var slackCommand = new Model.SlackCommandRequest();
                slackCommand = this.Bind<Model.SlackCommandRequest>();

                var result = omdb.SearchSingleResult(slackCommand.text);

                var response = new Model.SlackCommandResponse().FromOmdbTitle(result);

                return response;
            };

            Post["/imdb"] = parameters =>
            {
                var slackCommand = new Model.SlackCommandRequest();
                slackCommand = this.Bind<Model.SlackCommandRequest>();
                
                var result = omdb.SearchSingleResult(slackCommand.text);
                var response = new Model.SlackWebhookResponse().FromOmdbTitle(result);
                response.payload.channel = "#" + slackCommand.channel_name;

                var slackResponder = new Integration.SlackWebhookResponder();
                slackResponder.Send(response);

                return "";
            };
        }
    }
}