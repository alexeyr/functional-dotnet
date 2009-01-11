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

using System;
using System.Collections;
using System.Collections.Generic;
using FP.Collections;

namespace FP.Text {
    /// <summary>
    /// A finger-tree-based rope ("heavyweight string"). It can do all string
    /// operations. An amortized running time is given for each operation, with <i>n
    /// </i> referring to the length  of the sequence and <i>i</i> being the integral
    /// index used by some operations. 
    /// </summary>
    /// <remarks>Do not use the default constructor. This is not complete yet!
    /// </remarks>
    [Obsolete]
    public struct FTRope : IRope<FTRope> {
        private readonly FingerTree<FlatRope, int> _ft;

        internal FTRope(FingerTree<FlatRope, int> ft) {
            _ft = ft;
        }

        public IEnumerator<char> GetEnumerator(int startIndex) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<char> GetEnumerator() {
            return GetEnumerator(0);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public FTRope SubString(int startIndex, int length) {
            throw new NotImplementedException();
        }

        public FTRope Concat(FTRope other) {
            throw new System.NotImplementedException();
        }

        public bool IsEmpty {
            get { return Count == 0; }
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public int Count {
            get { return _ft.Measure; }
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th character in the sequence.
        /// Should be quick constant time.
        /// </summary>
        public char this[int index] {
            get { throw new System.NotImplementedException(); }
        }

        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) {
            throw new System.NotImplementedException();
        }
    }
}