using System;

namespace Task1
{
    class Log
    {
        public DateTime timeStamp { get; set; }
        public string logLevel { get; set; }
        public int thread { get; set; }
        public string processName { get; set; }
        public string methodName { get; set; }
        public string message { get; set; }

        public void GetInfo()
        {
            Console.WriteLine($"Time: {timeStamp.ToString("dd/MM/yy hh:mm:ss.fff")} | Thread #: {thread}" +
                              $" | Method name with class name:{methodName}\n");
        }

    }
}