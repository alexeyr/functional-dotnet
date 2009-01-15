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
using System.IO;
using FP.Collections;

namespace FP.Text {
    /// <summary>
    /// Represents a random-access sequence of characters.
    /// </summary>
    public interface ICharSequence : IRandomAccess<char>, IReversibleEnumerable<char> {
        /// <summary>
        /// Copies the sequence to <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="sourceIndex">The index to start copying from.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        /// <param name="count">The number of elements to copy.</param>
        void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count);

        /// <summary>
        /// Gets the enumerator starting at the given index.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        IEnumerator<char> GetEnumerator(int startIndex);

        /// <summary>
        /// Writes the sequence out on the given writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="count">The number of characters to write.</param>
        void WriteOut(TextWriter writer, int startIndex, int count);
    }

    /// <summary>
    /// Represents a char sequence for which all operations' times do not depend on its length.
    /// </summary>
    public interface IFlatCharSequence : ICharSequence { }

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
        public static void CopyTo<TSequence>(
            this TSequence sequence,
            char[] destination,
            int destinationIndex)
            where TSequence : ICharSequence {
            sequence.CopyTo(0, destination, destinationIndex, sequence.Count);
        }

        /// <summary>
        /// Converts the <paramref name="sequence"/> to an array.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns>The array with the same elements as <paramref name="sequence"/>.</returns>
        public static char[] ToArray(this ICharSequence sequence) {
            return ToArray(sequence, 0, sequence.Count);
        }

        /// <summary>
        /// Converts the <paramref name="sequence"/> to an array.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        /// The array with the same elements as <paramref name="sequence"/>.
        /// </returns>
        public static char[] ToArray(this ICharSequence sequence, int startIndex, int count) {
            var array = new char[count];
            sequence.CopyTo(startIndex, array, 0, count);
            return array;
        }

        /// <summary>
        /// Converts a character sequence to a string.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns>The string with the same characters as <paramref name="sequence"/>.</returns>
        public static string AsString(this ICharSequence sequence) {
            return sequence.SubStringAsString(0, sequence.Count);
        }

        /// <summary>
        /// Converts a part of a character sequence to a string.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns>The string with the same characters as <paramref name="sequence"/>.</returns>
        public static string SubStringAsString(this ICharSequence sequence, int startIndex, int count) {
            if (count == 0) 
                return string.Empty;
            return new string(sequence.ToArray(startIndex, count));
        }

        /// <summary>
        /// Writes a part of a character sequence to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="charSequence">The char sequence.</param>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="count">The number of characters to write.</param>
        public static void Write(
            this TextWriter writer, ICharSequence charSequence, int startIndex, int count) {
            charSequence.WriteOut(writer, startIndex, count);
        }

        /// <summary>
        /// Writes a character sequence to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="charSequence">The char sequence.</param>
        public static void Write(
            this TextWriter writer, ICharSequence charSequence) {
            charSequence.WriteOut(writer, 0, charSequence.Count);
        }
    }
}