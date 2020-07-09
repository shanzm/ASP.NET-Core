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

            /*--------------------------------------------------------------1.�жϲ�ͬ����������-----------------------------------------------------------------*/
            //�����ж��Ƿ��ǿ������������������ÿ������쳣ҳ���м��
            //����Ŀ����߻�������launchSettings�����õ�
            //����ͨ�����·����ж�����������
            //env.IsProduction()
            //env.IsStaging()
            //env.IsEnvironment("Demo");//�ж��Ƿ����Զ������������Demo
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

        /*-----------------------------------------------------------------2.Ϊ��ͬ�������������岻ͬ�����÷���---------------------------------------------------*/
        //�������ͨ���ж��������������ò�ͬ�ķ���������м��
        //Ȼ������ConfigureServices��Configure�п����������Ҫ�жϻ����ĵط�
        //��ʵ�����и�����д����ֱ��ʹ������ķ���
        //ע���������������Ĭ�ϵķ�������Լ����������
        //Ҳ������Configure+������������˵������launchSettings�ж����˵�ǰ�Ļ���������Demo,���ﺯ�������ǣ�ConfigureDemo()
        //��ǰStartupִ�е�ʱ�򣬻����Ȳ��Һ��л������Ƶ����ú���ִ��
        //�����������ǵĻ�������ΪDemo����������û��ConfigureDemo()�������ʹ��Ĭ�ϵ�Configure()����
        public void ConfigureDemo(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("helle DemoEnvironment");
            });

        }

        //�������÷����ConfigureServices��������Ҳ��һ���ģ�ֻ����Ҫ������Ҫ���Զ���Ļ���������"Configure"��"Services"�м�
        //���磺ConfigureDemoServices()
        public void ConfigureDemoServices(IServiceCollection services)
        {

        }

        /*-----------------------------------------------------------------3.����ͬ�����������ķ������ڲ�ͬ��Startup��---------------------------------------------*/
        //���������Ҫ���׻���������Ϊ��ͬ�Ļ������岻ͬ�����÷�����
        //�������Щ��ͬ�Ļ��������÷�����д��һ��Startup���л��Ե�����
        //�������ǿ��Զ���һ���µ�StartupDemo�࣬�ڸ����ж����µ����÷��������֮Ϊ��Startup������
    }
}
