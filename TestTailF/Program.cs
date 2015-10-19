using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;
using log4net;
using System.Threading;

namespace TestTailF
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            int i=0;
            while (true)
            {
                var lg = LogManager.GetLogger(typeof(Program));
                lg.Info("This is a message " + i++);
                lg.Error("This is an error " + i++);
                lg.Warn("This is a warning " + i++);
                Thread.Sleep(10);
            }
        }
    }
}
