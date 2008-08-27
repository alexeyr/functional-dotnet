#region License
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
#endregion
using System;
using FP.Collections.Immutable;
using Xunit;
using XunitExtensions;

namespace FPTests {
    public class VectorTests {
        private Vector<int> _emptyVector = Vector<int>.Empty;
        private Random _rnd = new Random();

        [Fact]
        public void InfiniteBounds() {
            for (int i = 0; i < 100; i++) {
                int index = _rnd.Next();
                Assert.Equal(0, _emptyVector[index]);
            }
        }

        [Fact]
        public void SingleElementVector() {
            for (int i = 0; i < 100; i++) {
                int value = _rnd.Next();
                Vector<int> vector = _emptyVector.SetAt(0, value);
                Assert.Equal(value, vector[0]);
            }            
        } // SingleElementVector()

        [Fact]
        public void ReplaceSingleElement() {
            var vector = Vector.New("Haskell", "Ocaml", "Scala", "Ruby");
            for (int i = 0; i < 4; i++) {
                var vector2 = vector.SetAt(i, "C#");
                for (int j = 0; j < 4; j++) {
                    if (j == i)
                        Assert.Equal("C#", vector2[j]);
                    else
                        Assert.Equal(vector[j], vector2[j]);
                }
            } // for (int)
        } // ReplaceSingleElement()

        [Fact]
        public void StoreManyElements() {
            const int LENGTH = 10000;
            var array = new int[LENGTH];
            var vector = _emptyVector;
            for (int i = 0; i < LENGTH; i++) {
                int index = _rnd.Next(LENGTH);
                int value = _rnd.Next();
                array[index] = value;
                vector = vector.SetAt(index, value);
            }
            for (int i = 0; i < LENGTH; i++)
                Assert.Equal(array[i], vector[i]);
            vector = vector.SetAt(LENGTH - 1, array[LENGTH - 1]); //otherwise the last elements may be still default and not included in the count.
            Assert.Equal(LENGTH, vector.Count);
            Assert2.SequenceEqual(array, vector);
        } // StoreManyElements()
    } // class VectorTests
} // namespace FPTests
