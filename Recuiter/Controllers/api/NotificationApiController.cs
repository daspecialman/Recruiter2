using Data.Hubs;
using Data.Models;
using Recruiter.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Recruiter.Controllers.api
{
    public class NotificationApiController : ApiController
    {
        private RecruiterContext context = new RecruiterContext();

        [HttpPost]
        public HttpResponseMessage SendNotification(NotifModels obj)
        {
            NotificationHub notificationHub = new NotificationHub();
            Notification objNotif = new Notification();
            objNotif.SentTo = obj.UserID;

            context.Configuration.ProxyCreationEnabled = false;
            context.Notifications.Add(objNotif);
            context.SaveChanges();

            notificationHub.SendNotification(objNotif.SentTo);

            var query = (from t in context.Notifications
                         select t).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, new { query });
        }
    }
}