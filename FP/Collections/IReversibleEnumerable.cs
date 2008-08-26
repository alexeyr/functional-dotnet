#region License
/*
* IReversible.cs is part of functional-dotnet project
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

using System.Collections.Generic;

namespace FP.Collections {
    /// <summary>
    /// Represents a sequence which can be effectively enumerated in reverse order.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReversibleEnumerable<T> : IEnumerable<T> {
        /// <summary>
        /// Gets the reverse enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> GetReverseEnumerator();
    } // interface IReversibleEnumerable
}