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

using System.IO;
using FP.Collections;

namespace FP.Text {
    /// <summary>
    /// Represents a random-access sequence of characters.
    /// </summary>
    public interface ICharSequence : IRandomAccess<char>, IReversibleEnumerable<char>, IEnumerableFrom<char> {
        /// <summary>
        /// Copies the sequence to <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="sourceIndex">The index to start copying from.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        /// <param name="count">The number of elements to copy.</param>
        void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count);

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
}