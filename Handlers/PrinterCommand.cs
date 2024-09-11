using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerApp.Handlers
{
    public class PrinterCommand
    {
        public static Dictionary<string, string> Commands { get; } = new Dictionary<string, string>
        {
            // Initialization Commands
            { "Reset System", "01" },
            { "Start Up/Shut Down", "02" },
            { "Keyboard Lock", "03" },
            { "Keyboard Unlock", "04" },

            // Communication Settings
            { "Inter Message Delay Time", "10" },
            { "Maximum Message Acknowledge Time", "11" },
            { "Transmission Retries", "12" },
            { "Communication Errors", "13" },
            { "Configure Print Unicode Text Message Response", "14" },

            // Print Setup
            { "Print Unicode Text Message", "20" },
            { "Update Message", "21" },
            { "Real Time Clock Setup", "22" },
            { "Clear Queued Messages", "25" },

            // Message Storage Control
            { "Message Directory", "30" },
            { "Transfer Message", "31" },
            { "Delete Message", "32" },
            { "Logo Directory", "33" },
            { "Transfer Logo", "34" },
            { "Delete Logo", "35" },
            { "Transfer Attribute Block", "36" },
            { "Message Print", "37" },
            { "All Messages Offline", "39" },
            { "Serial Number Transfer", "3A" },
            { "Online Serial Number Transfer", "3B" },

            // Machine Setup
            { "Product Count", "40" },
            { "Print-Go Delay (Nominal Registration)", "41" },
            { "Global Bold", "42" },
            { "Reverse", "43" },
            { "Invert", "44" },
            { "Run Hours", "45" },
            { "Month Names", "46" },
            { "Day Names", "47" },
            { "Hour Letters", "48" },
            { "Dynamic Ink Data", "49" },
            { "Printer Format", "4A" },
            { "Repeat", "4B" },
            { "Ink Data", "4C" },
            { "Print Height", "4D" },
            { "Stroke Setup", "4E" },
            { "SGB Command", "4F" },
            { "Late Message Configuration", "50" },

            // Service
            { "Printer Identification", "60" },
            { "Printer Configuration", "61" },
            { "Program Update", "62" },
            { "Product Detect Level", "63" },
            { "Product Detect Persistence", "64" },
            { "Product Detect Response Time", "65" },
            { "Product Configuration", "66" },
            { "Status Mode Select", "67" },
            { "Charge Level", "68" },
            { "Detect Threshold (Conduit Compensation)", "69" },
            { "Flight-Time Compensation", "6A" },
            { "Modulation Mode", "6B" },
            { "Modulation Setpoint", "6C" },
            { "Modulation Setting Criterion", "6D" },
            { "Break Off Time", "6F" },
            { "Characterize Jet", "70" },
            { "Modulation Level", "72" },
            { "Ball Fall Time", "76" },
            { "Modify SGB Table (Raster Optimization Rig)", "7C" },

            // Remote Control
            { "Print Enable", "81" },
            { "Read Sensors", "82" },
            { "Peltier Control", "91" },
            { "Machine State", "95" },
            { "Dump Memory", "9B" },
            { "Ink Pressure", "9E" },
            { "Fluid Level", "9F" },

            // Test
            { "Serial Number Test", "B0" },
            { "Beacon Test", "B1" },
            { "Key Test", "B2" },
            { "Display Test", "B3" },
            { "Dry Temp Test", "B4" },
            { "Pump Test", "B5" },
            { "IO Test", "B6" },
            { "Heat Test", "B7" },
            { "Liquid Test", "B8" },
            { "Circulate Test", "B9" },
            { "Print Head Test", "BA" },
            { "Print Window Test", "BB" },
            { "Fault Log", "BC" },
            { "HV Test", "BD" },
            { "Ramp Test", "BE" },
            { "Auto-modulation Test", "BF" },
            { "Modulation Window Test", "C0" },
            { "Pressure Window Test", "C1" },

            // File System
            { "File Transfer", "A0" },
            { "Catalogue Files", "A1" },
            { "Delete File", "A2" },
            { "Rename File", "A3" },

            // Unsolicited Status
            { "Print Acknowledge", "E0" },
            { "Print Failed", "E1" },
            { "Extended Status", "E2" },
            { "Printer Alert", "E3" },

            // Status and Alert
            { "Get Status", "E8" },
            { "Get Alert List", "E9" },

            // Miscellaneous
            { "Control Stream", "FC" },
            { "Boot Mode", "FD" },
            { "Start-up Stream", "FE" },
            { "Debug", "FF" }
        };

        public IEnumerable<KeyValuePair<string, string>> GetAllCommands()
        {
            return Commands;
        }
    }

}
