using System;
using clamsharp;

namespace testing
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (Engine e = new Engine())
			{
				ClamReturnCode ret = e.ScanFile("/home/bperry/tmp/eicar");
				ret = e.ScanFile("/home/bperry/tmp/not_eicar");
			}
		}
	}
}
