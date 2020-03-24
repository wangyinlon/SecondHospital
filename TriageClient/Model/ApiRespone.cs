using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriageClient.Model
{
  public class ApiRespone<T>
    {
        public string Code { get; set; }
        public string RequestId { get; set; }
        public T Result { get; set; }
        public string message { get; set; }

    
    }
}
