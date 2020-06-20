using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/*
 研究.net Core 则首要研究：启动流程，主机，依赖注入，服务容器，管道，中间件，应用配置，多环境，日志，路由，静态文件

.net core 3.1 是可以开发winform 和webForm的，但是目前还不能跨平台的

首先要明白：.net core是.net的核心API，
.net extensions是.net core 的扩展包，其中 都是.net的功能性组件，包括日志、依赖注入、应用程序配置等


.net core extensions的源码：https://github.com/dotnet/extensions
克隆下来怎么调试：

不要直接打开解决方案

首先运行克隆文件中的：restor.cmd-->运行：startvs.cmd-->运行：build.cmd-->打开解决方案

但是不建议这样，因为这是要运行所有的项目，自己的电脑的内存可能不够

所有，可以调试自己想要看的部分代码，

点开：extension-->src-->选中需要查看的部分（比如Hosting表示主机类）-->点击该目录下的startvs.cmd

其实查看的效果和在线的查看的是一模一样的
.net core在线浏览源码：https://source.dot.net/

 */


/*

1. 新建项目，若是没有Core模板，则下拉到最后，安装新的工具和模版

2. 新建一个ASP.NET Core Web应用程序

3. 下一步新建项目名称及位置，

4. 下一步选择.NET Core -->.NET Core 3.1(注意不需使用VS2019 v16.4版本以上)
    选择空项目模版

   注意这里先 取消选中HTTPS配置的单选框

4. 调试的时候，在调试的下拉框中选择当前项目名称的选项（即使用控制台启动），不要点击IIS Express调试（这其实是涉及到两种）

    若是使用IIS启动，则不会出现控制台的调试信息，而且也就不会启动Kestrel

 */


namespace _001HelloCore
{

    /// <summary>
    /// 整个的Program类中的Main()函数就是在创建主机和配置主机
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
        //上面写法就是默认生成的，就是 Linq写法，其实等价于下面写法

        ///默认主机构建器，主要用于配置主机 
        ///主机负责应用的启动和生存期的管理、配置服务器和请求处理的管道
        ///默认设置日志记录、依赖关系的注入和配置
        ///主机是.net core 中的一个类，类名就是Host
        ///主机封装了应用资源的对象，应用是运行在主机中的，主机中包含当前应用所需要的所有资源

        ///.net core 有两种主机，泛型主机(通用主机 )和web主机（他是通用主机的扩展，它提供额外web功能，支持HTTP）


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>//从前缀为“ASPNETCORE”的环境变量加载WEB主机配置，默认是将Kestrel设置为Web服务器并对其进行默认的设置，当然这里也可以设置为IIS服务器
                {
                    //组件配置(不属于主机，但是由主机调用，都是扩展类提供的方法)
                    //在这里配置称为硬编码，其实以上配置都是可以在配置文件中配置的
                    webBuilder.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 1024 * 1024 * 100);//配置请求体最大值是100M，默认是28.6M
                    webBuilder.ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Debug));//配置日记记录的最小级别

                    //
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:8000");//配置端口
                });
        }
    }
}

/*
Kestre是一个跨平台的适用于ASP.NET Core的Web服务器
可以简单的看作是IIS，但是其功能比较少，提供的都是Http服务

注意Kestrel性能是极高的，而且可以运行在Linux上

主流用法是，与其他的方向代理服务器（Nginx,IIS,Apache）结合使用(就是用户发送的web请求是直接发送给反向代理服务器，由反向代理服务器在发送给Kestrel服务器)


*/
