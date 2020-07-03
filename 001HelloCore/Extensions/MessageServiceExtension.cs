using _001HelloCore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace _001HelloCore.Extensions
{
    public static class MessageServiceExtension
    {
        //对IServiceCollection接口进行扩展，添加一个AddSingleton<IMessageService, SmsService>()方法
        public static void AddMessage(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMessageService, SmsService>();
        }
    }
}
