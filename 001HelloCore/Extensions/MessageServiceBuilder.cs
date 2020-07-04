using _001HelloCore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _001HelloCore.Extensions
{
    //消息服务构建器,主要作用就是提供配置
    //构造器中就相当于定义好一系列的方法
    public class MessageServiceBuilder
    {
        //属性
        public IServiceCollection Services { get; set; }

        //构造函数
        public MessageServiceBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public void UserEmail()
        {
            Services.AddSingleton<IMessageService, EmailService>();
        }

        public void UserSms()
        {
            Services.AddSingleton<IMessageService, SmsService>();
        }
    }
}
