//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using SufeiUtil;

//namespace WebAppReadCard.Utils
//{
//    public class Configs
//    {
       

//        private static volatile INIFileHelper _connection = null;
//        public static INIFileHelper Instance()
//        {
//            if (_connection == null)
//            {
//                lock (typeof(Configs))
//                {
//                    if (_connection == null) // double-check
//                    {
//                        _connection = new INIFileHelper(AppDomain.CurrentDomain.BaseDirectory + "/bin/WebAppReadCard.ini");
//                    }
//                }
//            }
//            return _connection;
//        }
//    }
//}