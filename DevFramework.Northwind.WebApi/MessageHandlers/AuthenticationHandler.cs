using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DevFramework.Northwind.WebApi.MessageHandlers
{
    // App_Start -> WebApiConfig.cs içerisinde config.MessageHandlers.Add(new AuthenticationHandler()) register edilecektir.
    // Böylelikle her request içerisindeki header bilgisi okunacak ve authentication işlemleri yapılıp ilgili principal atamaları yapılacaktır.
    // Core katmanımızda ise ilgili principal bilgileri (SecuredOperationAspect içinde) okunarak authorization işlemleri ele alınacaktır.
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = request.Headers.GetValues("authentication").FirstOrDefault();
                if (token != null)
                {
                    // string türündeki token'ı çözümlemek (decode) için öncelikle byte dizisine çeviriyoruz.
                    byte[] data = Convert.FromBase64String(token);
                    // ve ardından Encoding sınıfı ile dizimizi çözümlüyoruz.
                    string decodedString = Encoding.UTF8.GetString(data);

                    string[] values = decodedString.Split(':');

                    IUserService userService = InstanceFactory.GetInstance<IUserService>();

                    var user = userService.GetUserByNameAndPassword(values[0], values[1]);

                    if (user != null)
                    {
                        IPrincipal principal = new GenericPrincipal(new GenericIdentity(values[0]), 
                                                                    userService.GetUserRoles(user).Select(r => r.RoleName.ToLower()).ToArray());
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                }
            }
            catch (Exception)
            {
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
