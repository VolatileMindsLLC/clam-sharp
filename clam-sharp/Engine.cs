using System;
using System.Runtime.InteropServices;

namespace clamsharp
{
	public class Engine : IDisposable
	{
		private ClamEngine engine;
		public Engine ()
		{
			int ret = ClamBindings.cl_init((uint)ClamDatabaseOptions.CL_DB_STDOPT);
			
			if (ret != (int)ClamReturnCode.CL_SUCCESS)
				throw new Exception("Expected CL_SUCCESS, got " + (ClamReturnCode)ret);
			
			engine = ClamBindings.cl_engine_new();
			
			string c = Marshal.PtrToStringAnsi(ClamBindings.cl_retdbdir());
			uint signo = 0;
			ret = ClamBindings.cl_load(c, engine, ref signo,(uint)ClamScanOptions.CL_SCAN_STDOPT);
			
			if (ret != (int)ClamReturnCode.CL_SUCCESS)
				throw new Exception("Expected CL_SUCCESS, got " + (ClamReturnCode)ret);
			
			ret = ClamBindings.cl_engine_compile(engine);
			
			if (ret != (int)ClamReturnCode.CL_SUCCESS)
				throw new Exception("Expected CL_SUCCESS, got " + (ClamReturnCode)ret);
		}
		
		public ClamEngine EnginePointer { get { return engine; } }
		
		public ClamReturnCode ScanFile(string filepath)
		{
			ulong scanned = 0;
			IntPtr vname = (IntPtr)null;
			ClamReturnCode ret = (ClamReturnCode)ClamBindings.cl_scanfile(filepath, ref vname, ref scanned, engine, (uint)ClamScanOptions.CL_SCAN_STDOPT);
			
			if (ret == ClamReturnCode.CL_VIRUS)
			{
				string virus = Marshal.PtrToStringAnsi(vname);
				Console.WriteLine("Found virus: " + virus);
			}
			else
				Console.WriteLine("File clean!");
			
			return ret;
		}
		
		public void Dispose()
		{
			ClamBindings.cl_engine_free(ref engine);
		}
	}
}

