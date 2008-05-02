/* (C) Alexey Romanov 2008 */

using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A static class to help with type inference.
    /// </summary>
    public static class Either {
        ///<summary>
        ///Constructs a <see cref="Either{L,R}.Left"/>.
        ///</summary>
        public static Either<L,R>.Left Left<L,R>(L value) {
            return new Either<L,R>.Left(value);
        }

        ///<summary>
        ///Constructs a <see cref="Either{L,R}.Right"/>.
        ///</summary>
        public static Either<L, R>.Right Right<L, R>(R value) {
            return new Either<L, R>.Right(value);
        }
    }

    /// <summary>
    /// A type representing a value of one of two types: <typeparamref name="L"/> or <typeparamref name="R"/>. 
    /// When differentiating a correct value from an error, the convention is to use Right for the correct value.
    /// </summary>
    /// <seealso cref="Result{T}"/>
    /// <seealso cref="Maybe{T}"/>
    /// <typeparam name="L">The left type.</typeparam>
    /// <typeparam name="R">The right type.</typeparam>
    public abstract class Either<L, R> {
        /// <summary>
        /// Gets a value indicating whether this instance is a <see cref="Right"/>.
        /// </summary>
        /// <value><c>true</c> if this instance is a <see cref="Right"/>; otherwise, <c>false</c>.</value>
        public abstract bool IsRight { get; }
        /// <summary>
        /// Case analysis.
        /// </summary>
        /// <param name="onLeft">Action to do if this is a <see cref="Left"/>.</param>
        /// <param name="onRight">Action to do if this is a <see cref="Right"/>.</param>
        public abstract void Match(Action<L> onLeft, Action<R> onRight);
        /// <summary>
        /// Case analysis.
        /// </summary>
        /// <param name="onLeft">Function to apply if this is a <see cref="Left"/>.</param>
        /// <param name="onRight">Function to apply if this is a <see cref="Right"/>.</param>
        public abstract Res Match<Res>(Func<L, Res> onLeft, Func<R, Res> onRight);

        /// <summary>
        /// Represents a value of the type <typeparamref name="L"/>.
        /// </summary>
        public sealed class Left : Either<L,R> {
            private readonly L _value;

            /// <summary>
            /// Initializes a new instance of the <see cref="Either&lt;L, R&gt;.Left"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public Left(L value) {
                _value = value;
            }

            /// <summary>
            /// Gets a value indicating whether this instance is a <see cref="Either{L,R}.Right"/>.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance is a <see cref="Either{L,R}.Right"/>; otherwise, <c>false</c>.
            /// </value>
            public override bool IsRight {
                get { return false; }
            }

            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>The value.</value>
            public L Value {
                get { return _value; }
            }

            /// <summary>
            /// Case analysis. Does <paramref name="onLeft"/>.
            /// </summary>
            /// <param name="onLeft">Action to do if this is a <see cref="Either{L,R}.Left"/>.</param>
            /// <param name="onRight">Action to do if this is a <see cref="Either{L,R}.Right"/>.</param>
            public override void Match(Action<L> onLeft, Action<R> onRight) {
                onLeft(_value);
            }

            /// <summary>
            /// Case analysis. Applies <paramref name="onLeft"/>.
            /// </summary>
            /// <param name="onLeft">Function to apply if this is a <see cref="Either{L,R}.Left"/>.</param>
            /// <param name="onRight">Function to apply if this is a <see cref="Either{L,R}.Right"/>.</param>
            public override Res Match<Res>(Func<L, Res> onLeft, Func<R, Res> onRight) {
                return onLeft(_value);
            }
        }

        /// <summary>
        /// Represents a value of the type <typeparamref name="L"/>.
        /// </summary>
        public sealed class Right : Either<L, R> {
            private readonly R _value;

            /// <summary>
            /// Initializes a new instance of the <see cref="Either&lt;L, R&gt;.Right"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public Right(R value) {
                _value = value;
            }

            /// <summary>
            /// Gets a value indicating whether this instance is a <see cref="Either{L,R}.Right"/>.
            /// </summary>
            /// <value>
            /// 	<c>true</c> if this instance is a <see cref="Either{L,R}.Right"/>; otherwise, <c>false</c>.
            /// </value>
            public override bool IsRight {
                get { return true; }
            }

            /// <summary>
            /// Case analysis. Does <paramref name="onRight"/>.
            /// </summary>
            /// <param name="onLeft">Action to do if this is a <see cref="Either{L,R}.Left"/>.</param>
            /// <param name="onRight">Action to do if this is a <see cref="Either{L,R}.Right"/>.</param>
            public override void Match(Action<L> onLeft, Action<R> onRight) {
                onRight(_value);
            }

            /// <summary>
            /// Case analysis. Applies <paramref name="onRight"/>.
            /// </summary>
            /// <param name="onLeft">Function to apply if this is a <see cref="Either{L,R}.Left"/>.</param>
            /// <param name="onRight">Function to apply if this is a <see cref="Either{L,R}.Right"/>.</param>
            public override Res Match<Res>(Func<L, Res> onLeft, Func<R, Res> onRight) {
                return onRight(_value);
            }

            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>The value.</value>
            public R Value {
                get { return _value; }
            }
        }
    }
}
