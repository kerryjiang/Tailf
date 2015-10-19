using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Tailf
{
    public  class CommandLineParser
    {
        List<string> fileNames;
        List<string> warnings;
        public IEnumerable<string> FileNames { get { return fileNames; } }
        public IEnumerable<string> Warnings { get { return warnings; } }
        string[] cmdline;
        List<OptionAttribute> options = new List<OptionAttribute>();
        private void AddOptions( OptionAttribute option)
        {
            this.options.Add(option);
        }

        

        public CommandLineParser(string[] cmdline)
        {
            this.cmdline = cmdline;
            warnings = new List<string>();
            fileNames = new List<string>();
        }
        public void Parse()
        {
            ExtractOptions();
            foreach (string s in cmdline)
            {
                if (s.StartsWith("-")) // option
                {
                    if (s.Substring(1).Contains(':'))
                    {
                        string[] tokens = s.Substring(1).Split(':');
                        string rhs = s.Substring(1);
                        ParseOption(tokens[0],rhs.Substring(rhs.IndexOf(':')+1) );
                    }
                    else
                    {
                        throw new Exception(string.Format("Invalid option: {0}. Must be in the form -x:xxxxx"));
                    }
                }
                else
                {
                    fileNames.Add(s);
                }
            }
            CheckMandatory();
        }
        public string GetHelp()
        {
            StringBuilder sb = new StringBuilder();
            foreach (OptionAttribute opt in options)
            {
                sb.Append("\n -");
                sb.Append(opt.Flag);
                sb.Append("\t");
                sb.Append(opt.LongHelp);
                if (opt.Optional)
                    sb.Append("\t\t(optional)");
                else
                    sb.Append("\t\t(mandatory)");
            }
            return sb.ToString();
        }
        public string GetShortHelp()
        {
            StringBuilder sb = new StringBuilder();
            foreach (OptionAttribute opt in options)
            {
                if (opt.Optional)
                {
                    sb.Append(" [");
                }
                else
                {
                    sb.Append(" <");
                }
                sb.Append("-");
                sb.Append(opt.Flag);
                sb.Append(":");
                sb.Append(opt.ShortHelp);
                if (opt.Optional)
                {
                    sb.Append("]");
                }
                else
                {
                    sb.Append(">");
                }
            }
            return sb.ToString();
        }
        private void CheckMandatory()
        {
            bool error = false;
            foreach (OptionAttribute opt in options)
            {
                if (opt.Optional == false && opt.Given == false)
                {
                    error = true;
                    break;
                }
            }
            if (error == true)
            {
                throw new NotEnougthParametersException();
            }

        }

        private void ExtractOptions()
        {
            PropertyInfo[] pis = GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                OptionAttribute[] options = pi.GetCustomAttributes(typeof(OptionAttribute),true) as OptionAttribute[];
                if (options.Length == 1)
                {
                    options[0].SetPropertyInfo(pi);
                    AddOptions(options[0]);
                    if (options[0].Optional)
                    {
                        if (!string.IsNullOrEmpty(options[0].Default))
                            pi.SetValue(this, Convert.ChangeType(options[0].Default, pi.PropertyType), null);
                    }
                }
            }
        }

        private void ParseOption(string option, string value)
        {
            if (!options.Any(k => k.Flag == option))
                warnings.Add("Ignoring unknown option:" + option);
            else
            {
                OptionAttribute opt = options.Where(k => k.Flag == option).First();
                PropertyInfo pi = opt.GetPropertyInfo();
                pi.SetValue(this, Convert.ChangeType(value, pi.PropertyType), null);
                opt.Given = true;
            }
        }
    }
}
