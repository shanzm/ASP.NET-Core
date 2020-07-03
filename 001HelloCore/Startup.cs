using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _001HelloCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

//.net ��չ��
using Microsoft.Extensions.DependencyInjection;//����ע�������ڵ������ռ�
//���DI��ܣ���.net �Դ��ģ���ԱȽϼ򵥣����ǹ��ܲ��࣬��ͨ�����õ�
//�����Ҫʹ�ø��๦�ܣ������ʹ�õ�������IOC����
//ASP.NET Core������ע����������ģ����е��඼��Ҫע�룬�ſ���ʹ�ã�ͬʱ������ʵ�ֵ���ģʽ������������
using Microsoft.Extensions.Hosting;

namespace _001HelloCore
{
    /// <summary>
    /// �����������Asp.Net Core Web Ӧ�õ������࣬
    /// �����еķ����������ùܵ����м�� 
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //���÷���
        //��������ǿ�ѡ�����û�з���Ļ������Բ�д���������
        //������ע��ķ�����������ӵ�����������IOC������
        //IOC��������ע���������������ʵ���˷���ӿڵ����֮Ϊ������������У����������񣨸�����ʵ�������󣩣�
        public void ConfigureServices(IServiceCollection services)
        {
            //���������� CreateHostBuilder(args).Build()����ConfigureServices������ConfigureServices�����Ĳ������Ƿ��������Ľӿڡ�����֮�������������������������в�����
            //������������ɺ󣬾�Ĭ�ϵİ�����ע����һЩ���񣬱����ȡ������������,����Ӧ�����õȣ�  
            //ͨ��ʹ��services��������������Ӹ��ַ�����
            //����Ӧ�ö��������У�����Ӧ�ö��ǿ����õ���������ģ���ȡ��ע����κ����͵�ʵ��

            //�������ASP.NET Core���õķ������
            //��ӶԿ�������API��ع��ܵ�֧�֣����ǲ�֧����ͼ��ҳ��
            services.AddControllers();//�������½�����Web API��Ŀ����Ĭ���������������
            //MVC��ͼ�������
            services.AddControllersWithViews();//MVC��ͼ�������֧��Razor��
            //services.AddRazorPages();//ʹ��Razor��ͼ����

            //�����.net core 2.X�汾��ʹ�õ�MVC�������
            //ע�⣬����ע��������ע�����ͬ����ķ���Ḳ��ǰע��ġ�
            services.AddMvc();

            //ʹ�õ������ķ���ֻҪ��֧��.net core �ĵ����������������ṩһ��������չ����

            //IOC������������������ڵģ�
            //��ô���Ǹ���ô�������������������?��ʹ��.net core���Դ��ķ��������ʱ�������ǲ���Ҫ�����������ڵģ�Ĭ�϶������úõ�
            //���ǣ������Ҫ����Զ�������࣬�����Ҫѡ��һ��
            //��.net core ���Դ���IOC�����еĶ�����������������
            //services.AddTransient<>();    ˲ʱ����Ҳ����AutoFac�е�һ������һ��ʵ������ÿ�ν��������ʱ�򶼷���һ���µ�ʵ��
            //services.AddScoped<>();       ������һ�������д���һ��ʵ���������Ĳ�������Ҫ���ʵ�����򲻻��ڴ����µĶ�����֮ǰ�����ĸ�ʵ��
            //services.AddSingleLeton<>();  ����������Ӧ�õ��������ڣ�ֻҪ�����������ͬһ��ʵ����ֻ���ڵ�һ�ε�ʱ�򴴽�һ��ʵ����֮����ʹ�����ʵ��


            //����Զ���������ʵ���˽ӿڵ���   ���ӿڳ�֮Ϊ���񣩡�
            //����������Services�ļ����У��Լ������ģ��������һ��IMessageService�ӿڣ�����һ��EmailService��ʵ�ָýӿ�
            //������������Զ���ķ�����Ϣ�ķ������������������Ϊ˲ʱ
            services.AddTransient<IMessageService, EmailService>();//AddTransient<T1,T2>()����T1�Ƿ��񣨽ӿڻ�����ࣩ��T2��������ӿ�ʵ���ࣩ

            //��Ҫ����һ�£�IOC�ĸ���������ʲô�����Ǵ���һ�����󣬲���Ҫ�Լ����������д���
            //�����������������һ��EmailService����IOC�����У���ô������������������ô��ȡ���EmailService�����أ�
            //������һ��ʾ�����½�һ��Controller�ļ��У����һ��һ��HomeController��
            //����ʹ�ü�HomeController��


            //���ﻹ��һ�����⣬����ͬһ�����񣨽ӿ�),�ж��ʵ���ࣨ�������������Ҫ��ÿ��ʵ�����ʵ������ע�뵽IOC�����У����������ǲ����Եģ�
            //���磬�������Ǹ�IMessageService�ӿ����һ��ʵ����SmsService
            //���ǽ�SmsServiceע�ᵽ������
            services.AddTransient<IMessageService, SmsService>();
            //����������ǲ��ᱨ��ģ�������ʵ�ϣ�SmsService�������������ע���EmailService���
            //��ô������ô����أ��Ǿ���һ���ӿھ�һ��ʵ���ࣨһ�������һ���������Ȼ��ʹ��services.AddXXX<>()һ��һ������ӷ���
            //��Ȼ��Ҳ����ʹ�õ�������IOC����ʵ��AutoFac�������ǿ���һ���ӿ��ж����ͬʵ�����

            //��������Ҫ˼��һ�����⣺�Ǿ��������Լ�д��һ���������������������Ҫ�����ܶ������ķ����ѵ�����Ҫ��ʹ���������Ŀ�����Ա������������е�������ѡ������������
            //��Ȼ���������ġ����ǿ��Դ���һ��������չ�������½�һ��Extensions�ļ��У����һ��MessageServiceExtensions�ࣩ
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //���ùܵ�
        //��������Ǳ����
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
    }
}

