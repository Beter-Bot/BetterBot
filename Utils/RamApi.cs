using DalSoft.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBot.Utils
{
    public class RamApi
    {
        private static string APIKEY = BotConfig.APIKEY;

        private static string APIURL = $"https://api.rambot.xyz/{BotConfig.RAMAPIV}/public/";

        public static async Task<dynamic> Run(string url, string lang = "false")
        {
            string requestUrl = APIURL + url;

            if (lang != "false") requestUrl += $"/{lang}";

            dynamic client = new RestClient(requestUrl, new Config()
                .UseRetryHandler(
                    maxRetries: 5,
                    waitToRetryInSeconds: 6,
                    maxWaitToRetryInSeconds: 10,
                    backOffStrategy: DalSoft.RestClient.Handlers.RetryHandler.BackOffStrategy.Linear
                ));

            return await client.Headers(new Dictionary<string, string> { { "api-key", APIKEY } }).Get();


        }

    }
}
