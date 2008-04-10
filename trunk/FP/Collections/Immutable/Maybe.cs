using System;
using System.Collections;
using System.Collections.Generic;
using FP.HaskellNames;

// ReSharper disable RedundantIfElseBlock
namespace FP.Collections.Immutable {

    /// <summary>
    /// A convenience static class to provide static methods for <see cref="Maybe{T}"/> and
    /// <see cref="Nullable{T}"/>.
    /// </summary>
    public static class Maybe {
        /// <summary>
        /// Return a wrapper around the specified non-null object.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="t">The object.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">if <paramref name="t"/> is <c>null</c>.</exception>
        public static Maybe<T> Just<T>(T t) {
            if (t == null)
                throw new ArgumentNullException();
            else
                return new Maybe<T>.Just(t);
        }

        /// <summary>
        /// Returns <see cref="Maybe{T}.Nothing"/>.
        /// </summary>
        public static Maybe<T> Nothing<T>() {
            return Maybe<T>.Nothing;
        }

        /// <summary>
        /// Converts a sequence to maybe.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns><c>Nothing</c> if <paramref name="sequence"/> is empty;
        /// <c>Just(sequence.First())</c> otherwise.</returns>
        public static Maybe<T> ToMaybe<T>(this IEnumerable<T> sequence) {
            using (var enumerator = sequence.GetEnumerator()) {
                if (enumerator.MoveNext())
                    return Just(enumerator.Current);
                else
                    return Nothing<T>();
            }
        }

        /// <summary>
        /// Converts <see cref="Nullable{T}"/> to <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="nullable">The nullable.</param>
        /// <returns><c>Nothing</c> if <paramref name="nullable"/> doesn't have a value;
        /// <c>Just(nullable.Value)</c> otherwise.</returns>
        public static Maybe<T> ToMaybe<T>(this T? nullable) where T : struct {
            if (nullable.HasValue)
                return Just(nullable.Value);
            else
                return Nothing<T>();
        }

        /// <summary>
        /// Converts <see cref="Maybe{T}"/> to <see cref="Nullable{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe">The maybe.</param>
        /// <returns><c>null</c> if <paramref name="maybe"/> is <c>Nothing</c>;
        /// <c>maybe.Value</c> otherwise.</returns>
        public static T? ToNullable<T>(this Maybe<T> maybe) where T : struct {
            if (maybe.HasValue)
                return maybe.Value;
            else
                return null;
        }

        /// <summary>
        /// An explicit conversion from <typeparamref name="T"/> to <see cref="Maybe{T}"/>.
        /// Works the same as the implicit cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns></returns>
        public static Maybe<T> FromValue<T>(T value) {
            return value;
        }

        /// <summary>
        /// Selects the values of elements which have them.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectValues<T>(this IEnumerable<Maybe<T>> sequence) {
            foreach (var maybe in sequence)
                if (maybe.HasValue)
                    yield return maybe.Value;
        }

        /// <summary>
        /// Selects the values of elements which have them.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectValues<T>(this IEnumerable<T?> sequence) where T : struct {
            return sequence.Map(x => x.ToMaybe()).SelectValues();
        }

        /// <summary>
        /// A version of <see cref="Enumerable3.Map{T,TR}"/> which can throw away elements.
        /// In particular, if <c>function(x)</c> doesn't have a value for an element <c>x</c> of 
        /// the <paramref name="sequence"/>, no element is included in the result; if it has value <c>y</c>,
        /// <c>y</c> is included in the list.
        /// </summary>
        /// <typeparam name="T">Type of elements of the sequence.</typeparam>
        /// <typeparam name="R">Type of elements of the resulting sequence.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IEnumerable<R> MapMaybe<T, R>(this IEnumerable<T> sequence, Func<T, Maybe<R>> function) {
            return sequence.Map(function).SelectValues();
        }

        /// <summary>
        /// A version of <see cref="Enumerable3.Map{T,TR}"/> which can throw away elements.
        /// In particular, if <c>function(x)</c> doesn't have a value for an element <c>x</c> of 
        /// the <paramref name="sequence"/>, no element is included in the result; if it has value <c>y</c>,
        /// <c>y</c> is included in the list.
        /// </summary>
        /// <typeparam name="T">Type of elements of the sequence.</typeparam>
        /// <typeparam name="R">Type of elements of the resulting sequence.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public static IEnumerable<R> MapMaybe<T, R>(this IEnumerable<T> sequence, Func<T, R?> function) where R : struct {
            return sequence.Map(function).SelectValues();
        }
    }

    /// <summary>
    /// This class represents an optional value, like <see cref="Nullable{T}"/>, but
    /// works with reference types as well. Also known as <c>Option</c> in ML/F#/Scala.
    /// </summary>
    /// <typeparam name="T">The type of wrapped object.</typeparam>
    /// <remarks>All instances which have value are <see cref="Just"/>, the only instance 
    /// which doesn't have a value is <see cref="Nothing"/>.</remarks>
    /// <seealso cref="Nullable{T}"/>
    [Serializable]
    public abstract class Maybe<T> : IEnumerable<T> {
        /// <summary>
        /// Gets the value, if it exists.
        /// </summary>
        /// <value>The value.</value>
        abstract public T Value { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value><c>true</c> if this instance has value; otherwise, <c>false</c>.</value>
        abstract public bool HasValue { get; }

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public abstract IEnumerator<T> GetEnumerator();

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        private Maybe() {}

        /// <summary>
        /// The wrapper for no value.
        /// </summary>
        public static readonly NothingT Nothing = new NothingT();

        /// <summary>
        /// If the current instance has a value, do <paramref name="action"/> with it.
        /// </summary>
        /// <param name="action">The action to try.</param>
        /// <remarks>Called <c>may</c> in OCaml</remarks>
        public void Try(Action<T> action) {
            TryOrElse(action, Functions.DoNothing);
        }

        /// <summary>
        /// If the current instance has a value, do <paramref name="action"/> with it.
        /// Otherwise do <paramref name="defaultAction"/>.
        /// </summary>
        /// <param name="action">The action to try.</param>
        /// <param name="defaultAction">The default action.</param>
        public void TryOrElse(Action<T> action, Action defaultAction) {
            if (HasValue)
                action(Value);
            else
                defaultAction();
        }

        /// <summary>
        /// If the current instance has a value, return it.
        /// Otherwise return <paramref name="default"/>.
        /// </summary>
        /// <param name="default">The default value.</param>
        /// <returns></returns>
        public T ValueWithDefault(T @default) {
            return HasValue ? Value : @default;
        }

        /// <summary>
        /// If the current instance has a value, calls <paramref name="function"/> on it and returns the result.
        /// Otherwise returns <paramref name="default"/>.
        /// </summary>
        /// <param name="function">The function to call.</param>
        /// <param name="default">The default result.</param>
        public R TryOrElse<R>(Func<T, R> function, R @default) {
            return HasValue ? function(Value) : @default;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="T"/> to <see cref="FP.Collections.Immutable.Maybe{T}"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>Just(value)</c> if value is not null; <c>Nothing</c> otherwise.</returns>
        /// <remarks>It is implicit by parallel with <see cref="Nullable{T}"/>.</remarks>
        public static implicit operator Maybe<T>(T value) {
            if (value != null)
                return new Just(value);
            else
                return Nothing;            
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="FP.Collections.Immutable.Maybe{T}"/> to <see cref="T"/>.
        /// </summary>
        /// <param name="maybe">The maybe.</param>
        /// <returns><see cref="Value"/> if it exists; <c>default(T)</c> otherwise.</returns>
        public static explicit operator T(Maybe<T> maybe) {
            return maybe.ValueWithDefault(default(T));
        }
        
        /// <summary>
        /// Represents the absence of value. Can't be called <c>Nothing</c> to prevent
        /// naming conflict with <see cref="Maybe{T}.Nothing"/>.
        /// </summary>
        public class NothingT : Maybe<T> {
            internal NothingT() {}

            ///<summary>
            ///Returns an enumerator that iterates through the collection.
            ///</summary>
            ///<returns>
            ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> without any elements.
            ///</returns>
            ///<filterpriority>1</filterpriority>
            public override IEnumerator<T> GetEnumerator() {
                yield break;
            }

            /// <summary>
            /// Gets a value indicating whether this instance has value.
            /// </summary>
            /// <value><c>false</c>.</value>
            public override bool HasValue {
                get { return false; }
            }

            /// <summary>
            /// Gets the value, if it exists.
            /// </summary>
            /// <value>The value.</value>
            /// <exception cref="ArgumentException"><c>ArgumentException</c>.</exception>
            public override T Value {
                get { throw new ArgumentException(); }
            }
        }

        /// <summary>
        /// Represents a non-null value.
        /// </summary>
        public class Just : Maybe<T> {
            private readonly T _value;

            internal Just(T value) {
                _value = value;
            }

            ///<summary>
            ///Returns an enumerator that iterates through the collection.
            ///</summary>
            ///
            ///<returns>
            ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> with a single element,
            ///namely <see cref="Value"/>.
            ///</returns>
            ///<filterpriority>1</filterpriority>
            public override IEnumerator<T> GetEnumerator() {
                yield return _value;
            }

            /// <summary>
            /// Gets a value indicating whether this instance has value.
            /// </summary>
            /// <value><c>true</c>.</value>
            public override bool HasValue {
                get { return true; }
            }

            /// <summary>
            /// Gets the value, if it exists.
            /// </summary>
            /// <value>The value.</value>
            public override T Value {
                get { return _value; }
            }
        }
    }
}
// ReSharper restore RedundantIfElseBlock
