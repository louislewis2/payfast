namespace PayFast.AspNet
{
    using System;
    using System.Web.Mvc;
    using System.Diagnostics;
    using System.Collections.Generic;

    public class PayFastNotifyModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                if (bindingContext == null)
                {
                    throw new ArgumentNullException(nameof(bindingContext));
                }

                var formCollection = controllerContext.HttpContext.Request.Form;

                if (formCollection == null || formCollection.Count < 1)
                {
                    return null;
                }

                var properties = new Dictionary<string, string>();

                foreach (string key in formCollection.Keys)
                {
                    var value = formCollection.Get(key);

                    properties.Add(key: key, value: value);
                }

                var model = new PayFastNotify();
                model.FromFormCollection(properties);

                return model;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
