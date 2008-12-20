#region License
/*
* MaybeTests.cs is part of functional-dotnet project
* 
* Copyright (c) 2008 Alexey Romanov
* All rights reserved.
*
* This source file is available under The New BSD License.
* See license.txt file for more information.
* 
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
* CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
* INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
* MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
*/
#endregion

using System;
using FP.Core;
using FP.Linq;
using Xunit;
using Microsoft.Pex.Framework;

namespace FPTests {
    [PexClass(typeof(Optional<>))]
    public partial class MaybeTests {
        [Fact]
        public void Maybe_NullShouldConvertToNothing() {
            var m = OptionalNotNull.Wrap<string>(null);
            Assert.Equal(m, Optional.None<string>());
            Assert.False(m.HasValue);
        }

        [PexMethod]
        public void Maybe_ValueShouldConvertToSomething([PexAssumeNotNull] object o) {
            Optional<object> m = o;
            Assert.True(m.HasValue);
            Assert.Equal(m.Value, o);
        }

        [PexMethod]
        public void Maybe_QueriesShouldWork(int x, int y) {
            var query = from x1 in Optional.Some(x)
                        from y1 in Optional.Some(y)
                        select x1 + y1;
            Assert.Equal(query.Value, x + y);
        }

        [PexMethod]
        public void Maybe_NothingShouldPropagateInQueries(int x) {
            var query = from x1 in Optional.Some(x)
                        from y1 in Optional<int>.None
                        select x1 + y1;
            Assert.False(query.HasValue);
        }

        [PexMethod]
        public void Maybe_DefaultOperatorReturnsTheFirstFoundValue (int x, Optional<int> my) {
            Assert.Equal(Optional.Some(x) || my, Optional.Some(x));
        }

        [PexMethod]
        public void Maybe_DefaultOperatorCausesConversions(int x, int y) {
            Assert.Equal(Optional<int>.None || x || y, Optional.Some(x));
        }

        [PexMethod]
        public void Maybe_DefaultOperatorIgnoresNothingAsFirstArgument(Optional<int> mx) {
            Assert.Equal(Optional<int>.None || mx, mx);
        }

        [Fact]
        public void Maybe_TryCatchesExceptions() {
            int zero = 0;
            Assert.Equal(Optional<int>.None, Optional.Try(() => 1 / zero));
            Assert.Equal(Optional<int>.None, Optional.Try<int, DivideByZeroException>(() => 1 / zero));
        }
    }
}
