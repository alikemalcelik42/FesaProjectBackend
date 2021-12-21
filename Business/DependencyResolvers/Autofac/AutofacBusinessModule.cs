namespace Business.DependencyResolvers.Autofac
{
    using Castle.DynamicProxy;
    using Core.Utilities.Interceptors;
    using Core.Utilities.Security.JWT;
    using DataAccess.Abstract;
    using DataAccess.Concrete.EntityFramework;
    using global::Autofac;
    using global::Autofac.Extras.DynamicProxy;
    using global::Business.Abstarct;
    using global::Business.Concrete;

    namespace Business.DependencyResolvers.Autofac
    {
        public class AutofacBusinessModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<WriteManager>().As<IWriteService>().SingleInstance();
                builder.RegisterType<EfWriteDal>().As<IWriteDal>().SingleInstance();

                builder.RegisterType<UserManager>().As<IUserService>();
                builder.RegisterType<EfUserDal>().As<IUserDal>();

                builder.RegisterType<AuthManager>().As<IAuthService>();
                builder.RegisterType<JwtHelper>().As<ITokenHelper>();

                var assembly = System.Reflection.Assembly.GetExecutingAssembly();

                builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                    .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                    {
                        Selector = new AspectInterceptorSelector()
                    }).SingleInstance();
            }
        }
    }
}
