using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sciencecom
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();
        }

//        protected void Application_Error(object sender, EventArgs e)
//{
//    HttpContext ctx = HttpContext.Current;
//    Exception ex = ctx.Server.GetLastError();
//    ctx.Response.Clear();
 
//    RequestContext rc = ((MvcHandler)ctx.CurrentHandler).RequestContext;
//    IController controller = new Sciencecom.Controllers.DataController(); // Тут можно использовать любой контроллер, например тот что используется в качестве базового типа
//    var context = new ControllerContext(rc, (ControllerBase)controller);
 
//    var viewResult = new ViewResult();
 
//    var httpException = ex as HttpException;
//    if (httpException != null)
//    {
//        switch (httpException.GetHttpCode())
//        {
//            case 404:
//                viewResult.ViewName = "Error";
//                break;
 
//            case 500:
//                viewResult.ViewName = "Error500";
//                break;
 
//            default:
//                viewResult.ViewName = "Error";
//                break;
//        }
//    }
//    else
//    {
//        viewResult.ViewName = "Error";
//    }
 
//    viewResult.ViewData.Model = new HandleErrorInfo(ex, context.RouteData.GetRequiredString("controller"), context.RouteData.GetRequiredString("action"));
//    viewResult.ExecuteResult(context);
//    ctx.Server.ClearError();
//}

    }
}
