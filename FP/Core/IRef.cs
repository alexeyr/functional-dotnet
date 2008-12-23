using System;

namespace FP.Core {
    /// <summary>
    /// An interface for mutable references to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values.</typeparam>
    /// <remarks>Type <typeparamref name="T"/> should be immutable or at least the value
    /// should not be mutated. In this case the use of this type is thread-safe.
    /// All value changes except directly mutating the value are atomic.
    /// 
    /// Similar to Alice ML's/OCaml's ref type and Clojure's atom type.</remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRef<T> {
        /// <summary>
        /// Value of the reference.
        /// </summary>
        T Value { get; set; }

        /// <summary>
        /// The validator. Must be side-effect free. This will be called before any change
        /// of value with the intended new value as the argument and should throw an exception
        /// if the change is invalid.
        /// </summary>
        /// <value>The validator function.</value>
        Action<T> Validator { get; }

        /// <summary>
        /// Stores the specified new value.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <returns>The old value.</returns>
        /// <exception cref="RefValidationException">when <paramref name="newValue"/> doesn't
        /// pass <see cref="Ref{T}.Validator"/>.</exception>
        T Store(T newValue);

        /// <summary>
        /// Atomically sets <see cref="Ref{T}.Value"/> to <paramref name="newValue"/> if and only
        /// if the current value of the atom is identical to <see cref="oldValue"/>
        /// according to <c>Value.Equals(oldValue)</c> and <c>Validator(newValue)</c>
        /// succeeds.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns><c>true</c> if the change happened, else <c>false</c>.</returns>
        /// <exception cref="RefValidationException">when <paramref name="newValue"/> doesn't
        /// pass <see cref="Ref{T}.Validator"/>.</exception>
        bool CompareAndSet(T oldValue, T newValue);

        /// <summary>
        /// Atomically replaces the current value of this ref with
        /// <c>f(Value)</c>.
        /// </summary>
        /// <param name="f">The function to apply to the value. Must be side-effect free.
        /// </param>
        /// <returns>The pair (value swapped out, value swapped in).
        /// </returns>
        /// <remarks>Reads the current <see cref="Ref{T}.Value"/>, applies <paramref name="f"/> to
        /// it, and attempts to <see cref="Ref{T}.CompareAndSet"/> the result in. If this doesn't
        /// succeed, retry in a spin loop. The net effect is that the value will always be
        /// the result of the application of the supplied function to a current value,
        /// atomically. However, because <paramref name="f"/> might be called multiple
        /// times, it must be free of side effects. <c>false</c> is only returned if
        /// validation of the replacement value fails.
        /// </remarks>
        /// <exception cref="RefValidationException"> when the new value doesn't
        /// pass <see cref="Ref{T}.Validator"/>.</exception>
        Tuple<T, T> Modify(Func<T, T> f);

        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The intended new value for this reference.</param>
        /// <exception cref="RefValidationException">when <paramref name="value"/> is
        /// not valid.</exception>
        void Validate(T value);
    }
}