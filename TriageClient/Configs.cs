using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriageClient.Model;

namespace TriageClient
{
   public class Configs
   {
       /// <summary>
       /// 出诊
       /// </summary>
       public static string PatientStateDefault = "等待叫号";

       public static string State_JiaoHaoFail = "叫号失败,请稍后重试！";
       public static string State_YiJiaoHoa = "已叫号";
       public static string State_DengDaiJiaoHoa = "等待叫号";

        public static ApiRespone<QueryDocLoginModel> QueryDocLoginModel { get; set; }
    }
}
