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

using System;
using System.Linq;
using FP.Collections;
using FP.Core;
using Microsoft.Pex.Framework;
using Xunit;
using XunitExtensions;

namespace FPTests {
    [PexClass(typeof(OrderedSequence<>))]
    public partial class OrdSequenceTests {
        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void Test_Ordering<T>([PexAssumeNotNull] T[] array) where T : IComparable<T> {
            var seq = OrderedSequence.FromEnumerable(array);
            Array.Sort(array);
            Assert2.SequenceEqual(array, seq);
            Array.Reverse(array);
            Assert2.SequenceEqual(array, seq.ReverseIterator());
        }

        [PexMethod(MaxBranches = 20000)]
        [PexGenericArguments(typeof(int))]
        public void Test_MinMax<T>([PexAssumeNotNull] T[] array) where T : IComparable<T> {
            PexAssume.IsNotNullOrEmpty(array);
            var seq = OrderedSequence.FromEnumerable(array);
            Array.Sort(array);
            Assert.Equal(array[0], seq.Min);
            Assert.Equal(array[array.Length - 1], seq.Max);
            Assert2.SequenceEqual(array.Skip(1), seq.RemoveMin());
            Assert2.SequenceEqual(array.Take(array.Length - 1), seq.RemoveMax());
        }

        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void Test_ContainsPositive<T>([PexAssumeNotNull] T[] array, T item) where T : IComparable<T> {
            var seq = OrderedSequence.FromEnumerable(array);
            PexAssume.IsTrue(array.Contains(item));
            Assert.Contains(item, seq);
            Assert.Equal(0, seq[item].Value.CompareTo(item));
        }

        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void Test_ContainsNegative<T>([PexAssumeNotNull] T[] array, T item) where T : IComparable<T> {
            var seq = OrderedSequence.FromEnumerable(array);
            PexAssume.IsFalse(array.Contains(item));
            Assert.DoesNotContain(item, seq);
            Assert.False(seq[item].HasValue);
        }

        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void Test_ExtractAll<T>([PexAssumeNotNull] T[] array, T item) where T : IComparable<T> {
            var seq = OrderedSequence.FromEnumerable(array);
            var split = seq.ExtractAll(item);
            Assert2.SequenceEqual(seq, split.Item1.Concat(split.Item2).Concat(split.Item3));
            PexAssert.TrueForAll(split.Item1, t => t.CompareTo(item) < 0);
            PexAssert.TrueForAll(split.Item2, t => t.CompareTo(item) == 0);
            PexAssert.TrueForAll(split.Item3, t => t.CompareTo(item) > 0);
        }

        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void Test_ExtractOneNegative<T>([PexAssumeNotNull] T[] array, T item) where T : IComparable<T> {
            PexAssume.IsFalse(array.Contains(item));
            var seq = OrderedSequence.FromEnumerable(array);
            var maybeSplit = seq.ExtractOne(item);
            Assert.False(maybeSplit.HasValue);
        }

        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void Test_ExtractOnePositive<T>([PexAssumeNotNull] T[] array, T item) where T : IComparable<T> {
            PexAssume.IsTrue(array.Contains(item));
            var seq = OrderedSequence.FromEnumerable(array);
            var split = seq.ExtractOne(item).Value;
            Assert2.SequenceEqual(seq, split.Item1.Append(split.Item2).Concat(split.Item3));
            PexAssert.TrueForAll(split.Item1, t => t.CompareTo(item) <= 0);
            Assert.True(split.Item2.CompareTo(item) == 0);
            PexAssert.TrueForAll(split.Item3, t => t.CompareTo(item) >= 0);
        }

        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void Test_Split<T>([PexAssumeNotNull] T[] array, T item, bool equalGoLeft) where T : IComparable<T> {
            var seq = OrderedSequence.FromEnumerable(array);
            var split = seq.Split(item, equalGoLeft);
            Assert2.SequenceEqual(seq, split.Item1 + split.Item2);
            if (equalGoLeft) {
                PexAssert.TrueForAll(split.Item1, t => t.CompareTo(item) <= 0);
                PexAssert.TrueForAll(split.Item2, t => t.CompareTo(item) > 0);
            }
            else {
                PexAssert.TrueForAll(split.Item1, t => t.CompareTo(item) < 0);
                PexAssert.TrueForAll(split.Item2, t => t.CompareTo(item) >= 0);                
            }
        }

        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void Test_Insert<T>([PexAssumeNotNull] T[] array1, [PexAssumeNotNull] T[] array2) where T : IComparable<T> {
            var seq1 = OrderedSequence.FromEnumerable(array1);
            var seq = seq1.InsertRange(array2);
            Assert2.SequenceEqual(array1.Concat(array2).Sort(), seq);
        }

        [PexMethod]
        [PexGenericArguments(typeof(int))]
        public void Test_Intersect<T>([PexAssumeNotNull] T[] array1, [PexAssumeNotNull] T[] array2) where T : IComparable<T> {
            var seq1 = OrderedSequence.FromEnumerable(array1);
            var seq2 = OrderedSequence.FromEnumerable(array2);
            Assert2.SequenceEqual(array1.Intersect(array2).Sort(), seq1.Intersect(seq2));
        }

        [PexMethod(MaxBranches = 20000)]
        [PexGenericArguments(typeof(int))]
        public void Test_Union<T>([PexAssumeNotNull] T[] array1, [PexAssumeNotNull] T[] array2) where T : IComparable<T> {
            var seq1 = OrderedSequence.FromEnumerable(array1);
            var seq2 = OrderedSequence.FromEnumerable(array2);
            Assert2.SequenceEqual(array1.Concat(array2).Sort(), seq1 + seq2);
        }
    }
}