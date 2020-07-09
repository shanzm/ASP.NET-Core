using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _004Environment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseStartup<Startup>();
                    //注意若是需要使用我们自定义的Startup类配置，这里需要将上句代码修改为下面：
                    webBuilder.UseStartup(Assembly.GetExecutingAssembly().FullName);
                    //获取当前的程序集的名称，在当前程序集中找含有命名中含有Demo的Startup类（因为我们在launchSettings设置的环境名为Demo）
                    //若是没有Startup类则再使用默认的Startup类
                });
    }
}
