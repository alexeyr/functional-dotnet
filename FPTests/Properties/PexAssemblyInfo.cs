using System.IO;
using Microsoft.Pex.Framework.Instrumentation;

[assembly : PexInstrumentAssembly("FP")]
[assembly : PexAssemblyUnderTest("FP")]
[assembly : PexInstrumentAssembly("XunitExtensions")]
[assembly: PexInstrumentAssembly("FP.Validation")]
[assembly: PexInstrumentType(typeof(TextWriter))]
