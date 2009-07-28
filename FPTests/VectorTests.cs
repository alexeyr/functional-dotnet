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

using FP.Collections.Persistent;
using Xunit;
using XunitExtensions;
using Microsoft.Pex.Framework;

namespace FPTests {
    [PexClass(typeof (Vector<>))]
    public partial class VectorTests {
        [PexMethod]
        public void Test_InfiniteBounds(int i) {
            PexAssume.IsTrue(i >= 0);
            Assert.Equal(0, Vector<int>.Empty[i]);
            Assert.Equal(null, Vector<object>.Empty[i]);
        }

        [PexGenericArguments(typeof(int))]
        [PexGenericArguments(typeof(object))]
        [PexMethod(MaxBranches = 20000)]
        public void Test_SingleElementVector<T>(int i, T value) {
            PexAssume.IsTrue(i >= 0);
            Vector<T> vector = Vector<T>.Empty.SetAt(i, value);
            Assert.Equal(value, vector[i]);
        } // SingleElementVector()

        [PexGenericArguments(typeof(int))]
        [PexMethod(MaxBranches = 40000)]
        public void Test_ReplaceSingleElement<T>([PexAssumeNotNull] T[] array, T newValue, int i) {
            var vector = Vector.New(array);
            PexAssume.IsTrue(i >= 0);
            var vector2 = vector.SetAt(i, newValue);
            Assert.True(vector2.Count >= vector.Count);
            Assert.Equal(newValue, vector2[i]);
            PexAssert.TrueForAll(0, vector2.Count, j => j == i || Equals(vector2[j], vector[j]));
        } // ReplaceSingleElement(i, j)

        [PexGenericArguments(typeof(int))]
        // [PexGenericArguments(typeof(object))]
        [PexMethod(MaxBranches = 40000)]
        public void Test_StoreManyElements<T>([PexAssumeNotNull] T[] array) {
            var vector = Vector<T>.Empty;
            for (int i = 0; i < array.Length; i++)
                vector = vector.SetAt(i, array[i]);
            for (int i = 0; i < array.Length; i++)
                Assert.Equal(array[i], vector[i]);
            Assert.Equal(array.Length, vector.Count);
            Assert2.SequenceEqual(array, vector);
        } // StoreManyElements()

        [PexGenericArguments(typeof(int))]
        // [PexGenericArguments(typeof(object))]
        [PexMethod(MaxBranches = 40000)]
        public void Test_Append<T>([PexAssumeNotNull] T[] array) {
            var vector = Vector<T>.Empty;
            foreach (T t in array)
                vector = vector.Append(t);
            for (int i = 0; i < array.Length; i++)
                Assert.Equal(array[i], vector[i]);
            Assert.Equal(array.Length, vector.Count);
            Assert2.SequenceEqual(array, vector);
        } // StoreManyElements()
    } // class VectorTests
} // namespace FPTests