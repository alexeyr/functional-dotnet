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

using FP.Text;
using Microsoft.Pex.Framework;
using Xunit;

namespace FPTests {
    [PexClass(typeof (Rope))]
    public partial class RopeTests {
        [PexMethod]
        public void Test_Creation([PexAssumeNotNull] string[] strings) {
            string largeString = string.Concat(strings);
            Rope largeRope = string.Empty.MakeStringRope();
            foreach (string s in strings) {
                PexAssume.IsNotNull(s);
                PexAssume.IsTrue(s.Length == 0 || !char.IsControl(s[0]));
                largeRope = largeRope.Concat(s.MakeStringRope());
            }
            Assert.Equal(largeString, largeRope.AsString());
        }

        [PexMethod]
        public void Test_Concat([PexAssumeNotNull] Rope rope1, [PexAssumeNotNull] Rope rope2) {
            Assert.Equal(rope1.AsString() + rope2.AsString(), rope1.Concat(rope2).AsString());
        }

        //TODO: Test substring creation!
    }
}