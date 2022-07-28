using DalSoft.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBot.Utils
{
    public class RamApiv
    {
       

        private static string APIURL = $"https://api.rambot.xyz/public/version/{BotConfig.RAMAPIV}";

        public static async Task<dynamic> Run()
        {
            string requestUrl = APIURL;


            var client = new RestClient(requestUrl, new Config()
                .UseRetryHandler(
                    maxRetries: 5,
                    waitToRetryInSeconds: 6,
                    maxWaitToRetryInSeconds: 10,
                    backOffStrategy: DalSoft.RestClient.Handlers.RetryHandler.BackOffStrategy.Linear
                ));

            return await client.Get();
        }

    }
}
