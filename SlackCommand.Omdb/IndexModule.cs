namespace SlackCommand.Omdb
{
    using Nancy;
    using Nancy.ModelBinding;
    using Newtonsoft.Json;

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

            Post["/imdb"] = parameters =>
            {
                var slackCommand = new Model.SlackCommandRequest();
                slackCommand = this.Bind<Model.SlackCommandRequest>();

                var searchTerm = slackCommand.text.Trim();
                var isListSearch = searchTerm.StartsWith("?");
                var response = new Model.SlackWebhookResponse();

                if(isListSearch)
                {
                    
                }
                else
                {
                    var singleSearchResultCmd = new Commands.SearchSingleResultCommand();
                    response = singleSearchResultCmd.Execute(searchTerm, slackCommand.channel_name, slackCommand.user_name);
                }

                var slackResponder = new Integration.SlackWebhookResponder();
                slackResponder.Send(response);

                var jsonResponse = JsonConvert.SerializeObject(response);
                return jsonResponse;
            };
        }
    }
}