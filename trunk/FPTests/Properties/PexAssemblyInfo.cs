using System;
using System.IO;
using Microsoft.Pex.Framework.Instrumentation;
using System.Threading;
using System.Security;

[assembly : PexInstrumentAssembly("FP")]
[assembly : PexAssemblyUnderTest("FP")]
[assembly : PexInstrumentAssembly("XunitExtensions")]
[assembly: PexInstrumentAssembly("FP.Validation")]
[assembly: PexInstrumentAssembly("Microsoft.Pex.Framework")]
[assembly: PexInstrumentAssembly("Microsoft.ExtendedReflection")]
[assembly: PexInstrumentAssembly("mscorlib")]
[assembly: PexInstrumentAssembly("System")]
[assembly: PexInstrumentType(typeof(Thread))]
[assembly: PexInstrumentType(typeof(object))]
[assembly: PexInstrumentType(typeof(ThreadStart))]
[assembly: PexInstrumentType(typeof(IntPtr))]
[assembly: PexInstrumentType(typeof(SecurityManager))]
[assembly: PexInstrumentType(typeof(SecurityContext))]
[assembly: PexInstrumentType(typeof(Predicate<string>))]
[assembly: PexInstrumentType(typeof(Type))]
[assembly: PexInstrumentType("mscorlib", "System.RuntimeType")]
