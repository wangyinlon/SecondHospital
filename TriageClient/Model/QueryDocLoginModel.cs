using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriageClient.Model
{
   public class QueryDocLoginModel
    {
        /// <summary>
        /// 诊间代码
        /// </summary>
        public string ZJDM { get; set; }
        /// <summary>
        /// 诊间名称
        /// </summary>
        public string ZJMC { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string KSMC { get; set; }

        /// <summary>
        /// 就诊医师代码
        /// </summary>
        public string ZJYSDM { get; set; }

        /// <summary>
        /// 就诊医师名称
        /// </summary>
        public string ZJYSMC { get; set; }

        public string IPDZ { get; set; }

        public string SEX { get; set; }

        public string DLSJ { get; set; }
    }
}
