using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Code;
using Flurl;
using Flurl.Http;
using OpenAuth.Repository.Domain;
using RestSharp;
using TriageClient.Model;
using YinLong.Framework;

namespace TriageClient
{
    public class Apis
    {
        HttpHelperMin _helper = new HttpHelperMin();
        HttpItemMin _item = new HttpItemMin();
        HttpResultMin _result = new HttpResultMin();
        /// <summary>
        /// 医生端获取已经签到得患者0普通，1专家，2复查
        /// </summary>
        /// <param name="ysdm"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<OUTP_JZJLK> QuerySignPatiend(string ysdm, string date)
        {
            try
            {
                _item.URL = ConfigurationManager.AppSettings["ApiHost"] + $"/HisApi/Triage/QuerySignPatiend?ysdm={ysdm}&date={date}";

                _item.Encoding = Encoding.UTF8;
                _result = _helper.GetHtml(_item);
                YinLong.Framework.Logs.Log4.Debug("[QuerySignPatiend]" + _result.Html);
                Regex regex = new Regex("\"GHXH\":(.*?),.*?\"PATID\":(.*?),\".*?\"HZXM\":\"(.*?)\".*?\"GHLB\":(.*?),.*?\"JLZT\":(.*?)}");//初始化正则对象 
                MatchCollection mc = regex.Matches(_result.Html);//匹配;
                if (mc.Count == 0)
                {
                    return null;
                }
                List<OUTP_JZJLK> result = new List<OUTP_JZJLK>();
                for (int ic = 0; ic < mc.Count; ic++)
                {
                    result.Add(new OUTP_JZJLK()
                    {
                        GHXH = mc[ic].Groups[1].Value,
                        PATID = mc[ic].Groups[2].Value,
                        HZXM = mc[ic].Groups[3].Value,
                        GHLB = mc[ic].Groups[4].Value,
                        PatientState = mc[ic].Groups[5].Value == "0" ? Configs.State_DengDaiJiaoHoa : Configs.State_YiJiaoHoa
                    });
                }
                YinLong.Framework.Logs.Log4.Debug("[QuerySignPatiend],[ysdm]" + ysdm + ",[返回]:" + _result.Html);
                return result;
                //var client = new RestClient("http://200.200.200.104:9963");
                //var request = new RestRequest("HisApi/Triage/QuerySignPatiend?ysdm=" + ysdm + "&date=" + date, Method.GET);
                //request.AddHeader("Content-Type","application/json; charset=utf-8");
                //request.AddHeader("Accept", "text/plain");
                //client.Encoding = Encoding.GetEncoding("UTF-8");

                //var t = client.Execute<ApiRespone<List<OUTP_JZJLK>>>(request);
                //return t.Data.Result;
            }
            catch (Exception e)
            {
                YinLong.Framework.Logs.Log4.Error("[QuerySignPatiend],[ysdm]" + ysdm + ",[异常]:" + e);

                return null;
            }


        }

        /// <summary>
        /// 叫号
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="GHXH"></param>
        /// <returns></returns>
        public bool PutPatiendCall(string patid, string GHXH)
        {
            try
            {
                _item.URL = ConfigurationManager.AppSettings["ApiHost"] + $"/HisApi/Triage/PutPatiendCall?patid={patid.Replace(".0", "")}&GHXH={GHXH.Replace(".0", "")}";

                _item.Encoding = Encoding.UTF8;
                _result = _helper.GetHtml(_item);
                YinLong.Framework.Logs.Log4.Debug("[PutPatiendCall],[patid]" + patid + ",[返回]:" + _result.Html);
                if (_result.Html.Contains("\"Code\":200"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                YinLong.Framework.Logs.Log4.Error("[PutPatiendCall],[patid]" + patid + ",[异常]:" + e);
                return false;

            }


        }

        public async Task<ApiRespone<QueryDocLoginModel>> QueryDocLogin(string ysdm, string pwd)
        {
            try
            {
                //Thread.Sleep(5000);
                var responseString = await (ConfigurationManager.AppSettings["ApiHost"] + "/HisApi/Triage/QueryDocLogin")
                    .SetQueryParams(new { ysdm = ysdm, pwd = pwd })
                  .GetJsonAsync<ApiRespone<QueryDocLoginModel>>();
                return responseString;
            }
            catch (Exception e)
            {
                YinLong.Framework.Logs.Log4.Error("[QueryDocLogin异常]:" + e.ToString());
                return null;
            }
        }

        #region 广播
        /// <summary>
        /// 广播服务
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public bool Call(string msg)
        {
            try
            {
                msg = "请" + msg + "到" + Configs.QueryDocLoginModel.Result.ZJMC + "就诊";
                _item.URL = ConfigurationManager.AppSettings["CallHost"] + "/call?msg=" + System.Web.HttpUtility.UrlEncode(msg, Encoding.UTF8);
                _item.Encoding = Encoding.UTF8;
                _result = _helper.GetHtml(_item);
                YinLong.Framework.Logs.Log4.Debug("[Call],[msg]" + msg + ",[返回]:" + _result.Html);
                if (_result.Html.Contains("\"code\":200"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                YinLong.Framework.Logs.Log4.Error("[Call],[msg]" + msg + ",[异常]:" + e);
                return false;

            }


        }

        #endregion
    }
}
