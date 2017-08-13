using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ActiveCampaign.WhoAmI
{
    public static class WhoAmI
    {
        [FunctionName("WhoAmI")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            HttpClient client = DynamicsCRMUtility.GetCRMHttpClient();

            var httpMessageResponse = await client.GetAsync(DynamicsCRMUtility.WhoAmI, HttpCompletionOption.ResponseHeadersRead);
            if (!httpMessageResponse.IsSuccessStatusCode)
                return req.CreateResponse(HttpStatusCode.InternalServerError, httpMessageResponse.Content);
            string result = await httpMessageResponse.Content.ReadAsStringAsync();

            return req.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }
    }
}
