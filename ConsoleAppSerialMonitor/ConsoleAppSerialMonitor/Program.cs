using System.Configuration;
using System.IO.Ports;

namespace ConsoleAppSerialMonitor
{
    class Program
    {
        private static readonly string _commPort = System.Configuration.ConfigurationManager.AppSettings["CommPort"];
        private static readonly int _baudRate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["BaudRate"]);
        private static readonly string _parityValue = System.Configuration.ConfigurationManager.AppSettings["Parity"];
        private static readonly Parity _parity = (Parity)Enum.Parse(typeof(Parity), _parityValue);
        private static readonly int _dataBits = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DataBits"]);
        private static readonly string _stopBitsValue = System.Configuration.ConfigurationManager.AppSettings["StopBits"];
        private static readonly StopBits _stopBits = (StopBits)Enum.Parse(typeof(StopBits), _stopBitsValue);

        private static SerialPort _port = new SerialPort(portName: _commPort, baudRate: _baudRate, parity: _parity, dataBits: _dataBits, stopBits: _stopBits);

        static void Main()
        {
            bool _loop = false;
            while (_loop == false)
            {
                try
                {
                    _loop = SerialPortProgram.ReadSerialPort(_port);
                    if (_loop)
                    {
                        Main();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

    }

    class SerialPortProgram
    {
        public static bool ReadSerialPort(SerialPort port)
        {
            bool _condition = false;
            port.Open();
            while (_condition == false)
            {
                string _line = port.ReadLine();
                Console.WriteLine(_line);
                if (_line == "I'm down.\r")
                {
                    _condition = true;
                }
            }
            port.Close();
            return _condition;
        }
    }
}
