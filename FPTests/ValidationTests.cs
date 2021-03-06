/*
* ValidationTests.cs is part of functional-dotnet project
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
using System;
using Xunit;
using FP.Validation;

namespace FPTests {
    public class ValidationTests {
        [Fact]
        public void IsNotNullWorksWithReferenceTypes() {
            Requires.That
                .IsNotNull(new object(), "obj")
                .Check();

            const object objNull = null;

            Assert.Throws<ArgumentNullException>(() =>
                                               Requires.That
                                                   .IsNotNull(objNull, "obj")
                                                   .Check());
        }

        [Fact]
        public void IsNotNullWorksWithNullableValueTypes() {
            int? nullableIntNull = null;
            int? nullableIntNotNull = 1;

            Requires.That
                .IsNotNull(nullableIntNotNull, "nullableIntNotNull")
                .Check();

            Assert.Throws<ArgumentNullException>(() =>
                                               Requires.That
                                                   .IsNotNull(nullableIntNull, "nullableIntNull")
                                                   .Check());
        }
    }
}