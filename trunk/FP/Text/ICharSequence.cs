/*
* ICharSequence.cs is part of functional-dotnet project
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

using System.Collections.Generic;

namespace FP.Text {
    /// <summary>
    /// Represents a random-access sequence of characters.
    /// </summary>
    /// <typeparam name="TChar">The type of chars used, normally either
    /// <see cref="char"/> or <see cref="byte"/>.</typeparam>
    public interface ICharSequence<TChar> : IEnumerable<TChar> {
        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        int Length { get; }
        /// <summary>
        /// Gets the <paramref name="index"/>-th <see cref="TChar"/> in the sequence.
        /// Should be quick constant time.
        /// </summary>
        TChar this[int index] { get; }
        /// <summary>
        /// Copies the sequence to <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="sourceIndex">The index to start copying from.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        /// <param name="count">The number of elements to copy.</param>
        void CopyTo(int sourceIndex, TChar[] destination, int destinationIndex, int count);
    }

    public static class CharSequences {
        public static void CopyTo<TChar, TSequence>(this TSequence sequence, TChar[] destination, int destinationIndex)
            where TSequence : ICharSequence<TChar> {
            sequence.CopyTo(0, destination, destinationIndex, sequence.Length);
        }

        public static TChar[] ToArray<TChar>(this ICharSequence<TChar> sequence) {
            var array = new TChar[sequence.Length];
            sequence.CopyTo(array, 0);
            return array;
        }

        public static string AsString(this ICharSequence<char> sequenñe) {
            return new string(sequenñe.ToArray());
        }
    }
}