using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YinLong.Utils.Core.Config;

namespace CardService.Config
{
   public  class AppCfg : AppConfig<AppCfg>
    {
        public AppCfg()
        {
          
        }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        public bool SelfStart { get; set; }
        public int Handle { get; set; }
    }
}
