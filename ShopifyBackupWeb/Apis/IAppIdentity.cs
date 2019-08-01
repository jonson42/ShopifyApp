using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopifyBackupWeb.Apis
{
        interface IAppIdentity
        {

        }
        public class AppIdentity : IAppIdentity
        {
            private readonly List<Claim> _claims;

            public AppIdentity(IHttpContextAccessor accessor)
            {
                var context = accessor.HttpContext;
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                _claims = context.User?.Claims == null ? new List<Claim>() : context.User.Claims.ToList();
            }

            public int AppSourceKey => Convert.ToInt32(_claims.SingleOrDefault(w => w.Type == nameof(AppSourceKey))?.Value);
            public string AppSourceName => _claims.SingleOrDefault(w => w.Type == nameof(AppSourceName))?.Value ?? string.Empty;
        }
}
