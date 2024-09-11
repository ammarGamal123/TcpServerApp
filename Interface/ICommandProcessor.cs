using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpServerApp.Handlers;

namespace TcpServerApp.Interface
{
    public interface ICommandProcessor
    {
        // Processes the recieved commands
        CommandResponse ProcessCommand(byte[] commandBytes);

        
    }
}
