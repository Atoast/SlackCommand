namespace SlackCommand.Omdb.Model
{
    public class OmdbCommand
    {
        public string Raw { get; set; }
        public string SearchTerm { get; set; }
        public OmdbType Type { get; set; }

        public OmdbCommand(string command)
        {

        }

        public void ParseCommand(string command)
        {
            var containsSearchParameter = command.Trim().StartsWith("?");
            if (containsSearchParameter)
            {
                var isMovieSearch = command.ToLower().StartsWith("?movie");
                if(isMovieSearch)
                {
                    this.Type = OmdbType.Movie;
                }

                var isTvSearch = command.ToLower().StartsWith("?tv");
                if()
            }


        }

        public enum OmdbType
        {
            Tv,
            Movie
        }
    }
}