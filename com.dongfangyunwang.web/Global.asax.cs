using Autofac;
using Autofac.Integration.Mvc;
using com.dongfangyunwang.BLL;
using com.dongfangyunwang.DAL;
using com.dongfangyunwang.entity;
using com.dongfangyunwang.IBLL;
using com.dongfangyunwang.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace com.dongfangyunwang.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<MemberDAL>().As<IMemberDAL>();
            builder.RegisterType<MemberBLL>().As<IMemberBLL>();
            builder.RegisterType<FollowBLL>().As<IFollowBLL>();
            builder.RegisterType<FollowDAL>().As<IFollowDAL>();
            builder.RegisterType<FollowRecordDAL>().As<IFollowRecordDAL>();
            builder.RegisterType<FollowRecordBLL>().As<IFollowRecordBLL>();
            builder.RegisterType<InformationDAL>().As<IInformationDAL>();
            builder.RegisterType<InformationBLL>().As<IInformationBLL>();
        }
    }
}
