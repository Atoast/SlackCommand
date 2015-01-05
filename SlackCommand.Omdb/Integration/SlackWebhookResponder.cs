using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace SlackCommand.Omdb.Integration
{
    public class SlackWebhookResponder
    {
        private const string WEBHOOKID = "services/T02FQR5EX/B036Y8N0Q/qdYKxXGqjdiidTdFmelSxOhY";
        public HttpStatusCode Send(Model.SlackWebhookResponse webhookResponse)
        {
            var url = "https://hooks.slack.com/";
            var client = new RestClient(url);
            var postRequest = new RestRequest(WEBHOOKID, Method.POST);
            var jsonBody = JsonConvert.SerializeObject(webhookResponse.payload);
            postRequest.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

            IRestResponse r = client.Execute(postRequest);
            return r.StatusCode;
        }
    }
}