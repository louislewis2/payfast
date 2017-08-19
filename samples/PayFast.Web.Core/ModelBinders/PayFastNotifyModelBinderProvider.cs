namespace PayFast.Web.Core.ModelBinders
{
    using System;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

    public class PayFastNotifyModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(PayFastNotify))
            {
                return new BinderTypeModelBinder(typeof(PayFastNotifyModelBinder));
            }

            return null;
        }
    }
}
