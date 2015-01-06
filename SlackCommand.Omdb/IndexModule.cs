namespace SlackCommand.Omdb
{
    using Nancy;
    using Nancy.ModelBinding;
    using Newtonsoft.Json;
    using System.IO;

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

            Get["/imdb/{imdbid}.jpg"] = parameters =>
            {
                var client = new RestSharp.RestClient("http://img.omdbapi.com");
                var request = new RestSharp.RestRequest(RestSharp.Method.GET).AddQueryParameter("apikey", "cce8fe13").AddQueryParameter("i", parameters["imdbid"]);
                var responseData = client.DownloadData(request);
                var r = new Response();
                r.Contents = s => {
                    using (var writer = new BinaryWriter(s))
                    {
                        writer.Write(responseData);
                    };
                };
                r.ContentType = "image/jpeg";

                return r;
            };
        }

    }
}