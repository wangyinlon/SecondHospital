using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Model
{
    public class LogEntity : System.EventArgs
    {
        public string Log { get; set; }
    }

    public class ResultEntity : System.EventArgs
    {
        public bool Success { get; set; }
        public int Count { get; set; }
    }
    public class DgvResultEntity : System.EventArgs
    {
        public string ColumnName { get; set; }
        public string Log { get; set; }
        public int Index { get; set; }
    }
}
