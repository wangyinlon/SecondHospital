using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YinLong.Utils.Core.Config;

namespace HttpServer.Config
{
   public class AppCfg : AppConfig<AppCfg>
    {
        public AppCfg()
        {
            Port = 9999;
 
        }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        public bool SelfStart { get; set; }
    }
}
