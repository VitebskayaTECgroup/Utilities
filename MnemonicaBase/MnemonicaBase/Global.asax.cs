using System.Web.Mvc;
using System.Web.Routing;

namespace MnemonicaBase
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			RouteTable.Routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
