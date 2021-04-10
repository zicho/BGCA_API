//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System;
//using System.Security.Claims;

//namespace API.Core.Attributes
//{
//    public class AuthorizeUser : IAuthorizationFilter
//    {
//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var user = context.HttpContext.User.FindFirstValue(ClaimTypes.Name);
//            var role = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);

//            if (!hasClaim)
//            {
//                //            var user = _context.User.FindFirstValue(ClaimTypes.Name);
//                //            var role = GetRoleFromHttpContext();
//            }
//        }
//    }
//}
////    {
////        private readonly HttpContext _context;

////        private string GetRoleFromHttpContext() => _context.User.FindFirstValue(ClaimTypes.Role);

////        public AuthorizeUser(HttpContext context)
////        {
////            _context = context;

////            var user = _context.User.FindFirstValue(ClaimTypes.Name);
////            var role = GetRoleFromHttpContext();
////        }

////        protected override bool AuthorizeCore(HttpContext httpContext)
////        {
////            var isAuthorized = base.AuthorizeCore(httpContext);
////            if (!isAuthorized)
////            {
////                return false;
////            }

////            string username = httpContext.User.Identity.Name;

////            UserRepository repo = new UserRepository();

////            return repo.IsUserInRole(username, this.Roles);
////        }
////    }
////}