﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _003ReadConfiguration.SettingModel
{
    public class AppSetting
    {
        public string ConnectionString { get; set; }
        public WebSetting WebSetting { get; set; }
    }
}
