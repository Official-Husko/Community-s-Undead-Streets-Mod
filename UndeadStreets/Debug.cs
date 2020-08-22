using System;
using System.IO;

namespace CWDM
{
	public class Debug
	{
		public static void Log(String message)
		{
			File.AppendAllText("./scripts/CWDM/Logs/CWDM.log", DateTime.Now + " : " + message + Environment.NewLine);
		}
	}
}