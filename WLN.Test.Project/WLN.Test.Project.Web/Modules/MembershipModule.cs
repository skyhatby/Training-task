using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Microsoft.Practices.Unity;
using WLN.Test.Project.Logic.Membership.Identity;
using WLN.Test.Project.Logic.Membership.Intarfaces;
using WLN.Test.Project.Web.App_Start;

namespace WLN.Test.Project.Web.Modules
{
    public class MembershipModule : IHttpModule
    {
        private IMembershipService _membershipService;

        public MembershipModule()
        {
            _membershipService = UnityConfig.GetConfiguredContainer().Resolve<IMembershipService>();
        }
        private MyEventHandler _eventHandler;
       
        public void Dispose()
        { }

        public delegate void MyEventHandler(Object s, EventArgs e);

        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += OnPostAuthenticateRequest;
        }

        public event MyEventHandler MyEvent
        {
            add { _eventHandler += value; }
            remove { _eventHandler -= value; }
        }

        public void OnPostAuthenticateRequest(Object s, EventArgs e)
        {
            var app = s as HttpApplication;

            if (app != null)
            {
                var c = app.Request.Cookies["asdf"];
                if (c == null) return;
                var t = FormsAuthentication.Decrypt(c.Value);
                if (t == null) return;
                var st = t.UserData;
                var ui = UserInfo.FromString(st);
                string[] roles = _membershipService.GetUserRoles(ui.UserId);
                var i = new UserIdIdentity { IsAuthenticated = true, Name = t.Name, UserId = ui.UserId };
                var gp = new GenericPrincipal(i, roles);
                HttpContext.Current.User = gp;
            }

            if (_eventHandler != null)
                _eventHandler(this, null);
        }
    }
}