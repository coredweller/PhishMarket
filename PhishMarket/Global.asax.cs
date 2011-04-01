using System;
using System.Web;
using StructureMap;
using TheCore.Infrastructure;
using PhishPond.Repository.LinqToSql;

namespace PhishMarket
{
    public class Global : System.Web.HttpApplication
    {
        LogWriter writer = new LogWriter();

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            OnStart();

        }

        protected virtual void OnStart()
        {
            //Setup Ioc container and related services
            Bootstrap();

            // uncomment the following line if you wish to have StructureMap verify its configuration.  ASP.NET error page will be generated if configuration is incorrect.
            ObjectFactory.AssertConfigurationIsValid();
            System.Diagnostics.Debug.Write(ObjectFactory.WhatDoIHave());
        }

        private void Bootstrap()
        {
            //setup IoC container
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new TheCore.CoreRegistry());
                x.AddRegistry(new PhishPond.PhishPondRegistry());
                x.AddRegistry(new PhishMarket.PhishMarketRegistry());
                x.AddRegistry(new Yaf.YafRegistry());
            });

            Ioc.InitializeWith(new DependencyResolverFactory(new DependencyResolver()));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            System.Web.HttpContext context = HttpContext.Current;
            System.Exception ex = Context.Server.GetLastError();

            writer.WriteLine("THERE WAS AN ERROR");
            writer.WriteLine(ex);

            context.Server.ClearError();

            //Response.Redirect("some_error_occured_we_are_sorry .aspx");   CONSIDER REPLACING WITH MY OWN ERROR PAGE EVENTUALLY
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}