﻿using System.Web.Http;

namespace TodoList.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(RoutesConfig.Register);
            GlobalConfiguration.Configure(JsonCamelCaseConfig.Configure);
        }
    }
}
