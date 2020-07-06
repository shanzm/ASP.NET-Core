using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _001HelloCore.Extensions;
using _001HelloCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

//.net 扩展包
using Microsoft.Extensions.DependencyInjection;//依赖注入框架所在的命名空间
//这个DI框架，是.net 自带的，相对比较简单，但是功能不多，普通需求够用的
//如果需要使用更多功能，则可以使用第三方的IOC容器。
//其实大部分的时候都是需要使用第三方的IOC容器的
//在.net Core项目中使用第三方的IOC容器，其实使用方法都是一样的， 只是第三方的IOC容器将默认的IOC容器替换了而已
//ASP.NET Core中依赖注入是最基础的，所有的类都需要注入，才可以使用，同时帮我们实现单例模式，管理单例对象
using Microsoft.Extensions.Hosting;

namespace _001HelloCore
{
    /// <summary>
    /// 这里才是我们Asp.Net Core Web 应用的启动类，
    /// 该类中的方法用于配置管道和中间件 
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //配置服务
        //这个方法是可选（如果没有服务的话，可以不写这个方法）
        //以依赖注入的方法将服务添加到服务容器（IOC容器）
        //IOC容器负责：注册组件（添加组件（实现了服务接口的类称之为组件）到容器中），解析服务（给服务实例化对象），
        public void ConfigureServices(IServiceCollection services)
        {
            /*--------------------------------------------------------------配置服务------------------------------------------------------------------*/

            //主机构建器 CreateHostBuilder(args).Build()调用ConfigureServices（），ConfigureServices（）的参数就是服务容器的接口。换言之，服务容器在主机构建过程中产生。
            //在主机创建完成后，就默认的帮我们注册了一些服务，比如获取主机环境变量,整个应用配置等，  
            //通过使用services可以在容器中添加各种服务类
            //整个应用都在主机中，整个应用都是可以用到这个容器的，获取已注册的任何类型的实例

            //下面添加ASP.NET Core内置的服务组件
            //添加对控制器和API相关功能的支持，但是不支持视图和页面
            services.AddControllers();//若是你新建的是Web API项目，则默认添加这个服务组件
            //MVC视图服务组件
            services.AddControllersWithViews();//MVC视图本身就是支持Razor的
            //services.AddRazorPages();//使用Razor视图引擎

            //这个是.net core 2.X版本中使用的MVC服务组件
            //注意，这里注册服务后面注册的相同种类的服务会覆盖前注册的。
            services.AddMvc();

            //使用第三方的服务，只要是支持.net core 的第三方服务，它都会提供一个服务扩展方法


            /*---------------------------------------------------------------IOC容器中的组件的声明周期--------------------------------------------------*/

            //IOC中组件都是有生命周期的，
            //那么我们该怎么设置组件的生命周期呢?在使用.net core中自带的服务组件的时候，我们是不需要设置生命周期的，默认都是设置好的
            //但是，如果需要添加自定义服务类，你就需要选择一个
            //在.net core 中自带的IOC容器中的对象生存周期有三种
            //services.AddTransient<>();    瞬时：（也就是AutoFac中的一个依赖一个实例），每次解析服务的时候都返回一个新的实例
            //services.AddScoped<>();       作用域：一个请求中创建一个实例，后续的操作还需要这个实例，则不会在创建新的而是用之前创建的改实例
            //services.AddSingleLeton<>();  单例：整个应用的生命周期，只要向服务器请求同一个实例，只有在第一次的时候创建一个实例，之后都是使用这个实例


            //添加自定义的组件（实现了接口的类   ，接口称之为服务）。
            //首先我们在Services文件夹中（自己创建的），添加了一个IMessageService接口，声明一个EmailService类实现该接口
            //这里我们添加自定义的发送消息的服务组件，器生命周期为瞬时
            services.AddTransient<IMessageService, EmailService>();//AddTransient<T1,T2>()其中T1是服务（接口或抽象类），T2是组件（接口实现类）


            /*----------------------------------------------------------------如何使用容器中的服务组件----------------------------------------------------*/

            //你要回忆一下，IOC的根本作用是什么？就是创建一个对象，不需要自己在依赖类中创建
            //所以这里我们添加了一个EmailService对象到IOC容器中，那么我们在其他类中是怎么获取这个EmailService对象呢？
            //这里做一个示例：新建一个Controller文件夹，添加一个一个HomeController类
            //具体使用见HomeController类


            /*-----------------------------------------------------------------配置自定义服务组件--------------------------------------------------------*/

            //这里还有一个问题，就是同一个服务（接口),有多个实现类（组件），我们想要把每个实现类的实例对象都注入到IOC容器中，可以吗？这是不可以的！
            //比如，这里我们给IMessageService接口添加一个实现类SmsService
            //我们将SmsService注册到容器中
            services.AddTransient<IMessageService, SmsService>();
            //在这里程序是不会报错的，但是事实上，SmsService组件覆盖了上面注册的EmailService组件
            //那么这里怎么解决呢？那就是一个接口就一个实现类（一个服务就一个组件），然后使用services.AddXXX<>()一个一个的添加服务
            //当然你也可以使用第三方的IOC，其实在AutoFac容器中是可以一个接口有多个不同实现类的

            //这里我们要思考一个问题：那就是我们自己写了一个第三方组件，这个组件需要依赖很多其他的服务，难道我们要让使用这个组件的开发人员在这里添加所有的依赖并选择生存周期吗
            //当然不是这样的。我们可以创建一个服务扩展方法（新建一个Extensions文件夹，添加一个MessageServiceExtensions类,给IServiceCollection接口（也就是这里的services对象）扩展一个AddMessage方法）
            services.AddMessage();//这里就是使用我们自己定义的扩展方法，可以将"services.AddTransient<IMessageService, SmsService>();"取而代之


            //但是我们也看到了，IMessageService接口是有两个实现类的而，而我们对IServiceCollection扩展,添加的方法是" serviceCollection.AddSingleton<IMessageService, SmsService>();"
            //即：添加的服务是"services.AddTransient<IMessageService, SmsService>();",然而我们若是需要添加“services.AddTransient<IMessageService, EmailService>();”服务则我们还要在对IServiceCollection扩展
            //这里有另外的一种方法，使用构造器！
            //首先添加一个构造器MessageServiceBuilder，在构造器中定义方法用于配置服务的方法
            //在扩展方法中，添加一个扩展方法，其中一个参数是委托类型
            services.AddMessage(options: builder => builder.UserSms());
            //其实感觉这里为了配置服务，又是定义扩展类，又是定义构造器类，很麻烦，但是.net Core源码中到处都是这种方式！ 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //配置管道
        //这个方法是必须的
        //在Program中的 CreateHostBuilder(args).Build().Run();运行之后，执行Configre()
        //管道：Pipeline，其中包含各种中间件，在.netCore 中路由、认证、会话、缓存等等都是通过管道实现的
        //MVC,WebApi这个web开发在.netCore中就是建立在不同的中间件（Middleware)之上。
        //我们可以编写中间件，扩展请求通道。所以你要是想要建立一个自己的类似MVC的框架等，就可以通过编写大量的中间件来实现。

        //每个中间件都有两个职责：
        //1. 选择是否将请求传递给管道中的下一个中间件
        //2. 在管道的下一个中间件的前后执行工作
        //简而言之:每一个中间件都有权作出是否将请求传递给下一个中间件，也可以直接作出响应
        //就是说：若是当前中间件决定不需要将请求传递给下一个中间件，则当前中间件就直接作出响应，请求不在继续传到到后续的中间件中（这也称为管道的短路）。
        //举个例子：比如说http请求，经历的第一个中间件可以是权限验证中间件，若是没有通过权限验证，则可以直接作出响应，不需要把请求传递到后续的中间件。

        //中间件是有顺序的，中间件执行顺序就是你在这里的书写顺序

        //其实从下面添加中间件可以发现，中间件本质就是一堆委托
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*---------------------------------------------------------------最基本的添加中间件的方法---------------------------------------------*/

            /*我们在这里使用Use和Run添加两个中间件

           //使用Use添加中间件
           app.Use(async (context, next) =>
           {
               //请求处理
               await context.Response.WriteAsync("Middleware 1 begin");
               await next();//有这一句则该中间件将请求传递到下一个中间件，没有则直接进行本中间件的响应处理(也就是触发管道短路)
               //注意一切可以触发管道短路的中间件都是可以称之为终端终端中间件（终结点），所以这里我们只要把await next()删除，则本中间件就是终端中间件了

               //响应处理
               await context.Response.WriteAsync("Middleware 1 end");
           });

           //注意一些中间件是依赖某些服务的，所以在配置中间件的时候，你要现在ConfigureServices()中添加相应的服务
           //比如说这里要使用跨域中间件,则你需要在ConfigureServices()中添加services.AddCors()
           //然后在这里添加中间件：
           // app.UseCors();

           //使用Run()函数添加终端中间件（终结点），终端中间件只有一个，而且这个中间件是最后一个中间件，之后就没有了，就是对请求的响应处理了
           //这里这个中间件的作用就是打印"hello Run中间件 "
           //Run（）函数的参数是一个异步的委托，其参数是HttpContext类型的
           app.Run(async context => { await context.Response.WriteAsync("hello Middleware "); });

           */


            /*------------------------------------------------------------------添加自定义中间件------------------------------------------------*/
            //自定义中间件：我们添加一个TestMiddleware.cs定义中间件
            //将自定义的中间件TestMiddleware添加到管道中
            app.UseMiddleware<TestMiddleware>();

            //如果我们自定义一个中间件，发布出去希望给别人使用，
            //但是用户是不知道怎么使用的，这时候我们就是可以使用扩展方法，从而用户就可以像是使用app.UseXXX()的方式使用我们的中间件
            //添加CustomMiddlewareExtensions类，对IApplicationBuilder接口扩展
            app.UseTest();//这里使用自己定义的扩展方法，从而不需要在使用app.UseMiddleware<TestMiddleware>();来添加自定义的中间件。



            //顺便说一句在这个页面打断点是没有意义的，这个页面都是在对服务和中间件进行配置，这些委托是不会在这个页面执行的，中间件配置在Program中的 "  CreateHostBuilder(args).Build().Run();"运行中的时候执行一次
            //我们可以在中间件的内部打断点


            /*-----------------------------------------------------------------默认配置的中间件-------------------------------------------------*/

            if (env.IsDevelopment())//判断当前是否是开发这环境，在launchSettings.json中我们默认配置的环境变量是开发者环境“  "ASPNETCORE_ENVIRONMENT": "Development",”
            {
                app.UseDeveloperExceptionPage();//开发者异常界面中间件，如果出现异常则会把异常显示在该页面
            }

            //终端路由中间件
            //用于匹配路由和终结点
            app.UseRouting();

            //终结点中间件(即终端中间件，其实不是指某个特定的中间件，而是指能够触发管道短路的一类中间件)
            //用于配置路由和终结点之间的映射关系
            //注意在.net Core 2.X版本中UseRouting和UseEndpoints是统一为一个称之为路由中间件,在3.0中拆分为两个，这两个在项目中是 缺一不可的
            //终结点，可以视为应用程序提供针对HTTP请求的处理器
            //比如说，若是MVC项目则终结点就是控制器中的某个方法
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });//第一参数其实就是路由模版
            });

            //这里我们使用useXX添加中间件，其实其内部不是调用Use()就是调用Run()


           
        }
    }
}

