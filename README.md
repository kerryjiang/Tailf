# Tailf

Tailf is a C# implementation of the tail -f command available on unix/linux systems. Differently form other ports it does not lock the file in any way so it works even if other rename the file: this is expecially designed to works well with log4net rolling file appender.

You will probably use tailf to monitor log files in order to see new messages as soon they are enqueued. Additionally a filter could be added to show only the lines containing a pattern matching a given regular expression. The usual log4net rolling file appender is not prevented to created regular backup when tailf monitoring is active.


* **tailf mylog.txt** continuously dump on the console the content of **mylog.txt** as soon new lines are written into it. 
* **tailf -n:15 mylog.txt** continuously dump on the console the content of  **mylog.txt** as soon new lines are written into it. At startup the last 15 lines are dumped. 
* **tailf mylog.txt -f:ERROR** continuously dump on the console the content of **mylog.txt** as soon new lines are written into it. Just lines containing  **"ERROR"** are shown. 
* **tailf mylog.txt -f:"ERROR|WARN"** continuously dump on the console the content of **mylog.txt** as soon new lines are written into it. Just lines containing  **"ERROR"** or **"WARN"** are shown; double quotes are necessary since | is a special char in the command shell.
* **tailf mylog.txt  -l:"\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2},\d{3} \[\d+\] (?&lt;level&gt;\w{4,5})" -c:WARN=Yellow;ERROR=Red;FATAL=Red** continuously dump on the console the content of **mylog.txt** with different colors according log level as soon new lines are written into it. The parameter **-l** is the regex expression for extracting log level, and the parameter **-c** is the color mapping definition from level to color.


Clone from the project Tailf of Felix Pollan in Codeplex:
http://tailf.codeplex.com/
