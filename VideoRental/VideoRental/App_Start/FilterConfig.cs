using System.Web;
using System.Web.Mvc;

namespace VideoRental
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
            //NOTE: Here we mention global filters. 
            //It is equivalent to [Authorize] attribute on all controllers
            //You have to use [AllowAnonymous] on whatever action or controller you want to skip this filter
            filters.Add(new AuthorizeAttribute());

            //NOTE: To allow access tp app through Https protocol only
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
