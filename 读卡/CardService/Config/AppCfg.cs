using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public int Port { get; set; } = 9999;
        public bool SelfStart { get; set; }
        public int Handle { get; set; }
    }
}
