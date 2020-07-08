using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDemo
{
    public class ApplicationBuilder
    {
        // 里面放的不是真正的中间件，中间件的委托
        private static readonly IList<Func<RequestDelegate, RequestDelegate>> _components = 
            new List<Func<RequestDelegate, RequestDelegate>>();

        // 扩展Use
        public ApplicationBuilder Use(Func<HttpContext, Func<Task>, Task> middleware)
        {
            return Use(next =>
            {
                return context =>
                {
                    Task SimpleNext() => next(context);
                    return middleware(context, SimpleNext );
                };
            });
        }

        // 原始Use
        public ApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            // 添加中间件
            _components.Add(middleware);
            return this;
        }
	
        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                Console.WriteLine("默认中间件");
                return Task.CompletedTask;
            };

            // 上面的代码是一个默认的中间件
            // 重要的是下面几句，这里对Func<RequestDelegate, RequestDelegate>集合进行反转，
            // 逐一执行添加中间件的委托，最后返回第一个中间件委托
            // 这里的作用就是把list里独立的中间件委托给串起来，然后返回反转后的最后一个中间件（实际上的第一个）
            
            // 管道才真正的建立起来，每一个中间件都首尾相连
            // 兜底中间件传给2号中间件的委托，包含有链子的中间件就被创建出来，把2号中间件返回出来
            foreach (var component in _components.Reverse())
            {
                app = component(app);
            }

            return app;
        }
    }
}
