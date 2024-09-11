using TcpServerApp.Handlers;
using TcpServerApp.Interface;

public class CommandProcessor : ICommandProcessor
{
    private PrinterCommand _printerCommand;

    public CommandProcessor()
    {
        _printerCommand = new PrinterCommand();
    }


    public CommandResponse ProcessCommand(byte[] commandBytes)
    {
        // Convert the byte array to a hexadecimal string representation
        string commandHex = BitConverter.ToString(commandBytes).Replace("-", string.Empty);

        // Execute the command and return the result
        return ExecuteCommand(commandHex);
    }



    private CommandResponse ExecuteCommand(string commandHex)
    {
        switch (commandHex)
        {
            // Initialization Commands
            case "01":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "System reset" };
            case "02":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Start Up/Shut Down executed" };
            case "03":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Keyboard locked" };
            case "04":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Keyboard unlocked" };

            // Communication Settings
            case "10":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Inter Message Delay Time set" };
            case "11":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Maximum Message Acknowledge Time configured" };
            case "12":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Transmission Retries set" };
            case "13":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Communication Errors logged" };
            case "14":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Print Unicode Text Message Response configured" };

            // Print Setup
            case "20":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Print Unicode Text Message executed" };
            case "21":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Message updated" };
            case "22":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Real Time Clock Setup completed" };
            case "25":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Queued messages cleared" };

            // Message Storage Control
            case "30":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Message Directory set" };
            case "31":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Message transferred" };
            case "32":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Message deleted" };
            case "33":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Logo Directory set" };
            case "34":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Logo transferred" };
            case "35":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Logo deleted" };
            case "36":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Attribute Block transferred" };
            case "37":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Message printed" };
            case "39":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "All messages printed offline" };
            case "3A":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Serial Number transferred" };
            case "3B":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Online Serial Number transferred" };

            // Machine Setup
            case "40":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Product Count set" };
            case "41":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Print-Go Delay (Nominal Registration) set" };
            case "42":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Global Bold set" };
            case "43":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Reverse set" };
            case "44":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Invert set" };
            case "45":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Run Hours configured" };
            case "46":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Month Names set" };
            case "47":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Day Names set" };
            case "48":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Hour Letters set" };
            case "49":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Dynamic Ink Data configured" };
            case "4A":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Printer Format set" };
            case "4B":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Repeat function configured" };
            case "4C":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Ink Data set" };
            case "4D":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Print Height set" };
            case "4E":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Stroke Setup configured" };
            case "4F":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "SGB Command executed" };
            case "50":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Late Message Configuration set" };

            // Service
            case "60":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Printer Identification retrieved" };
            case "61":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Printer Configuration retrieved" };
            case "62":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Program Update completed" };
            case "63":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Product Detect Level set" };
            case "64":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Product Detect Persistence set" };
            case "65":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Product Detect Response Time set" };
            case "66":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Product Configuration set" };
            case "67":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Status Mode Select set" };
            case "68":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Charge Level retrieved" };
            case "69":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Detect Threshold (Conduit Compensation) set" };
            case "6A":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Flight-Time Compensation set" };
            case "6B":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Modulation Mode set" };
            case "6C":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Modulation Setpoint set" };
            case "6D":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Modulation Setting Criterion set" };
            case "6F":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Break Off Time set" };
            case "70":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Characterize Jet executed" };
            case "72":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Modulation Level set" };
            case "76":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Ball Fall Time set" };
            case "7C":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Modify SGB Table (Raster Optimization Rig) executed" };

            // Remote Control
            case "81":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Print Enabled" };
            case "82":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Sensors read" };
            case "91":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Peltier Control executed" };
            case "95":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Machine State retrieved" };
            case "9B":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Memory dumped" };
            case "9E":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Ink Pressure set" };
            case "9F":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Fluid Level retrieved" };

            // Test
            case "B0":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Serial Number Test completed" };
            case "B1":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Beacon Test completed" };
            case "B2":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Key Test completed" };
            case "B3":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Display Test completed" };
            case "B4":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Dry Temp Test completed" };
            case "B5":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Pump Test completed" };
            case "B6":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "System Test completed" };
            case "B7":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Sensor Test completed" };
            case "B8":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Nozzle Test completed" };
            case "B9":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Acoustic Test completed" };
            case "BA":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Buzzer Test completed" };
            case "BB":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Laser Test completed" };
            case "BC":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Data Test completed" };

            // Status
            case "C0":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Operation Status retrieved" };
            case "C1":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Printer Status retrieved" };
            case "C2":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Print Head Status retrieved" };
            case "C3":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Ink Level retrieved" };
            case "C4":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Ink Quality retrieved" };
            case "C5":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Sensor Status retrieved" };
            case "C6":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Temperature Status retrieved" };
            case "C7":
                return new CommandResponse { IsSuccess = true, ResponseMessage = "Communication Status retrieved" };

            default:
                return new CommandResponse { IsSuccess = false, ResponseMessage = "Unknown command" };
        }
    }

}
