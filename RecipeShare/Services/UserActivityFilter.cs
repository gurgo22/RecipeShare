using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using RecipeShare.Data;
using RecipeShare.Models;

namespace RecipeShare.Services {
    public class UserActivityFilter : IActionFilter {

        public ApplicationDbContext context;

        public UserActivityFilter(ApplicationDbContext context) { 
            this.context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context) {
        
        }

        public void OnActionExecuting(ActionExecutingContext context) {

            string data = "";

            //USER SITE ACTION DATA
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            string url = controllerName + "/" + actionName; //$"{controllerName}/{actionName}";

            //USER INPUT DATA
            string userData = context.HttpContext.Request.QueryString.Value;

            if (!string.IsNullOrEmpty(userData)) {

                data = userData;

            } else {
                data = JsonConvert.SerializeObject(context.ActionArguments.FirstOrDefault());
            }

            string username = context.HttpContext.User.Identity.Name;

            string ip = context.HttpContext.Connection.RemoteIpAddress.ToString();

            if (username != null) {
                StoreActivity(data, url, username, ip);
            }
        }

        public void StoreActivity(string data, string url, string userName, string ip) {

            Console.WriteLine("Activity stored");
            UserActivity userActivity = new UserActivity(data,url,userName,ip);

            context.UserActivities.Add(userActivity);

            context.SaveChanges();
        }
    }
}
