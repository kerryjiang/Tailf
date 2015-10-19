using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tailf
{
    class Program
    {
        private const short DefaultLines = 5;
        static void Main(string[] args)
        {
            TailfParameters prms = new TailfParameters(args);
            try
            {
                prms.Parse();
                if (prms.FileNames.Count() == 0)
                {
                    throw new Exception("At least one file must be specified");
                }
                if (prms.FileNames.Count() > 1)
                {
                    throw new Exception("At most one file must be specified");
                }
                int n = 0;
                int.TryParse(prms.NOfLines, out n);
                if (n == 0)
                    n = DefaultLines;
                Tail tail = new Tail(prms.FileNames.First(), n);
                tail.LineFilter = prms.Filter;
                tail.Changed += new EventHandler<Tail.TailEventArgs>(tail_Changed);
                tail.Run();
                Console.ReadLine();
                tail.Stop();
            }
            catch (NotEnougthParametersException)
            {
                Console.Error.WriteLine("Use:");
                Console.Error.WriteLine("Tailf " + prms.GetShortHelp()+" <filename>");
                Console.Error.WriteLine(prms.GetHelp());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Fatal error:" + e.Message);
            }
        }

        static void tail_Changed(object sender, Tail.TailEventArgs e)
        {
            Console.Write(e.Line);
        }
    }
}
