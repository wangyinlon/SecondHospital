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
            IpKeepTime = 24;
            ThreadNum = 1;
        }

        public int ThreadNum { get; set; }
        public string AdslName { get; set; }
        public string AdslPass { get; set; }
        public string AdslUser { get; set; }
        public int TypeSpit { get; set; }
        public string SpitOneself { get; set; }
        public int IpKeepTime { get; set; }
        public int StopTime { get; set; }
        public int StopNum { get; set; }
        public int ADSLTime { get; set; }
        public int AdslNum { get; set; }

        public string DcUser { get; set; }
        public string DcPass { get; set; }
    }
}
