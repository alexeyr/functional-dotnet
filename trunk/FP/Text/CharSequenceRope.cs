/*
* CharSequenceRope.cs is part of functional-dotnet project
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
using System.Collections.Generic;
using System.IO;
using FP.Validation;

namespace FP.Text {
    /// <summary>
    /// A flat rope containing an <see cref="ICharSequence"/>.
    /// </summary>
    /// <typeparam name="TSequence">The type of the character sequence used by the rope.</typeparam>
    /// <seealso cref="SubStringRope"/>
    /// <remarks>If you plan to use this class with a specific type of character sequences, it may be convenient to 
    /// create a subclass.</remarks>
    [Serializable]
    public class CharSequenceRope<TSequence> : FlatRope
        where TSequence : IFlatCharSequence {
        private readonly TSequence _charSequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharSequenceRope{TSequence}"/> class.
        /// </summary>
        /// <param name="charSequence">The character sequence.</param>
        public CharSequenceRope(TSequence charSequence) {
            _charSequence = charSequence;
            if (_charSequence.Count < 0) {
                throw new ArgumentException(
                    string.Format(
                        "Attempted to create a rope from character sequence {0} with apparent length {1}. " +
                        "It's possible there is an overflow",
                        _charSequence, _charSequence.Count));
            }
        }

        public override sealed IEnumerator<char> GetEnumerator(int startIndex) {
            return _charSequence.GetEnumerator(startIndex);
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public override sealed int Count {
            get { return _charSequence.Count; }
        }

        public override sealed char this[int index] {
            get {
                // bounds checking is responsibility of _charSequence
                return _charSequence[index];
            }
        }

        public override sealed void CopyTo(int sourceIndex, char[] destination,
                                           int destinationIndex, int count) {
            _charSequence.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public override void WriteOut(TextWriter writer, int startIndex, int count) {
            _charSequence.WriteOut(writer, startIndex, count);
        }

        public override Rope SubStringInternal(int startIndex, int count) {
            if (startIndex == 0 && count == Count)
                return this;
            if (count <= MAX_SHORT_SIZE) {
                var array = _charSequence.ToArray(startIndex, count);
                return array.ToRope();
            }
            return new SubStringRope<TSequence>(this, startIndex, count);
        }

        public override Rope Reverse() {
            if (Count <= MAX_SHORT_SIZE) {
                var array = _charSequence.ToArray();
                Array.Reverse(array);
                return array.ToRope();
            }
            return new ReverseRope<TSequence>(this, 0, Count);
        }
    }
}