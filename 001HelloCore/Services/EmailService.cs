using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _001HelloCore.Services
{
    public class EmailService : IMessageService
    {
        /// <summary>
        /// 使用邮件发送信息
        /// </summary>
        public void Send()
        {
            Console.WriteLine("使用电子邮件发送信息");
        }
    }
}
