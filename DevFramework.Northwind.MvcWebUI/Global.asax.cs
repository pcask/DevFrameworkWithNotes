using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using DevFramework.Core.Utilities.Mvc.Infrastructure;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace DevFramework.Northwind.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Client'imizin dll tabanli çalismasi için çözümlemeyi BusinessModule üzerinden yapiyoruz.
            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule(), new AutoMapperModule()));

            // Client'imizin servis tabanli çalismasi için çözümlemeyi ServiceModule üzerinden yapiyoruz.
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new ServiceModule(), new AutoMapperModule()));
        }

        // Asp.net mvc projemizin initialize'i sirasinda PostAuthenticateRequest event'ine abone oluyoruz.
        // PostAuthenticateRequest olayi bir güvenlik modülü kullanicinin kimligini belirlediginde tektiklenir.
        // Yani kullanicinin authentication bilgilerine erisebilecegimiz olay.
        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                // Login ekraninda CreateAuthCookie method'i ile daha önce FormsAuthentication.FormsCookieName isminde olusturmus oldugumuz cookie'e erisiyoruz.
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie == null)
                    return;

                var encryptedTicket = authCookie.Value;

                if (String.IsNullOrEmpty(encryptedTicket))
                    return;

                var ticket = FormsAuthentication.Decrypt(encryptedTicket);

                var identity = (new SecurityUtilities()).ForsmAuthTicketToIdentity(ticket);
                var principal = new GenericPrincipal(identity, identity.Roles);

                // Olusturdugumuz principal'i web uygulamalarinda authentication, authorization vb. islemlerde kullanmak için;
                HttpContext.Current.User = principal;
                // Olusturdugumuz principal'i backend tarafinda authentication, authorization vb. islemlerde kullanmak için;
                Thread.CurrentPrincipal = principal;
            }
            catch (Exception)
            {

            }
        }
    }
}
