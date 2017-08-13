using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCampaign
{
    public static class DynamicsCRMUtility
    {
        public static HttpClient GetCRMHttpClient()
        {
                NetworkCredential credential = new NetworkCredential("alturas", "AlturasS1stemi", "crm.loc");

                HttpClient client = new HttpClient(new HttpClientHandler() { Credentials = credential });
                client.BaseAddress = new Uri("http://alturascrm.westeurope.cloudapp.azure.com:5555/");
                client.Timeout = new TimeSpan(0, 2, 0);
                return client;
        }

        private const string baseUrl = "AlturasTestCRM/api/data/v8.0/";
        public const string WhoAmI = baseUrl + "WhoAmI()";
        public const string Account = baseUrl + "accounts";
        public static string GetAccount(string accountId) => Account + (accountId);
    }
}
