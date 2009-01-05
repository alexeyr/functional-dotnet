using Microsoft.Pex.Framework.Explorable;
using Microsoft.Pex.Framework.Instrumentation;

[assembly : PexInstrumentAssembly("FP")]
[assembly : PexAssemblyUnderTest("FP")]
[assembly : PexInstrumentAssembly("XunitExtensions")]
[assembly : PexExplorableFromFactories]
[assembly: PexInstrumentAssembly("FP.Validation")]
