using System;
using clamsharp;

namespace testing
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (ClamEngine e = new ClamEngine())
			{
				ClamResult result = null;
				
				result = e.ScanFile("/home/bperry/tmp/eicar");
				
				if (result != null && result.ReturnCode == ClamReturnCode.CL_VIRUS)
					Console.WriteLine("Found: " + result.VirusName);
				else
					Console.WriteLine("File Clean!");
				
				result = e.ScanFile("/home/bperry/tmp/not_eicar");
				
				if (result != null && result.ReturnCode == ClamReturnCode.CL_VIRUS)
					Console.WriteLine("Found: " + result.VirusName);
				else
					Console.WriteLine("File Clean!");
			}
		}
	}
}
