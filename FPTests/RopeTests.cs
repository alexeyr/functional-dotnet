#region License
/*
* RopeTests.cs is part of functional-dotnet project
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
using FP.Text;
using Xunit;

namespace FPTests {
    public class RopeTests {
        private const string _digits = "0123456789";
        private readonly static StringRope _digitsRope = _digits.ToRope();

        [Fact] 
        public void Substring() {
            Assert.Equal(_digits.Substring(0, 5), _digitsRope.SubString(0, 5).AsString());
            Assert.Equal(_digits.Substring(5, 5), _digitsRope.SubString(5, 5).AsString());
        }

        [Fact]
        public void ConcatTwoShortFlat() {
            var concat = _digitsRope.Concat(_digitsRope);
            Assert.IsAssignableFrom<FlatRope<char>>(concat);
            Assert.Equal(string.Concat(_digits, _digits), concat.AsString());
        }

        [Fact]
        public void ConcatSeveralShortFlat() {
            var manyDigits = string.Concat(_digits, _digits, _digits, _digits, _digits);
            var manyDigitsRope =
                _digitsRope.Concat(_digitsRope).Concat(_digitsRope).Concat(_digitsRope).Concat(
                    _digitsRope);
            Assert.Equal(manyDigits, manyDigitsRope.AsString());
            Assert.Equal(string.Concat(_digits, _digits, _digits), manyDigitsRope.SubString(10, 30).AsString());
        }
    }
}