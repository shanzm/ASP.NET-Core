using System;
using System.Threading.Tasks;

namespace PipelineDemo
{
    public delegate Task RequestDelegate(HttpContext context);

    class Program
    {
        static void Main(string[] args)
        {
            var app = new ApplicationBuilder();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("中间件1号 Begin");
                await next();
                Console.WriteLine("中间件1号 End");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("中间件2号 Begin");
                await next();
                Console.WriteLine("中间件2号 End");
            });

            // 这时候管道已经形成，执行第一个中间件，就会依次调用下一个
            // 主机创建以后运行的
            var firstMiddleware = app.Build();

            // 当请求进来的时候，就会执行第一个中间件
            // 主机给的
            firstMiddleware(new HttpContext());
        }
    }
}
