using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlackCommand.Omdb.Model
{
    public class SlackWebhookResponse
    {
        public SlackWebhookPayload payload { get; set; }

        public SlackWebhookResponse()
        {
            this.payload = new SlackWebhookPayload();
        }

        public SlackWebhookResponse FromOmdbTitleAsAttachment(OmdbTitle omdbTitle, string webhookUsername="Imdb")
        {
            var imdbTitle = FormatImdbTitle(omdbTitle.Title, omdbTitle.imdbId, omdbTitle.Year);
            var imdbText = FormatImdbText(omdbTitle.Plot, omdbTitle.imdbRating, omdbTitle.Poster, omdbTitle.Director);

            var response = new SlackWebhookResponse();

            var attachment = new SlackWebhookResponseAttachment()
            {
                fallback = imdbTitle,
                pretext = imdbTitle,
                color = "#FACC2E"
            };

            var fields = new SlackWebhookResponseAttachmentFields()
            {
                title = omdbTitle.Title,
                value = imdbText
            };

            attachment.fields.Add(fields);
            payload.attachments.Add(attachment);
            response.payload = payload;
            response.payload.username = webhookUsername;
            response.payload.text = "<" + FormatImdbPoster(omdbTitle.imdbId) + ">";

            return response;
        }

        public string FormatImdbTitle(string title, string imdbId, string year)
        {
            var posterUrl = FormatImdbPoster(imdbId);
            //var result = string.Format("<{3}>\n<http://www.imdb.com/title/{0}|{1}> ({2})", imdbId, title, year, posterUrl);
            var result = string.Format("<{3}>\n{1} ({2})", imdbId, title, year, posterUrl);
            return result;
        }

        public string FormatImdbText(string plot, string rating, string poster, string director)
        {
            var result = string.Format("{0}\nRating: {1}\nDirector: {2}", plot, rating, director);
            return result;
        }

        public string FormatImdbPoster(string imdbId)
        {
            var apiKey = "cce8fe13";
            var result = string.Format("http://img.omdbapi.com/?apikey={0}&i={1}",apiKey, imdbId);
            return result;
        }

        public SlackWebhookResponse FromOmdbSearchResult(OmdbSearchResultList searchResult)
        {
            var result = new SlackWebhookResponse(); ;
            return result;
        }

        public SlackWebhookResponse EmptyResultResponse(string searchTerm , string channel, string userName, string webhookUsername = "Imdb")
        {
            var response = new SlackWebhookResponse();
            response.payload.username = webhookUsername;
            response.payload.text = string.Format("Psssst @{0} I couldn't find a title named \"{1}\".", userName, searchTerm);
            response.payload.channel = string.Format("#{0}", channel);

            return response;
        }

        public SlackWebhookResponse FromOmdbTitle(OmdbTitle omdbTitle, string webhookUsername = "Imdb")
        {
            var imdbTitle = FormatImdbTitle(omdbTitle.Title, omdbTitle.imdbId, omdbTitle.Year);
            var imdbText = FormatImdbText(omdbTitle.Plot, omdbTitle.imdbRating, omdbTitle.Poster, omdbTitle.Director);
            var posterUrl = FormatImdbPoster(omdbTitle.imdbId);

            var response = new SlackWebhookResponse();

            response.payload.username = webhookUsername;
            response.payload.text = string.Format("{0}\n{1}\n{2}", imdbTitle, imdbText, posterUrl);

            return response;
        }
    }
    public class SlackWebhookPayload
    {
        public string channel { get; set; }
        public string username { get; set; }
        public string text { get; set; }
        public bool unfurl_links { get; set; }
        public bool unfurl_media { get; set; }
        public List<SlackWebhookResponseAttachment> attachments  { get; set; }

        public SlackWebhookPayload()
        {
            this.attachments = new List<SlackWebhookResponseAttachment>();
            this.unfurl_links = true;
            this.unfurl_media = false;
        }
        //curl -X POST --data-urlencode 'payload={"channel": "#debug", "username": "webhookbot", "text": "This is posted to #debug and comes from a bot named webhookbot.", "icon_emoji": ":ghost:"}' https://hooks.slack.com/services/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
    }

    public class SlackWebhookResponseAttachment
    {
        public string fallback { get; set; }
        public string pretext { get; set; }
        public string color { get; set; }
        public List<SlackWebhookResponseAttachmentFields> fields { get; set; }

        public SlackWebhookResponseAttachment()
        {
            this.fields = new List<SlackWebhookResponseAttachmentFields>();
        }

    }

    public class SlackWebhookResponseAttachmentFields
    {
        public string title { get; set; }
        public string value { get; set; }
    }
}

/*
{
   "attachments":[
      {
         "fallback":"New open task [Urgent]: <http://url_to_task|Test out Slack message attachments>",
         "pretext":"New open task [Urgent]: <http://url_to_task|Test out Slack message attachments>",
         "color":"#D00000",
         "fields":[
            {
               "title":"Notes",
               "value":"This is much easier than I thought it would be.",
               "short":false
            }
         ]
      }
   ]
}		
*/