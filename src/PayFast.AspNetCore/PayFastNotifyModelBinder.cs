namespace PayFast.AspNetCore
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.Extensions.Primitives;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class PayFastNotifyModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var formCollection = bindingContext.HttpContext.Request.Form;

            if (formCollection == null || formCollection.Count < 1)
            {
                return Task.CompletedTask;
            }

            var properties = new Dictionary<string, string>();

            foreach (var key in formCollection.Keys)
            {
                StringValues value = string.Empty;

                formCollection.TryGetValue(key: key, value: out value);

                properties.Add(key: key, value: value);
            }

            var model = new PayFastNotify();
            model.FromFormCollection(properties);

            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}
