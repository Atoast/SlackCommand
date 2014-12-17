using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlackCommand.Omdb.Model
{
    [Serializable]
    public class SlackCommandRequest
    {
        public string token { get; set; }
        public string team_id { get; set; }
        public string channel_id { get; set; }
        public string channel_name { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string command { get; set; }
        public string text { get; set; }
    }

    public class SlackCommandResponse
    {
        public string Text { get; set; }

        public string FromOmdbTitle(OmdbTitle omdbTitle)
        {
            var imdbLink = string.Format("http://www.imdb.com/title/{0}", omdbTitle.imdbId);
            return string.Format(
        @"*{0}* ({1}) - rating {2} 
        {3}
        {4}", omdbTitle.Title, omdbTitle.Year, omdbTitle.imdbRating, omdbTitle.Plot, imdbLink);
        }
    }
}