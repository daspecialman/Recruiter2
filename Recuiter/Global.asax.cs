using Data.Models;
using Newtonsoft.Json;
using Recuiter;
using Recuiter.CustomAuthentication;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Linq;
using Recruiter.Context;
using System.Net.Http;
using System.Data.Entity;
using Recruiter.App_Start;
using System.Web.Http;

namespace Recruiter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            new DbInsertOnAppStart().Seed();
		}


		protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["Cookie1"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);

                principal.UserId = serializeModel.UserId;
                principal.FirstName = serializeModel.FirstName;
                principal.LastName = serializeModel.LastName;
                principal.Roles = (serializeModel.RoleName != null) ? (serializeModel.RoleName.ToArray<string>()) : new string[] { };

                HttpContext.Current.User = principal;
            }

        }

    }

}
