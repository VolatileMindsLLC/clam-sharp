using System;
using System.Runtime.InteropServices;

namespace clamsharp
{
	public static class ClamBindings
	{
		[DllImport("clamav")]
		public extern static int cl_init(uint options);
		
		[DllImport("clamav")]
		public extern static ClamEngineDescriptor cl_engine_new();
		
		[DllImport("clamav")]
		public extern static int cl_engine_free(ClamEngineDescriptor engine);
		
		[DllImport("clamav")]
		public extern static IntPtr cl_retdbdir();
		
		[DllImport("clamav")]
		public extern static int cl_load(string path, ClamEngineDescriptor engine, ref uint signo, uint options);
		
		[DllImport("clamav")]
		public extern static int cl_scanfile(string path, ref IntPtr virusName, ref ulong scanned, ClamEngineDescriptor engine, uint options);
		
		[DllImport("clamav")]
		public extern static int cl_engine_compile(ClamEngineDescriptor engine);
	}
}
