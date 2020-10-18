using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string path = @"E:\Task2\File.txt";
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                var newLog = new List<Log>();
                string line;
                int i = 0;

                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (Regex.Match(line, @"[|]").Length > 0)
                    {
                        newLog.Add(new Log()
                        {
                            timeStamp = DateTime.Parse(Regex.Match(line, @"\d{2}/\d{2}/\d{2}\s\d{2}:\d{2}:\d{2}.\d{3}").Value),
                            logLevel = Regex.Match(line, @"[A-Z]{4,5}").Value,
                            thread = Convert.ToInt32(Regex.Match(line, @"\s\d{1,2}\s").Value),
                            processName = Regex.Match(line, @"Con(\w*)").Value,
                            methodName = Regex.Match(line, @"\s[A-Z]\w*[`]*\w*[.]\S+\s").Value,
                            message = Regex.Replace(line, @"\d{2}/\d{2}/\d{2}\s\d{2}:\d{2}:\d{2}.\d{3}\s[|]\s[A-Z]{4,5}\s*[|]\s\d{1,2}\s+.+Con(\w*)\s*[|]\s[A-Z]\w*[`]*\w*[.]\S+\s", "")
                        });
                    }
                }

                newLog = newLog.OrderBy(l => l.thread).ToList();
                List<Method> methods = new List<Method>();

                foreach (Log l in newLog)
                {
                    if (methods.Any(m => m.methodName == l.methodName) == false)
                    {
                        methods.Add(new Method()
                        {
                            methodName = l.methodName,
                            executionTime = 0
                        });
                    }
                }

                i = 0;
                while (i < newLog.Count)
                {
                    if (i < newLog.Count - 1)
                    {
                        if (newLog[i].thread == newLog[i + 1].thread)
                        {
                            int index = methods.FindIndex(a => a.methodName == newLog[i].methodName);
                            methods[index].executionTime += (newLog[i + 1].timeStamp)
                                .Subtract(newLog[i].timeStamp).TotalSeconds;
                        }
                    }
                    i++;
                }

                methods = methods.OrderByDescending(a => a.executionTime).ToList();

                i = 0;
                while (i < 10)
                {
                    methods[i].GetInfo();
                    i++;
                }
            }
        }
    }
}

