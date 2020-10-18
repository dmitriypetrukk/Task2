using System;
using System.Text.RegularExpressions;

namespace Task1
{
    class Method
    {
        public string methodName { get; set; }
        public double executionTime { get; set; }
        public void GetInfo()
        {
            Console.WriteLine($"Method name:{methodName}| Execution time (s): " +
                              $"{Regex.Match(executionTime.ToString(), @"\d+([,]\d)?\d?").Value}\n");
        }
    }
}