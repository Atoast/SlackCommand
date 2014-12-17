namespace SlackCommand.Omdb
{
    using Nancy;

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
            Post["/imdb/debug"] = parameters =>
            {
                return Request.Body;
            };
        }
    }
}