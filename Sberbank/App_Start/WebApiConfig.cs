using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Sberbank
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();
			
			config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
			config.Routes.MapHttpRoute(
				name: "ControllerAndAction",
				routeTemplate: "api/{controller}/{action}"
			);
			//config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
			//config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings {
			//	NullValueHandling = NullValueHandling.Ignore,
			//	ContractResolver = new CamelCasePropertyNamesContractResolver()
			//};
			//config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
		}
    }
}
