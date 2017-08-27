namespace PayFast.ApiTypes
{
    using PayFast.Base;

    public class FetchResponse
    {
        #region Properties

        public string token { get; set; }
        public ResultStatus status { get; set; }

        #endregion Properties
    }
}
