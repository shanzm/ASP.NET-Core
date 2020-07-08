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
        /*-----------------------------------------------------------------1.注入IConfiguration类型对象--------------------------------------------------*/
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)//通过构造函数注入configuration对象，之后在ConfigureServices()和Configure()都可以使用该对象
        {
            _configuration = configuration;
        }

        /*-----------------------------------------------------------------2.将配置模型注入到容器中-------------------------------------------------------*/
        public void ConfigureServices(IServiceCollection services)
        {
            //绑定部分配置并注册到容器
            Action<WebSetting> webSetting = setting =>
            {
                _configuration.GetSection("WebSetting").Bind(setting);
            };
            services.Configure(webSetting);


            //绑定全部配置并注册到容器
            Action<AppSetting> webSetting1 = setting =>
            {
                _configuration.Bind(setting);
            };
            services.Configure(webSetting1);

            //绑定全部配置并注册到容器-简洁写法
            services.Configure<AppSetting>(_configuration);

            //默认_configuration读取的appsettings.json文件
            //若是我们自己创建一个配置文件，比如说customsettings文件，则我们需要以下写法：
            //依旧是先创建一个配置模型类CustomSetting
            //这里说一个技巧，复制json文件，然后在新建的文件中：编辑-->选择性粘贴-->将JSON粘贴为类
            var customConfig = new ConfigurationBuilder().AddJsonFile("customsettings.json").Build();
            services.Configure<CustomSetting>(customConfig);

        }


        /*-----------------------------------------------------------------3.创建配置模型，读取配置并绑定到模型--------------------------------------------*/
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSetting> appOptions, IOptions<CustomSetting> customSetting)
        {
            app.Run(async context =>
            {
                /*
                var connStr = _configuration["ConnectionString"];
                var title = _configuration["WebSetting:Title"];
                var isCheckIp = Convert.ToBoolean(_configuration["WebSetting:Behavior:IsCheckIp"]);//注意读取配置文件的值都是string类型 

                //通过字符串作为key直接读取字符串容易出错
                //且若是需要读取的配置太多，太麻烦！
                //我们可以使用配置模型对象
                //新建文件夹SettingModel用于存放所有的配置模型
                //注意创建模型的时候，与配置文件中的配置键相对应
                //因为配置文件是Json格式，所以存在嵌套，所以建立配置模型的时候，要层层嵌套，
                //一般最外层定义为AppSetting对象

                //数据模型绑定，有两种，一种全部绑定，一种是部分绑定

                //全部绑定
                AppSetting appSetting = new AppSetting();
                _configuration.Bind(appSetting);

                //部分绑定
                WebSetting webSetting = new WebSetting();
                _configuration.GetSection("WebSetting").Bind(webSetting);//只绑定WebSetting对象中的WebSetting属性
                */

                //上面我们是直接获取配置项，但是我们可以把配置模型在服务容器中注入，然后在这里使用
                //这里，我们先在Configure()方法中添加一个appOptions参数，通过该参数获取参数
                //appOptions .Value .WebSetting 
                //凡是支持注入的地方，都可以使用这种方式获取配置选项。

                await context.Response.WriteAsync(appOptions.Value.WebSetting.WebName.ToString());
                await context.Response.WriteAsync(customSetting.Value.Name);//在Configure参数中添加一个customSetting参数，用于传递自定义的配置文件customsettings中的配置
            });


        }
    }
}
