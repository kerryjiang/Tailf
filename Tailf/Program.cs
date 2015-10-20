using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tailf
{
    class Program
    {
        private const short DefaultLines = 5;

        private static Dictionary<string, ConsoleColor> colorMappingDict;

        private static ConsoleColor? prevColor;

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

                if(!string.IsNullOrEmpty(prms.ColorMap))
                {
                    colorMappingDict = prms.ColorMap.Split("};,".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                        .Select(m =>
                        {
                            var arr = m.Split("=".ToArray(), 2);
                            return new KeyValuePair<string, ConsoleColor>(arr[0], (ConsoleColor)Enum.Parse(typeof(ConsoleColor), arr[1]));
                        })
                        .ToDictionary(p => p.Key, p => p.Value, StringComparer.OrdinalIgnoreCase);
                }

                Tail tail = new Tail(prms.FileNames.First(), n);
                tail.LevelRegex = prms.LevelRegex;
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
            if(colorMappingDict != null)
            {
                ConsoleColor color;

                if(!colorMappingDict.TryGetValue(e.Level, out color))
                {
                    if (prevColor.HasValue)
                    {
                        prevColor = null;
                        Console.ResetColor();
                    }  
                }
                else if(!prevColor.HasValue || prevColor.Value != color)
                {
                    Console.ForegroundColor = color;
                    prevColor = color;
                }
                
            }

            Console.Write(e.Line);
        }
    }
}
