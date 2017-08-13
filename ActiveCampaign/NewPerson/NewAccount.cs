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
    public static class NewAccount
    {
        [FunctionName("NewAccount")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            HttpClient client = DynamicsCRMUtility.GetCRMHttpClient();

            var httpMessageResponse = await client.PostAsJsonAsync(DynamicsCRMUtility.Account, GetAccount());
            if (!httpMessageResponse.IsSuccessStatusCode)
                return req.CreateResponse(HttpStatusCode.InternalServerError, httpMessageResponse.Content);
            string result = await httpMessageResponse.Content.ReadAsStringAsync();

            return req.CreateResponse(HttpStatusCode.OK, result, "application/json");
        }

        private static Account GetAccount()
        {
            return new Account
            {
                name = "Prova",
                description = "account di prova",
                creditonhold = false,
                address1_latitude = 53.2d,
                revenue = 1,
                accountcategorycode = 1
            };
        }
        protected class Account
        {
            public string name { get; set; }
            public bool creditonhold { get; set; }
            public double address1_latitude { get; set; }
            public string description { get; set; }
            public int revenue { get; set; }
            public int accountcategorycode { get; set; }         
        }
       
    }
}