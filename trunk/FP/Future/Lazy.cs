/*
* Lazy.cs is part of functional-dotnet project
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
using FP.Core;

namespace FP.Future {
    /// <summary>
    /// Represents a suspended computation that will be performed once, in the thread which requests it, 
    /// on first access, to determine the value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Lazy<T> : Future<T> {
        private bool _hasResult;
        private Result<T> _result;
        private Func<T> _calculation;

        /// <summary>
        /// Initializes a new instance of a <see cref="Lazy{T}"/> <see cref="Future{T}"/>.
        /// </summary>
        /// <param name="calculation">The calculation the new future does.</param>
        public Lazy(Func<T> calculation) {
            _calculation = calculation;
        }

        /// <summary>
        /// Gets a value indicating whether this future is lazy (and not forced yet).
        /// </summary>
        /// <value><c>true</c> if this future is not forced yet; otherwise, <c>false</c>.</value>
        public override bool IsLazy {
            get { return !_hasResult; }
        }

        /// <summary>
        /// Gets the result of the future, whether it is failed or determined. If the result isn't 
        /// available yet, blocks the thread until it is obtained.
        /// </summary>
        /// <value>The result.</value>
        public override Result<T> Result {
            get {
                if (!_hasResult) {
                    _result = Core.Result.Try(_calculation);
                    _hasResult = true;
                    _calculation = null;
                }
                return _result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a result (is successful or failed).
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has a result; otherwise, <c>false</c>.
        /// </value>
        public override bool HasResult {
            get { return _hasResult; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Future{T}"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return ToStringHelper("Lazy");
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="FP.Future.Future&lt;T&gt;"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="lazy">The lazy value.</param>
        /// <returns>The result of forcing this lazy value.</returns>
        public static implicit operator T(Lazy<T> lazy) {
            return lazy.Await();
        }
    }
}