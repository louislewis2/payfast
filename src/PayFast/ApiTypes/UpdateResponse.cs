namespace PayFast.ApiTypes
{
    using PayFast.Base;

    public class UpdateResponse : ApiResultBase
    {
        public GenericData<SubscriptionDetailResponse> data { get; set; }
    }
}
