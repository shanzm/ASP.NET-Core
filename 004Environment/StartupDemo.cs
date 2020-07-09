using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _004Environment
{
    //注意通过Startup类配置启动环境为Demo环境，则需要新建一个命名为StartupDemo,
    //注意这里的命名也是遵循Startup+自定义的启动环境名

    //但是注意其中的配置函数需要使用默认的命名ConfigureServices()和Configure()

    //需要注意的是：在Program.cs 中webBuilder.UseStartup<Startup>()需要修改为
    //webBuilder.UseStartup(Assembly.GetExecutingAssembly().FullName);


    public class StartupDemo
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("hello startupdemo类配置");
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
