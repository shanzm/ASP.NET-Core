using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _001HelloCore.Services
{
    public class SmsService : IMessageService
    {
        public void Send()
        {
            Console.WriteLine("使用短信发送信息");
        }
    }
}
