#region License
/*
* ISplittable.cs is part of functional-dotnet project
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
#endregion

using FP.Core;

namespace FP.Collections {
    /// <summary>
    /// Represents a sequence which can be efficiently split at any given index.
    /// </summary>
    /// <typeparam name="TSequence">The type of the sequence.</typeparam>
    public interface ISplittable<TSequence> {
        /// <summary>
        /// Returns a pair of sequences, the first contains the first <paramref name="count"/> of
        /// the sequence and the second one contains the rest of them.
        /// </summary>
        /// <param name="count">The index at which the sequence will be split.</param>
        /// <remarks>if <code>count &lt;= 0 || count &gt;= Count</code>, the corresponding part 
        /// of the result will be empty.</remarks>
        Tuple<TSequence, TSequence> SplitAt(int count);
    } // interface ISplittable
} // namespace FP.Collections
