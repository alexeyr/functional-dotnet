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
    public partial class MaybeTests {
        [Fact]
        public void Maybe_NullShouldConvertToNothing() {
            Maybe<string> m = (string)null;
            Assert.Equal(m, Maybe.Nothing<string>());
            Assert.False(m.HasValue);
        }

        [PexMethod]
        public void Maybe_ValueShouldConvertToSomething([PexAssumeNotNull] object o) {
            Maybe<object> m = o;
            Assert.True(m.HasValue);
            Assert.Equal(m.Value, o);
        }

        [PexMethod]
        public void Maybe_QueriesShouldWork(int x, int y) {
            var query = from x1 in Maybe.Just(x)
                        from y1 in Maybe.Just(y)
                        select x1 + y1;
            Assert.Equal(query.Value, x + y);
        }

        [PexMethod]
        public void Maybe_NothingShouldPropagateInQueries(int x) {
            var query = from x1 in Maybe.Just(x)
                        from y1 in Maybe<int>.Nothing
                        select x1 + y1;
            Assert.False(query.HasValue);
        }

        [PexMethod]
        public void Maybe_DefaultOperatorReturnsTheFirstJustValue (int x, Maybe<int> my) {
            Assert.Equal(Maybe.Just(x) || my, Maybe.Just(x));
        }

        [PexMethod]
        public void Maybe_DefaultOperatorCausesConversions(int x, int y) {
            Assert.Equal(Maybe<int>.Nothing || x || y, Maybe.Just(x));
        }

        [PexMethod]
        public void Maybe_DefaultOperatorIgnoresNothingAsFirstArgument(Maybe<int> mx) {
            Assert.Equal(Maybe<int>.Nothing || mx, mx);
        }

        [Fact]
        public void Maybe_TryCatchesExceptions() {
            int zero = 0;
            Assert.Equal(Maybe<int>.Nothing, Maybe.Try(() => 1 / zero));
            Assert.Equal(Maybe<int>.Nothing, Maybe.Try<int, DivideByZeroException>(() => 1 / zero));
        }
    }
}
