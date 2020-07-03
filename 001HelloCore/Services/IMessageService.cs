using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _001HelloCore.Services
{
    /// <summary>
    /// 消息借口
    /// </summary>
    public interface IMessageService
    {
        //发送消息
        void Send();
    }
}
