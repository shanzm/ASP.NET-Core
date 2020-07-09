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
                    //ע��������Ҫʹ�������Զ����Startup�����ã�������Ҫ���Ͼ�����޸�Ϊ���棺
                    webBuilder.UseStartup(Assembly.GetExecutingAssembly().FullName);
                    //��ȡ��ǰ�ĳ��򼯵����ƣ��ڵ�ǰ�������Һ��������к���Demo��Startup�ࣨ��Ϊ������launchSettings���õĻ�����ΪDemo��
                    //����û��Startup������ʹ��Ĭ�ϵ�Startup��
                });
    }
}
