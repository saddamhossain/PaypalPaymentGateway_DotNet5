using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using PaypalPaymentGateway_DotNet5.Settings;
using System.Linq;

namespace PaypalPaymentGateway_DotNet5.Controllers
{
    public class WebExperienceProfilesController : Controller
    {
        public ActionResult Index()
        {
            var apiContext = PaypalConfiguration.GetAPIContext();

            var list = WebProfile.GetList(apiContext);

            if (!list.Any())
            {
                SeedWebProfiles(apiContext);
                list = WebProfile.GetList(apiContext);
            }

            return View(list);
        }

        private void SeedWebProfiles(APIContext apiContext)
        {
            var digitalGoods = new WebProfile()
            {
                name = "StapleFoods",
                input_fields = new InputFields()
                {
                    no_shipping = 1
                }
            };
            WebProfile.Create(apiContext, digitalGoods);
        }

        //private APIContext GetApiContext()
        //{
        //    // Authenticate with PayPal
        //    var config = ConfigManager.Instance.GetProperties();
        //    var accessToken = new OAuthTokenCredential(config).GetAccessToken();
        //    var apiContext = new APIContext(accessToken);
        //    return apiContext;
        //}

    }
}
