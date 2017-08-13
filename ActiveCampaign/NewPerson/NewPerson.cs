using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;

namespace ActiveCampaign.NewPerson
{
    public static class NewPerson
    {
        [FunctionName("NewPerson")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            HttpClient client = GetNewHttpClient();
            //client.DefaultRequestHeaders.Add("Accept", "application/json");
            //client.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            //client.DefaultRequestHeaders.Add("OData-Version", "4.0");

            var httpMessageResponse = await client.GetAsync("AlturasTestCRM/api/data/v8.2/WhoAmI", HttpCompletionOption.ResponseHeadersRead);
            if (!httpMessageResponse.IsSuccessStatusCode)
                return req.CreateResponse(HttpStatusCode.InternalServerError, httpMessageResponse.Content);
            string result = await httpMessageResponse.Content.ReadAsStringAsync();

            return req.CreateResponse(HttpStatusCode.OK);
        }

        private static HttpClient GetNewHttpClient()
        {
            NetworkCredential credential = new NetworkCredential("alturas", "AlturasS1stemi", "crm.loc");

            HttpClient client = new HttpClient(new HttpClientHandler() { Credentials = credential });
            client.BaseAddress = new Uri("http://23.101.69.192:5555/");
            client.Timeout = new TimeSpan(0, 2, 0);
            return client;
        }
    }
}