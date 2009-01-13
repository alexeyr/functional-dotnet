/*
* IRope.cs is part of functional-dotnet project
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
using System.Globalization;
using FP.Collections;

namespace FP.Text {
    /// <summary>
    /// An interface for "ropes", scalable representations of strings.
    /// </summary>
    /// <typeparam name="TRope">The type of the rope.</typeparam>
    public interface IRope<TRope> : ICharSequence, ICatenable<TRope>
        where TRope : IRope<TRope> {
        /// <summary>
        /// Returns the substring.
        /// </summary>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="count">The number of elements in the subsequence.</param>
        /// <exception cref="IndexOutOfRangeException">When <paramref name="startIndex"/>or
        /// <paramref name="count"/> is negative, or their sum is greater than the length
        /// of the rope.</exception>
        TRope SubString(int startIndex, int count);

        /// <summary>
        /// Appends a part of the specified character sequence.
        /// </summary>
        /// <param name="charSequence">The character sequence.</param>
        /// <param name="startIndex">The starting index in <paramref name="charSequence"/>.</param>
        /// <param name="count">The number of elements in the subsequence.</param>
        TRope Append<TCharSequence>(TCharSequence charSequence, int startIndex, int count) where TCharSequence : IFlatCharSequence;

        /// <summary>
        /// Prepends a part of the specified character sequence.
        /// </summary>
        /// <param name="charSequence">The character sequence.</param>
        /// <param name="startIndex">The starting index in <paramref name="charSequence"/>.</param>
        /// <param name="count">The number of elements in the subsequence.</param>
        TRope Prepend<TCharSequence>(TCharSequence charSequence, int startIndex, int count) where TCharSequence : IFlatCharSequence;

        /// <summary>
        /// Reverses this rope.
        /// </summary>
        TRope Reverse();

        /// <summary>
        /// Removes all occurrences of a set of characters specified in an array from the beginning of this rope.
        /// </summary>
        /// <param name="trimChars">An array of Unicode characters to be removed.</param>
        TRope TrimStart(params char[] trimChars);

        /// <summary>
        /// Removes all occurrences of a set of characters specified in an array from the end of this rope.
        /// </summary>
        /// <param name="trimChars">An array of Unicode characters to be removed.</param>
        TRope TrimEnd(params char[] trimChars);
    }
}