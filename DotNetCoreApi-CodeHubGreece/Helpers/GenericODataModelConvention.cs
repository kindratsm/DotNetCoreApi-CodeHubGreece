using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

namespace DotNetCoreApi_CodeHubGreece.Helpers
{
    public class GenericODataModelConvention : IControllerModelConvention
    {

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType
                && controller.ControllerType.GetGenericTypeDefinition() == typeof(GenericODataController<>))
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var customNameAttribute = genericType.GetCustomAttribute<GeneratedControllerAttribute>();

                if (customNameAttribute != null)
                {
                    controller.ControllerName = genericType.Name;
                }
            }
        }

    }
}
