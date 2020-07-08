using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using _003ReadConfiguration.SettingModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace _003ReadConfiguration
{
    public class Startup
    {
        /*-----------------------------------------------------------------1.ע��IConfiguration���Ͷ���--------------------------------------------------*/
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)//ͨ�����캯��ע��configuration����֮����ConfigureServices()��Configure()������ʹ�øö���
        {
            _configuration = configuration;
        }

        /*-----------------------------------------------------------------2.������ģ��ע�뵽������-------------------------------------------------------*/
        public void ConfigureServices(IServiceCollection services)
        {
            //�󶨲������ò�ע�ᵽ����
            Action<WebSetting> webSetting = setting =>
            {
                _configuration.GetSection("WebSetting").Bind(setting);
            };
            services.Configure(webSetting);


            //��ȫ�����ò�ע�ᵽ����
            Action<AppSetting> webSetting1 = setting =>
            {
                _configuration.Bind(setting);
            };
            services.Configure(webSetting1);

            //��ȫ�����ò�ע�ᵽ����-���д��
            services.Configure<AppSetting>(_configuration);

            //Ĭ��_configuration��ȡ��appsettings.json�ļ�
            //���������Լ�����һ�������ļ�������˵customsettings�ļ�����������Ҫ����д����
            //�������ȴ���һ������ģ����CustomSetting
            //����˵һ�����ɣ�����json�ļ���Ȼ�����½����ļ��У��༭-->ѡ����ճ��-->��JSONճ��Ϊ��
            var customConfig = new ConfigurationBuilder().AddJsonFile("customsettings.json").Build();
            services.Configure<CustomSetting>(customConfig);

        }


        /*-----------------------------------------------------------------3.��������ģ�ͣ���ȡ���ò��󶨵�ģ��--------------------------------------------*/
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSetting> appOptions, IOptions<CustomSetting> customSetting)
        {
            app.Run(async context =>
            {
                /*
                var connStr = _configuration["ConnectionString"];
                var title = _configuration["WebSetting:Title"];
                var isCheckIp = Convert.ToBoolean(_configuration["WebSetting:Behavior:IsCheckIp"]);//ע���ȡ�����ļ���ֵ����string���� 

                //ͨ���ַ�����Ϊkeyֱ�Ӷ�ȡ�ַ������׳���
                //��������Ҫ��ȡ������̫�̫࣬�鷳��
                //���ǿ���ʹ������ģ�Ͷ���
                //�½��ļ���SettingModel���ڴ�����е�����ģ��
                //ע�ⴴ��ģ�͵�ʱ���������ļ��е����ü����Ӧ
                //��Ϊ�����ļ���Json��ʽ�����Դ���Ƕ�ף����Խ�������ģ�͵�ʱ��Ҫ���Ƕ�ף�
                //һ������㶨��ΪAppSetting����

                //����ģ�Ͱ󶨣������֣�һ��ȫ���󶨣�һ���ǲ��ְ�

                //ȫ����
                AppSetting appSetting = new AppSetting();
                _configuration.Bind(appSetting);

                //���ְ�
                WebSetting webSetting = new WebSetting();
                _configuration.GetSection("WebSetting").Bind(webSetting);//ֻ��WebSetting�����е�WebSetting����
                */

                //����������ֱ�ӻ�ȡ������������ǿ��԰�����ģ���ڷ���������ע�룬Ȼ��������ʹ��
                //�����������Configure()���������һ��appOptions������ͨ���ò�����ȡ����
                //appOptions .Value .WebSetting 
                //����֧��ע��ĵط���������ʹ�����ַ�ʽ��ȡ����ѡ�

                await context.Response.WriteAsync(appOptions.Value.WebSetting.WebName.ToString());
                await context.Response.WriteAsync(customSetting.Value.Name);//��Configure���������һ��customSetting���������ڴ����Զ���������ļ�customsettings�е�����
            });


        }
    }
}
