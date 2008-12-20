/*
* FingerTreeTests.cs is part of functional-dotnet project
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

namespace FPTests {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FP.Collections.Immutable;
    using Xunit;
    using XunitExtensions;

    public class OrdSequenceTests {
        private readonly OrderedSequence<int, int> _empty = OrderedSequence.Empty(int.MinValue);
        private readonly int[] _testData = new int[N];
        private readonly int[] _testDataSorted = new int[N];
        private readonly OrderedSequence<int, int> _seq;
        private const int N = 1000;
        private readonly Random _rnd = new Random();

        private static IEnumerable<T> Stutter<T>(IEnumerable<T> ts) {
            foreach (var t in ts) {
                yield return t;
                yield return t;
            }
        }

        private static IEnumerable<T> EveryNth<T>(IEnumerable<T> ts, int n) {
            int i = 0;
            foreach (var t in ts) {
                i++;
                if (i == n) {
                    yield return t;
                    i = 0;
                }
            }
        }

        public OrdSequenceTests() {
            for (int i = 0; i < N; i++)
                _testData[i] = _rnd.Next(N);
            _testData.CopyTo(_testDataSorted, 0);
            Array.Sort(_testDataSorted);
            _seq = OrderedSequence.FromEnumerable(_testData, int.MinValue);
        }

        [Fact]
        public void Test_Ordering() {
            Assert2.SequenceEqual(_testDataSorted, _seq);
        }

        [Fact]
        public void Test_FromOrderedEnumerable() {
            Assert2.SequenceEqual(_testDataSorted,
                                  OrderedSequence.FromOrderedEnumerable(_testDataSorted,
                                                                        int.MinValue));
        }

        [Fact]
        public void Test_Merge() {
            const int count = N / 4;
            var enum1 = _testData.Take(count);
            var enum2 = _testData.Skip(count);
            var seq1 = OrderedSequence.FromEnumerable(enum1, int.MinValue);
            var seq2 = OrderedSequence.FromEnumerable(enum2, int.MinValue);
            Assert2.SequenceEqual(_testDataSorted, seq1 + seq2);
            Assert2.SequenceEqual(_testDataSorted, seq2 + seq1);
        }

        [Fact]
        public void Test_Intersect() {
            var enum1 = _testData.Distinct().ToArray();
            var enum2 = EveryNth(enum1, 2);
            var enum3 = EveryNth(enum1, 3);
            var enum6 = EveryNth(enum1, 6);
            var seq2 = OrderedSequence.FromEnumerable(enum2, int.MinValue);
            var seq3 = OrderedSequence.FromEnumerable(enum3, int.MinValue);
            var seq6 = OrderedSequence.FromEnumerable(enum6, int.MinValue);
            Assert2.SequenceEqual(seq6, seq2.Intersect(seq3));
            Assert2.SequenceEqual(seq6, seq3.Intersect(seq2));
        }

        [Fact]
        public void Test_Indexing() {
            Assert.Equal(N, _seq.Count);
            for (int i = 0; i < N; i++) Assert.Equal(_testDataSorted[i], _seq[i].Item2);
        }
    }
}