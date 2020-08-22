using System;
using System.IO;

namespace CWDM
{
    public static class Log
    {
        public static void Write(string message)
        {
            if (!Directory.Exists("./scripts/CWDM/"))
            {
                Directory.CreateDirectory("./scripts/CWDM/");
            }
            if (!Directory.Exists("./scripts/CWDM/Logs/"))
            {
                Directory.CreateDirectory("./scripts/CWDM/Logs/");
            }
            File.AppendAllText("./scripts/CWDM/Logs/CWDM.log", DateTime.Now + " : " + message + Environment.NewLine);
        }
    }
}
