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
    public interface ICharSequence : IEnumerable<char> {
        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the <paramref name="index"/>-th character in the sequence.
        /// Should be quick constant time.
        /// </summary>
        char this[int index] { get; }

        /// <summary>
        /// Copies the sequence to <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="sourceIndex">The index to start copying from.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        /// <param name="count">The number of elements to copy.</param>
        void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count);
    }

    /// <summary>
    /// Utility class with extension methods for <see cref="ICharSequence"/>
    /// </summary>
    public static class CharSequences {
        /// <summary>
        /// Copies the given <paramref name="sequence"/> to <paramref name="destination"/>.
        /// </summary>
        /// <typeparam name="TSequence">The type of the sequence.</typeparam>
        /// <param name="sequence">The sequence to be copied.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">Index of the destination array where the copy
        /// will start.</param>
        public static void CopyTo<TSequence>(this TSequence sequence, char[] destination,
                                                    int destinationIndex)
            where TSequence : ICharSequence {
            sequence.CopyTo(0, destination, destinationIndex, sequence.Length);
        }

        /// <summary>
        /// Converts the <paramref name="sequence"/> to an array.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns>The array with the same elements as <paramref name="sequence"/>.</returns>
        public static char[] ToArray(this ICharSequence sequence) {
            var array = new char[sequence.Length];
            sequence.CopyTo(array, 0);
            return array;
        }

        /// <summary>
        /// Converts the <paramref name="sequence"/> to a string.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns>The string with the same characters as <paramref name="sequence"/>.</returns>
        public static string AsString(this ICharSequence sequence) {
            return new string(sequence.ToArray());
        }
    }
}