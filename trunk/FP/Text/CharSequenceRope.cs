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

namespace FP.Text {
    /// <summary>
    /// A flat rope containing an <see cref="ICharSequence{TChar}"/>.
    /// </summary>
    /// <typeparam name="TChar">The type of the characters in the rope.</typeparam>
    /// <typeparam name="TSequence">The type of the character sequence used by the rope.</typeparam>
    /// <seealso cref="StringRope"/>
    /// <seealso cref="ArrayRope{TChar}"/>
    /// <seealso cref="SubstringRope{TChar,TSequence}"/>
    /// <remarks>If you plan to use this class with a specific type of character sequences, it may be convenient to 
    /// create a subclass.</remarks>
    [Serializable]
    public class CharSequenceRope<TChar, TSequence> : FlatRope<TChar>
        where TSequence : ICharSequence<TChar> {
        [CLSCompliant(false)] protected readonly TSequence _charSequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharSequenceRope{TChar,TSequence}"/> class.
        /// </summary>
        /// <param name="charSequence">The character sequence.</param>
        public CharSequenceRope(TSequence charSequence) {
            _charSequence = charSequence;
        }

        public override sealed IEnumerator<TChar> GetEnumerator() {
            return _charSequence.GetEnumerator();
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public override sealed int Length {
            get { return _charSequence.Length; }
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th <see cref="TChar"/> in the sequence.
        /// </summary>
        public override sealed TChar this[int index] {
            get {
                //bounds checking is responsibility of _charSequence
                return _charSequence[index];
            }
        }

        public override sealed void CopyTo(int sourceIndex, TChar[] destination,
                                           int destinationIndex, int count) {
            _charSequence.CopyTo(sourceIndex, destination, destinationIndex, count);
        }

        public override Rope<TChar> SubString(int startIndex, int length) {
            if (startIndex == 0 && length == Length)
                return this;
            if (length <= MAX_SHORT_SIZE) {
                var array = new TChar[length];
                _charSequence.CopyTo(startIndex, array, 0, length);
                return
                    new ArrayRope<TChar>(array);
            }
            return new SubstringRope<TChar, TSequence>(_charSequence, startIndex, length);
        }
        }
}