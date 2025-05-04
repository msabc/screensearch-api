using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ScreenSearch.Api.Conventions
{
    public class GlobalRoutePrefixConvention(IRouteTemplateProvider routeTemplateProvider) : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _globalPrefix = new(routeTemplateProvider);

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                // For each selector (a route configuration for a controller action)
                foreach (var selector in controller.Selectors)
                {
                    if (selector.AttributeRouteModel != null)
                    {
                        // Combine the global prefix with the existing route attribute.
                        selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(
                            _globalPrefix, selector.AttributeRouteModel);
                    }
                    else
                    {
                        // If there's no route attribute, just assign the global prefix
                        selector.AttributeRouteModel = _globalPrefix;
                    }
                }
            }
        }
    }
}
