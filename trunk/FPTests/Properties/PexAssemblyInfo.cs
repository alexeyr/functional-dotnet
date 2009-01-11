using Microsoft.Pex.Framework.Explorable;
using Microsoft.Pex.Framework.Instrumentation;
using System.IO;

[assembly : PexInstrumentAssembly("FP")]
[assembly : PexAssemblyUnderTest("FP")]
[assembly : PexInstrumentAssembly("XunitExtensions")]
[assembly : PexExplorableFromFactories]
[assembly: PexInstrumentAssembly("FP.Validation")]
[assembly: PexInstrumentType(typeof(TextWriter))]
