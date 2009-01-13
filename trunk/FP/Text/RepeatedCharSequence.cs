/*
* RepeatedCharSequence.cs is part of functional-dotnet project
* 
* Copyright (c) 2009 Alexey Romanov
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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FP.Validation;

namespace FP.Text {
    /// <summary>
    /// A flat char sequence which consists of a single character repeated some number of times.
    /// </summary>
    public struct RepeatedCharSequence : IFlatCharSequence {
        private readonly char _char;
        private readonly int _count;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatedCharSequence"/> struct.
        /// </summary>
        /// <param name="ch">The character.</param>
        /// <param name="count">The number of times the character is repeated.</param>
        public RepeatedCharSequence(char ch, int count) {
            _char = ch;
            _count = count;
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

        public IEnumerator<char> GetEnumerator(int startIndex) {
            for (int i = startIndex; i < _count; i++)
                yield return _char;
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public int Count {
            get { return _count; }
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th <see cref="char"/> in the sequence.
        /// Should be quick constant time.
        /// </summary>
        public char this[int index] {
            get {
                Requires.That.IsInRange(0, _count - 1, index, "index").Check();

                return _char;
            }
        }

        /// <summary>
        /// Copies the array to <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="sourceIndex">The index to start copying from.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        /// <param name="count">The number of elements to copy.</param>
        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) {
            Requires.That.
                IsIndexAndCountInRange(this, sourceIndex, count, "sourceIndex", "count").Check();
            
            for (int i = destinationIndex; i < destinationIndex + count; i++) {
                destination[i] = _char;
            }
        }

        public void WriteOut(TextWriter writer, int startIndex, int count) {
            writer.Write(this.ToArray(startIndex, count));
        }

        public bool IsEmpty {
            get { return _count == 0; }
        }
    }
}