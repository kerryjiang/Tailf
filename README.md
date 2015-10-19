# Tailf

Tailf is a C# implementation of the tail -f command available on unix/linux systems. Differently form other ports it does not lock the file in any way so it works even if other rename the file: this is expecially designed to works well with log4net rolling file appender.

Clone from Codeplex:
http://tailf.codeplex.com/
