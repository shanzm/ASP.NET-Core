using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _004Environment
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            /*--------------------------------------------------------------1.判断不同的启动环境-----------------------------------------------------------------*/
            //这里判断是否是开发环境，若是则配置开发者异常页面中间件
            //这里的开发者环境是在launchSettings中配置的
            //可以通过以下方法判断启动环境。
            //env.IsProduction()
            //env.IsStaging()
            //env.IsEnvironment("Demo");//判断是否是自定义的启动环境Demo
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        /*-----------------------------------------------------------------2.为不同的启动环境定义不同的配置方法---------------------------------------------------*/
        //上面可以通过判断启动环境来配置不同的服务组件和中间件
        //然而在在ConfigureServices和Configure中可能有许多需要判断环境的地方
        //其实还是有更简洁的写法，直接使用下面的方法
        //注意这里的命名都是默认的方法，是约定大于配置
        //也就是在Configure+环境名，比如说我们在launchSettings中定义了当前的环境变量是Demo,这里函数名就是：ConfigureDemo()
        //当前Startup执行的时候，会优先查找含有环境名称的配置函数执行
        //但是若是我们的环境命名为Demo，但是我们没有ConfigureDemo()方法则会使用默认的Configure()函数
        public void ConfigureDemo(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("helle DemoEnvironment");
            });

        }

        //对于配置服务的ConfigureServices（）方法也是一样的，只是需要我们需要把自定义的环境名加在"Configure"和"Services"中间
        //例如：ConfigureDemoServices()
        public void ConfigureDemoServices(IServiceCollection services)
        {

        }

        /*-----------------------------------------------------------------3.将不同的启动环境的方法放在不同而Startup类---------------------------------------------*/
        //如果我们需要多套环境，我们为不同的环境定义不同的配置方法，
        //如果将这些不同的环境的配置方法都写在一个Startup类中会显得杂乱
        //所以我们可以定义一个新的StartupDemo类，在该类中定义新的配置方法，这称之为：Startup类配置
    }
}
