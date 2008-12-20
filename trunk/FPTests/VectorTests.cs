/*
* VectorTests.cs is part of functional-dotnet project
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

using FP.Collections.Immutable;
using Xunit;
using XunitExtensions;
using Microsoft.Pex.Framework;

namespace FPTests {
    [PexClass(typeof(Vector<>))]
    public partial class VectorTests {
        [PexMethod]
        public void Test_InfiniteBounds(int i) {
            PexAssume.IsTrue(i >= 0);
            Assert.Equal(0, Vector<int>.Empty[i]);
            Assert.Equal(null, Vector<object>.Empty[i]);
        }

        [PexGenericArguments(typeof(int))]
        [PexGenericArguments(typeof(object))]
        [PexMethod(MaxBranches = 2000)]
        public void Test_SingleElementVector<T>(int i, T value) {
            PexAssume.IsTrue(i >= 0);
            Vector<T> vector = Vector<T>.Empty.SetAt(i, value);
            Assert.Equal(value, vector[i]);
        } // SingleElementVector()

        [PexMethod(MaxBranches = 4000)]
        public void Test_ReplaceSingleElement(int i, int j) {
            var vector = Vector.New("Haskell", "Ocaml", "Scala", "Ruby");
            PexAssume.IsTrue(i >= 0);
            PexAssume.IsTrue(j >= 0);
            var vector2 = vector.SetAt(i, "C#");
            PexAssume.IsTrue(j < vector2.Count + 1);
            if (j == i)
                Assert.Equal("C#", vector2[j]);
            else
                Assert.Equal(vector[j], vector2[j]);
        } // ReplaceSingleElement(i, j)

        [PexGenericArguments(typeof(int))]
        [PexGenericArguments(typeof(object))]
        [PexMethod(MaxBranches = 4000)]
        public void Test_StoreManyElements<T>([PexAssumeNotNull] T[] array) {
            PexAssume.IsNotNull(array);
            PexAssume.IsTrue(array.Length <= 65);
            var vector = Vector<T>.Empty;
            for (int i = 0; i < array.Length; i++)
                vector = vector.SetAt(i, array[i]);
            for (int i = 0; i < array.Length; i++)
                Assert.Equal(array[i], vector[i]);
            Assert.Equal(array.Length, vector.Count);
            Assert2.SequenceEqual(array, vector);
        } // StoreManyElements()
    } // class VectorTests
} // namespace FPTests
