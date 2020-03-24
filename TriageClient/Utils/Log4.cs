using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YinLong.Framework.Logs
{

    public class Log4
    {
        public static readonly ILog Loginfo = LogManager.GetLogger("loginfo");
        public static readonly ILog Logerror = LogManager.GetLogger("logerror");
        public static void GenerateLogForObject<T>(T obj)
        {
            ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            if (obj == null)
            {
                logger.Error("传入的类型未实例化.");
            }
            else
            {
                Type typeFromHandle = typeof(T);
                try
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    PropertyInfo[] properties = typeFromHandle.GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        PropertyInfo propertyInfo = properties[i];
                        stringBuilder.AppendFormat("Nmae='{0}',Value='{1}',TypeFullName='{2}'||\n\t", propertyInfo.Name.Trim(), Convert.ToString(propertyInfo.GetValue(obj, null)), propertyInfo.PropertyType.FullName);
                    }
                    logger.Debug(obj.ToString());
                    logger.Debug(stringBuilder.ToString());
                }
                catch (Exception)
                {
                }
                finally
                {
                }
            }
        }
        public static void InfoWriteToLogFile(string text)
        {
            ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Info(text);
        }
        public static void WriteLog(string info)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Debug(info);
            logger.Error(info);
            logger.Fatal(info);
            logger.Warn(info);
            logger.Info(info);
        }
        public static void WriteLog(string info, Exception se)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Debug(info);
            logger.Error(info);
            logger.Fatal(info);
            logger.Warn(info);
        }
        public static void Debug(object log)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Debug(log);
        }
        public static void Debug(object log, Exception ex)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Debug(log, ex);
        }
        public static void Info(object log)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Info(log);
        }
        public static void Info(object log, Exception ex)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Info(log, ex);
        }
        public static void Warn(object log)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Warn(log);
        }
        public static void Warn(object log, Exception ex)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Warn(log, ex);
        }
        public static void Error(object log)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Error(log);
        }
        public static void Error(object log, Exception ex)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Error(log, ex);
        }
        public static void Fatal(object log)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Fatal(log);
        }
        public static void Fatal(object log, Exception ex)
        {
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            ILog logger = LogManager.GetLogger(declaringType);
            logger.Fatal(log, ex);
        }
    }

}
