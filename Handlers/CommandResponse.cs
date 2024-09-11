using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerApp.Handlers
{
    public class CommandResponse
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public string RawResponse { get; set; }
    }
}
