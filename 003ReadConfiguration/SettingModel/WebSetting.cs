using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _003ReadConfiguration.SettingModel
{
    public class WebSetting
    {
        public string WebName { get; set; }
        public string Title { get; set; }
        public Behavior Behavior { get; set; }
    }
}
