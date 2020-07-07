using _001HelloCore.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace _001HelloCore.Extensions
{
    //注意这是对IServiceCollection接口扩展，理论上应该命名为：IServiceCollectionExtension
    public static class MessageServiceExtension
    {
        //对IServiceCollection接口进行扩展，添加一个AddMessage方法
        //这样我们在StartUp.cs中就不需要写services.AddTransient<IMessageService, SmsService>();取而代之，可以写作：services.AddMessage()
        public static void AddMessage(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMessageService, SmsService>();
        }

        //这里首先我们添加一个构建器类MessageServiceBuilder，
        //在这个构建器类中我们创建一个用于配置不同服务的方法，
        //对IServiceCollection接口进行扩展，和上一个扩展方法不同的是我们添加了一个参数是MessageServiceBuilder类型的无返回值类型的委托
        //使用这个扩展方法，就没有必要在定义上面的那个扩展方法（但是两者并不冲突，所以我就没有注释掉上面的方法）
        public static void AddMessage(this IServiceCollection serviceCollection,Action<MessageServiceBuilder> options)
        {
            //创建构建器对象，作为委托的参数
            MessageServiceBuilder builder = new MessageServiceBuilder(serviceCollection);
            //调用委托
            options(builder);
        }
    }
}
