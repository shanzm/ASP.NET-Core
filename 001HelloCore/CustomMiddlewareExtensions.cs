using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _001HelloCore
{
    //该扩展类扩展的是IApplicationBuilder接口
    //在该接口中添加方法用于配置用户自定义的中间件
    public static  class CustomMiddlewareExtensions
    {
        //添加UseTest方法，用于配置TestMiddleware中间件
        public static IApplicationBuilder UseTest(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TestMiddleware>();
        }
    }
}
