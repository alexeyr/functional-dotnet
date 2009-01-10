/*
* CharSubsequence.cs is part of functional-dotnet project
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
    /// Represents a subsequence of a character sequence.
    /// </summary>
    /// <typeparam name="TSequence">The type of the sequence.</typeparam>
    public struct CharSubsequence<TSequence> : ICharSequence
        where TSequence : ICharSequence {
        //TODO: cache data
        //TODO: Patricia trie
        private readonly TSequence _sequence;
        private readonly int _offset;
        private readonly int _length;

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CharSubsequence&lt;TSequence&gt;"/> struct.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        public CharSubsequence(TSequence sequence, int offset, int length) {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset");
            if (length < 0 || offset + length > sequence.Count)
                throw new ArgumentOutOfRangeException("length");
            _sequence = sequence;
            _offset = offset;
            _length = length;
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CharSubsequence&lt;TSequence&gt;"/> struct.
        /// </summary>
        /// <param name="subsequence">The subsequence.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        public CharSubsequence(CharSubsequence<TSequence> subsequence, int offset, int length) {
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset");
            if (length < 0 || offset + length > subsequence.Count)
                throw new ArgumentOutOfRangeException("length");
            _sequence = subsequence._sequence;
            _offset = subsequence._offset + offset;
            _length = length;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<char> GetEnumerator() {
            for (int i = _offset; i < _offset + _length; i++)
                yield return _sequence[i];
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public int Count {
            get { return _length; }
        }

        ICharSequence IRandomAccessSequence<char, ICharSequence>.SubSequence(int startIndex, int count) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th character in the sequence.
        /// Should be quick constant time.
        /// </summary>
        public char this[int index] {
            get {
                if (index >= _length)
                    throw new ArgumentOutOfRangeException("index");
                return _sequence[_offset + index];
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
            if (sourceIndex < 0)
                throw new ArgumentOutOfRangeException("sourceIndex");
            if (sourceIndex + count > _length)
                throw new ArgumentOutOfRangeException("count");
            _sequence.CopyTo(_offset + sourceIndex, destination, destinationIndex, count);
        }

        public bool IsEmpty {
            get { return _length == 0; }
        }
        }
}