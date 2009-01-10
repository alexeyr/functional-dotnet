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
    /// An interface for large strings.
    /// </summary>
    /// <typeparam name="TRope">The type of the rope.</typeparam>
    public interface IRope<TRope> : ICharSequence, ICatenable<TRope>
        where TRope : IRope<TRope> { }
}