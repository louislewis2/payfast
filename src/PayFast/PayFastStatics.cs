namespace PayFast
{
    public static class PayFastStatics
    {
        public static string[] ValidSites => new[] { "www.payfast.co.za", "sandbox.payfast.co.za", "w1w.payfast.co.za", "w2w.payfast.co.za" };
        public const string CompletePaymentConfirmation = "COMPLETE";
        public const string CancelledPaymentConfirmation = "CANCELLED";
    }
}
