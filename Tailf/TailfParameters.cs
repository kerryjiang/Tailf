using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tailf
{
    public class TailfParameters : CommandLineParser
    {
        public TailfParameters(string[] args)
            :base(args)
        {

        }
        
        [Option("n", LongHelp = "Lines to show at the end of the files", Optional = true, ShortHelp = "lines count")]
        public string NOfLines { get; set; }
        [Option("f", LongHelp = "Regular expression that match a portion of the line to accept it", Optional = true, ShortHelp = "filter regex")]
        public string Filter { get; set; }
        [Option("l", LongHelp = "Regular expression which is used for finding the log level", Optional = true, ShortHelp = "level regex")]
        public string LevelRegex { get; set; }
        [Option("c", LongHelp = "The text color for logs of each log level", Optional = true, ShortHelp = "color map")]
        public string ColorMap { get; set; }
    }
}
