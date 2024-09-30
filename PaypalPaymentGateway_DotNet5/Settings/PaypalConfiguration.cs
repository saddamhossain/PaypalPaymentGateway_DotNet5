using PayPal.Api;
using System.Collections.Generic;

namespace PaypalPaymentGateway_DotNet5.Settings
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;

        static PaypalConfiguration()
        {
            var config = getConfig();
            clientId = "AVVUoP76ciO6pjFlVf8e1LQjlvoBdR0rnCEgcXX6tAD9_P8TwcJ1zpIkwRJlocCExqTzkdGmj02giWxV";
            clientSecret = "EDp7IAoGbtJfZa4FNNy-ubVcntOJUbsz53jxw9WWMegYnhB3Nieu4VtAVY4ct_wPYsAAu4CmEVxCwHkc";
        }

        public static Dictionary<string, string> getConfig()
        {
            return ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            // Authenticate with PayPal
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = getConfig();
            return apiContext;
        }

    }
}
