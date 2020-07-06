using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _001HelloCore
{
    //这个类就是自定义的中间件

    //这里首先就有一个疑问？为什么要自定义中间件，
    //其实我们编写WEB应用，实际上就是在写中间件，比如说你写MVC中的控制器，而控制器就是中间件的一部分
    //整个.net Core应用，都是在管道里运行的，而管道中放的就是中间件。

    //创建中间件需要按照约定来创建
    //属性名这个中间件需要有一个构造函数，该构造函数有一个RequestDelegate类型的参数
    public class TestMiddleware
    {
        //定义一个私有只读字段，存储RequestDelegate对象
        private readonly RequestDelegate _next;

        //构造函数
        public TestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //中间件的功能代码就是写在这里，这里是支持使用方法注入的（即：可以使用IOC中的对象）
        public async Task InvokeAsync(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync("create Middleware by myself");
            //可以选择是否调用next,如果这里不调用，那么这个中间件会触发短路
            await _next(httpContext);
        }
    }
}
