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
    /// Represents a suspended computation that will be performed once, in the thread
    /// which requests it, to determine the value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Lazy<T> : Future<T> {
        private Result<T> _result;
        private Func<T> _calculation;

        /// <summary>
        /// Initializes a new instance of a <see cref="Lazy{T}"/> <see cref="Future{T}"/>.
        /// </summary>
        /// <param name="calculation">The calculation the new future will do on-demand.</param>
        /// <remarks><paramref name="calculation"/> should be side-effect-free.</remarks>
        public Lazy(Func<T> calculation) {
            _calculation = calculation;
            _result = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lazy{T}"/> class which already holds
        /// a result.
        /// </summary>
        /// <param name="result">The result.</param>
        public Lazy(Result<T> result) {
            _calculation = null;
            _result = result;
        }

        /// <summary>
        /// Gets a value indicating whether this future is lazy (and not forced yet).
        /// </summary>
        /// <value><c>true</c> if this future is not forced yet; otherwise, <c>false</c>.</value>
        public override bool IsLazy {
            get { return !IsCompleted; }
        }

        /// <summary>
        /// Gets the result of the future, whether it is failed or successful. If the result isn't 
        /// available yet, blocks the thread until it is obtained.
        /// </summary>
        /// <value>The result.</value>
        public override Result<T> Result {
            get {
                if (!IsCompleted) {
                    _result = Core.Result.Try(_calculation);
                    _calculation = null;
                }
                return _result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a result (is successful or failed).
        /// </summary>
        /// <value>
        /// <c>true</c> if this lazy value has been forced; otherwise, <c>false</c>.
        /// </value>
        public override bool IsCompleted {
            get { return _calculation == null; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Future{T}"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return ToStringHelper("Lazy");
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Lazy{T}"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="lazy">The lazy value.</param>
        /// <returns>The result of forcing this lazy value.</returns>
        public static explicit operator T(Lazy<T> lazy) {
            return lazy.Await();
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="T"/> to <see cref="Lazy{T}"/>.
        /// </summary>
        /// <param name="t">The lazy value.</param>
        /// <returns>The ready future with result <paramref name="t"/>.</returns>
        public static implicit operator Lazy<T>(T t) {
            return new Lazy<T>(Core.Result.Success(t));
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Result{T}"/> to <see cref="Lazy{T}"/>.
        /// </summary>
        /// <param name="result">The lazy value.</param>
        /// <returns>The ready future with result <paramref name="result"/>.</returns>
        public static implicit operator Lazy<T>(Result<T> result) {
            return new Lazy<T>(result);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="FP.Future.Lazy&lt;T&gt;"/> to <see cref="System.Func&lt;T&gt;"/>.
        /// </summary>
        /// <param name="lazy">The lazy value.</param>
        /// <returns>The calculation needed to return the value.</returns>
        public static implicit operator Func<T>(Lazy<T> lazy) {
            return lazy.IsCompleted ? () => lazy._result.Value : lazy._calculation;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Func&lt;T&gt;"/> to 
        /// <see cref="FP.Future.Lazy&lt;T&gt;"/>.
        /// </summary>
        /// <param name="calculation">The calculation the returned future will do
        /// on-demand.</param>
        /// <returns>The lazy future with the specified calculation.</returns>
        public static implicit operator Lazy<T>(Func<T> calculation) {
            return new Lazy<T>(calculation);
        }
    }
}