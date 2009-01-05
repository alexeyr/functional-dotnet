using FP.Future;
using System;

namespace FP.Core {
    /// <summary>
    /// A lazy value.
    /// </summary>
    /// <typeparam name="T">Type of the value.</typeparam>
    /// <remarks>The difference with <see cref="Lazy{T}"/> is that this class doesn't
    /// allow for calculation of the value to throw exceptions, removing a level of
    /// misdirection. Do _not_ use this class if there is any possibility that an
    /// exception will be thrown.</remarks>
    public class LazyValue<T> {
        private T _value;
        private Func<T> _calculation;

        /// <summary>
        /// Initializes a new instance of a <see cref="Lazy{T}"/> <see cref="Future{T}"/>.
        /// </summary>
        /// <param name="calculation">The calculation the new future will do on-demand.</param>
        /// <remarks><paramref name="calculation"/> should be side-effect-free.</remarks>
        public LazyValue(Func<T> calculation) {
            _calculation = calculation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Lazy{T}"/> class which already holds
        /// a value.
        /// </summary>
        /// <param name="value">The result.</param>
        public LazyValue(T value) {
            _calculation = null;
            _value = value;
        }

        /// <summary>
        /// Gets the result of the future, whether it is failed or successful. If the result isn't 
        /// available yet, blocks the thread until it is obtained.
        /// </summary>
        /// <value>The result.</value>
        public T Value {
            get {
                if (!IsCompleted) {
                    _value = _calculation();
                    _calculation = null;
                }
                return _value;
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

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Future{T}"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return IsCompleted ? _value.ToString() : "Lazy";
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Lazy{T}"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="lazy">The lazy value.</param>
        /// <returns>The result of forcing this lazy value.</returns>
        public static explicit operator T(LazyValue<T> lazy) {
            return lazy.Value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="T"/> to <see cref="Lazy{T}"/>.
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
