using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using WLN.Test.Project.Logic.Membership.Identity;

namespace WLN.Test.Project.Web.Modules
{
    public class MembershipModule : IHttpModule
    {
        private MyEventHandler _eventHandler;
       
        public void Dispose()
        { }

        public delegate void MyEventHandler(Object s, EventArgs e);

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += OnAuthenticateRequest;
        }

        public event MyEventHandler MyEvent
        {
            add { _eventHandler += value; }
            remove { _eventHandler -= value; }
        }

        public void OnAuthenticateRequest(Object s, EventArgs e)
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
                var i = new UserIdIdentity { IsAuthenticated = true, Name = t.Name, UserId = ui.UserId };
                var gp = new GenericPrincipal(i, null);
                HttpContext.Current.User = gp;
            }

            if (_eventHandler != null)
                _eventHandler(this, null);
        }
    }
}