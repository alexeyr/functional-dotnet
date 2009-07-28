using System.Collections.Generic;
using FP.Collections.Persistent;
using FP.Core;
using Microsoft.Pex.Framework;

namespace FPTests {
    [PexClass(typeof(LazyList<>))]
    public partial class LazyListTests {
        [PexMethod]
        [PexGenericArguments(typeof(int), typeof(object))]
        public void Test_Enumeration<T>([PexAssumeNotNull] T[] array) {
            PexAssert.AreElementsEqual(array.ToLazyList(), array, (x, y) => EqualityComparer<T>.Default.Equals(x, y));
        }
    }
}
