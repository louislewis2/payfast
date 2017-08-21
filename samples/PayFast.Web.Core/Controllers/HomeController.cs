namespace PayFast.Web.Core.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Logging;

    using PayFast;
    using PayFast.AspNetCore;

    public class HomeController : Controller
    {
        #region Fields

        private readonly PayFastSettings payFastSettings;
        private readonly ILogger logger;

        #endregion Fields

        #region Constructor

        public HomeController(IOptions<PayFastSettings> payFastSettings, ILogger<HomeController> logger)
        {
            this.payFastSettings = payFastSettings.Value;
            this.logger = logger;
        }

        #endregion Constructor

        #region Methods

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Recurring()
        {
            var recurringRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

            // Merchant Details
            recurringRequest.merchant_id = this.payFastSettings.MerchantId;
            recurringRequest.merchant_key = this.payFastSettings.MerchantKey;
            recurringRequest.return_url = this.payFastSettings.ReturnUrl;
            recurringRequest.cancel_url = this.payFastSettings.CancelUrl;
            recurringRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            recurringRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            recurringRequest.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            recurringRequest.amount = 20;
            recurringRequest.item_name = "Recurring Option";
            recurringRequest.item_description = "Some details about the recurring option";

            // Transaction Options
            recurringRequest.email_confirmation = true;
            recurringRequest.confirmation_address = "sbtu01@payfast.co.za";

            // Recurring Billing Details
            recurringRequest.subscription_type = SubscriptionType.Subscription;
            recurringRequest.billing_date = DateTime.Now;
            recurringRequest.recurring_amount = 20;
            recurringRequest.frequency = BillingFrequency.Monthly;
            recurringRequest.cycles = 0;

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{recurringRequest.ToString()}";

            return Redirect(redirectUrl);
        }

        public IActionResult OnceOff()
        {
            var onceOffRequest = new PayFastRequest(this.payFastSettings.PassPhrase);
            // Merchant Details
            onceOffRequest.merchant_id = this.payFastSettings.MerchantId;
            onceOffRequest.merchant_key = this.payFastSettings.MerchantKey;
            onceOffRequest.return_url = this.payFastSettings.ReturnUrl;
            onceOffRequest.cancel_url = this.payFastSettings.CancelUrl;
            onceOffRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            onceOffRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            onceOffRequest.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            onceOffRequest.amount = 30;
            onceOffRequest.item_name = "Once off option";
            onceOffRequest.item_description = "Some details about the once off payment";

            // Transaction Options
            onceOffRequest.email_confirmation = true;
            onceOffRequest.confirmation_address = "sbtu01@payfast.co.za";

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";

            return Redirect(redirectUrl);
        }

        public IActionResult AdHoc()
        {
            var adHocRequest = new PayFastRequest(this.payFastSettings.PassPhrase);
            // Merchant Details
            adHocRequest.merchant_id = this.payFastSettings.MerchantId;
            adHocRequest.merchant_key = this.payFastSettings.MerchantKey;
            adHocRequest.return_url = this.payFastSettings.ReturnUrl;
            adHocRequest.cancel_url = this.payFastSettings.CancelUrl;
            adHocRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            adHocRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            adHocRequest.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            adHocRequest.amount = 70;
            adHocRequest.item_name = "Adhoc Agreement";
            adHocRequest.item_description = "Some details about the adhoc agreement";

            // Transaction Options
            adHocRequest.email_confirmation = true;
            adHocRequest.confirmation_address = "sbtu01@payfast.co.za";

            // Recurring Billing Details
            adHocRequest.subscription_type = SubscriptionType.AdHoc;

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{adHocRequest.ToString()}";

            return Redirect(redirectUrl);
        }

        public IActionResult Return()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Notify([ModelBinder(BinderType = typeof(PayFastNotifyModelBinder))]PayFastNotify payFastNotifyViewModel)
        {
            payFastNotifyViewModel.SetPassPhrase(this.payFastSettings.PassPhrase);

            var calculatedSignature = payFastNotifyViewModel.GetCalculatedSignature();

            var isValid = payFastNotifyViewModel.signature == calculatedSignature;

            this.logger.LogInformation($"Signature Validation Result: {isValid}");

            // The PayFast Validator is still under developement
            // Its not recommended to rely on this for production use cases
            var payfastValidator = new PayFastValidator(this.payFastSettings, payFastNotifyViewModel, this.HttpContext.Connection.RemoteIpAddress);

            var merchantIdValidationResult = payfastValidator.ValidateMerchantId();

            this.logger.LogInformation($"Merchant Id Validation Result: {merchantIdValidationResult}");

            var ipAddressValidationResult = await payfastValidator.ValidateSourceIp();

            this.logger.LogInformation($"Ip Address Validation Result: {merchantIdValidationResult}");

            // Currently seems that the data validation only works for success
            if (payFastNotifyViewModel.payment_status == PayFastStatics.CompletePaymentConfirmation)
            {
                var dataValidationResult = await payfastValidator.ValidateData();

                this.logger.LogInformation($"Data Validation Result: {dataValidationResult}");
            }

            if (payFastNotifyViewModel.payment_status == PayFastStatics.CancelledPaymentConfirmation)
            {
                this.logger.LogInformation($"Subscription was cancelled");
            }

            return Ok();
        }

        public IActionResult Error()
        {
            return View();
        }

        #endregion Methods
    }
}
