using System;
using System.Runtime.InteropServices;

namespace clamsharp
{
	public class ClamEngine : IDisposable
	{
		private ClamEngineDescriptor engine;
		
		public ClamEngine ()
		{
			ClamReturnCode ret = (ClamReturnCode)ClamBindings.cl_init((uint)ClamDatabaseOptions.CL_DB_STDOPT);
			
			if (ret != ClamReturnCode.CL_SUCCESS)
				throw new Exception("Expected CL_SUCCESS, got " + ret);
			
			engine = ClamBindings.cl_engine_new();
			
			string c = Marshal.PtrToStringAnsi(ClamBindings.cl_retdbdir());
			uint signo = 0;
			ret = (ClamReturnCode)ClamBindings.cl_load(c, engine, ref signo,(uint)ClamScanOptions.CL_SCAN_STDOPT);
			
			if (ret != ClamReturnCode.CL_SUCCESS)
				throw new Exception("Expected CL_SUCCESS, got " + ret);
			
			ret = (ClamReturnCode)ClamBindings.cl_engine_compile(engine);
			
			if (ret != ClamReturnCode.CL_SUCCESS)
				throw new Exception("Expected CL_SUCCESS, got " + ret);
		}
		
		public ClamResult ScanFile(string filepath)
		{
			ulong scanned = 0;
			IntPtr vname = (IntPtr)null;
			ClamReturnCode ret = (ClamReturnCode)ClamBindings.cl_scanfile(filepath, ref vname, ref scanned, engine, (uint)ClamScanOptions.CL_SCAN_STDOPT);
			
			if (ret == ClamReturnCode.CL_VIRUS)
			{
				string virus = Marshal.PtrToStringAnsi(vname);
				
				ClamResult result = new ClamResult();
				result.ReturnCode = ret;
				result.VirusName = virus;
				
				return result;
			}
			
			return null;
		}
		
		public void Dispose()
		{
			ClamReturnCode ret = (ClamReturnCode)ClamBindings.cl_engine_free(engine);
			
			if (ret != ClamReturnCode.CL_SUCCESS)
				throw new Exception("Expected CL_SUCCESS, got " + ret);
		}
	}
}