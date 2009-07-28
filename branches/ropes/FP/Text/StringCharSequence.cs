/*
* StringCharSequence.cs is part of functional-dotnet project
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

using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FP.Text {
    /// <summary>
    /// An adapter for <see cref="string"/> to the 
    /// <see cref="ICharSequence"/> interface.
    /// </summary>
    public struct StringCharSequence : IFlatCharSequence {
        private readonly string _string;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringCharSequence"/> struct.
        /// </summary>
        /// <param name="s">The string.</param>
        public StringCharSequence(string s) {
            _string = s ?? string.Empty;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<char> GetEnumerator() {
            return _string.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _string.GetEnumerator();
        }

        public IEnumerable<char> IteratorFrom(int startIndex) {
            for (int i = startIndex; i < _string.Length; i++)
                yield return _string[i];
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public int Count {
            get { return _string.Length; }
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th <see cref="char"/> in the sequence.
        /// Should be quick constant time.
        /// </summary>
        public char this[int index] {
            get { return _string[index]; }
        }

        /// <summary>
        /// Copies the array to <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="sourceIndex">The index to start copying from.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        /// <param name="count">The number of elements to copy.</param>
        public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) {
            _string.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public void WriteOut(TextWriter writer, int startIndex, int count) {
            writer.Write(_string.Substring(startIndex, count));
        }

        /// <summary>
        /// Gets the string represented by this sequence.
        /// </summary>
        /// <value>The string.</value>
        public string String {
            get { return _string; }
        }

        public IEnumerable<char> ReverseIterator() {
            return _string.ReverseIterator();
        }

        public override string ToString() {
            return String;
        }

        public bool IsEmpty {
            get { return _string.Length == 0; }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="StringCharSequence"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="charSequence">The char sequence.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(StringCharSequence charSequence) {
            return charSequence._string;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="StringCharSequence"/>.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator StringCharSequence(string s) {
            return new StringCharSequence(s);
        }
    }
}