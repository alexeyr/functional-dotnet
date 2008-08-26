#region License
/*
* RandomAccessSequence.cs is part of functional-dotnet project
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
using System.Collections;
using System.Collections.Generic;
using FP.Collections.Immutable;

namespace FP.Text {
    /// <summary>
    /// A finger-tree-based rope ("heavyweight string"). It can do all string operations.
    /// An amortized running time is given for each operation, with <i>n</i> referring to the length 
    /// of the sequence and <i>i</i> being the integral index used by some operations. 
    /// </summary>
    /// <typeparam name="T">Type of the elements of the sequence.</typeparam>
    /// <remarks>Do not use the default constructor.</remarks>
    public struct FTRope<TChar> : IRope<TChar, FTRope<TChar>> {
        private readonly FingerTree<FlatRope<TChar>, int> _ft;

        internal FTRope(FingerTree<FlatRope<TChar>, int> ft) {
            _ft = ft;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TChar> GetEnumerator() {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public FTRope<TChar> SubString(int startIndex, int length) {
            throw new System.NotImplementedException();
        }

        public FTRope<TChar> Concat(FTRope<TChar> other) {
            throw new System.NotImplementedException();
        }

        public bool IsEmpty {
            get { return Length == 0; }
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public int Length {
            get { return _ft.Measure; }
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th <see cref="TChar"/> in the sequence.
        /// Should be quick constant time.
        /// </summary>
        public TChar this[int index] {
            get { throw new System.NotImplementedException(); }
        }

        public void CopyTo(int sourceIndex, TChar[] destination, int destinationIndex, int count) {
            throw new System.NotImplementedException();
        }
    }
}