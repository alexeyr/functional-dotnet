/*
* LazyValue.cs is part of functional-dotnet project
* 
* Copyright (c) 2009 Alexey Romanov
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

namespace FP.Core {
    /// <summary>
    /// A lazy value.
    /// </summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    /// <remarks>The difference with <see cref="LazyValue{T}"/> is that this class doesn't
    /// allow for calculation of the value to throw exceptions, removing a level of
    /// misdirection. Do _not_ use this class if there is any possibility that an
    /// exception will be thrown.</remarks>
    public class LazyValue<T> {
        private T _value;
        private Func<T> _calculation;
        private object _syncRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyValue{T}"/> class.
        /// </summary>
        /// <param name="calculation">The calculation the new future will do on-demand.</param>
        /// <remarks><paramref name="calculation"/> should be side-effect-free.</remarks>
        public LazyValue(Func<T> calculation) {
            _calculation = calculation;
            _syncRoot = new object();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyValue{T}"/> class which already
        /// holds a value.
        /// </summary>
        /// <param name="value">The result.</param>
        public LazyValue(T value) {
            _calculation = null;
            _value = value;
        }

        /// <summary>
        /// Gets the value of the lazy calculation.
        /// </summary>
        /// <value>The value.</value>
        /// <remarks>If the calculation has not been done before, it is called and its
        /// result stored.</remarks>
        public T Value {
            get {
                Force();
                return _value;
            }
        }

        /// <summary>
        /// Forces calculation of this lazy calculation's value.
        /// </summary>
        public void Force() {
            if (!IsCompleted) {
                lock (_syncRoot) {
                    _value = _calculation();
                    _calculation = null;
                    _syncRoot = null;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a result (is successful or failed).
        /// </summary>
        /// <value>
        /// <c>true</c> if this lazy value has been forced; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleted {
            get { return _calculation == null; }
        }

        public override string ToString() {
            return IsCompleted ? _value.ToString() : "Lazy";
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="LazyValue{T}"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="lazy">The lazy value.</param>
        /// <returns>The result of forcing this lazy value.</returns>
        public static explicit operator T(LazyValue<T> lazy) {
            return lazy.Value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="T"/> to <see cref="LazyValue{T}"/>.
        /// </summary>
        /// <param name="t">The lazy value.</param>
        /// <returns>The ready future with result <paramref name="t"/>.</returns>
        public static implicit operator LazyValue<T>(T t) {
            return new LazyValue<T>(t);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="FP.Future.Lazy&lt;T&gt;"/> to <see cref="System.Func&lt;T&gt;"/>.
        /// </summary>
        /// <param name="lazy">The lazy value.</param>
        /// <returns>The calculation needed to return the value.</returns>
        public static explicit operator Func<T>(LazyValue<T> lazy) {
            return lazy.IsCompleted ? () => lazy._value : lazy._calculation;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Func&lt;T&gt;"/> to 
        /// <see cref="FP.Future.Lazy&lt;T&gt;"/>.
        /// </summary>
        /// <param name="calculation">The calculation the returned future will do
        /// on-demand.</param>
        /// <returns>The lazy future with the specified calculation.</returns>
        public static implicit operator LazyValue<T>(Func<T> calculation) {
            return new LazyValue<T>(calculation);
        }
    }
}
