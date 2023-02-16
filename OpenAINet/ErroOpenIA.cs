using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OpenAINet
{
    public class ErroOpenIA
    {
        public Error Error { get; set; }
    }
    public class Error
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public object Param { get; set; }
        public string Code { get; set; }
    }
}
