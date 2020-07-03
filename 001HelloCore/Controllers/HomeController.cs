using _001HelloCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace _001HelloCore.Controllers
{
    //假设是一个MVC或是API项目中的Controller
    public class HomeController
    {

        private IMessageService _messageService;

        //HomeController构造函数
        //这里是构造函数注入
        //其实我们想一想，在AutoFac中可以实现属性注入，我们可以定义一个IMessageService类型的属性，使用属性注入
        //但是.net Core自带的IOC容器好像不能实现属性注入（待测试）
        public HomeController(IMessageService messageService)
        {
            _messageService = messageService;
            //这里的messageService对象是IOC容器中注册的组件，IOC容器会自动把该对象注入到此处，即我们不需要在这里再创建messageService对象
        }
    }
}
