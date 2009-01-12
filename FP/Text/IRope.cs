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

using FP.Collections;

namespace FP.Text {
    /// <summary>
    /// An interface for "ropes", scalable representations of strings.
    /// </summary>
    public interface IRope : ICharSequence {
    }

    /// <summary>
    /// Ropes with representation compatible with <typeparamref name="TRope"/>.
    /// </summary>
    /// <typeparam name="TRope">The type of the rope.</typeparam>
    public interface IRope<TRope> : IRope, ICatenable<TRope>
        where TRope : IRope<TRope> {
        /// <summary>
        /// Returns the substring.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The length.</param>
        TRope SubString(int startIndex, int count);
        }
}