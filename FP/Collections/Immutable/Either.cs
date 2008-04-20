using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A static class to help with type inference.
    /// </summary>
    public static class Either {
        public static Either<L,R>.Left Left<L,R>(L value) {
            return new Either<L,R>.Left(value);
        }

        public static Either<L, R>.Right Right<L, R>(R value) {
            return new Either<L, R>.Right(value);
        }
    }

    /// <summary>
    /// A type representing a value of one of two types. When differentiating a correct value from
    /// an error, the convention is to use Right for the correct value.
    /// </summary>
    /// <seealso cref="Result{T}"/>
    /// <seealso cref="Maybe{T}"/>
    /// <typeparam name="L">The left type.</typeparam>
    /// <typeparam name="R">The right type.</typeparam>
    public abstract class Either<L, R> {
        public abstract bool IsRight { get; }
        public abstract L LeftValue { get; }
        public abstract R RightValue { get; }
        public abstract void Match(Action<L> onLeft, Action<R> onRight);
        public abstract Res Match<Res>(Func<L, Res> onLeft, Func<R, Res> onRight);

        public sealed class Left : Either<L,R> {
            private readonly L _value;

            public Left(L value) {
                _value = value;
            }

            public override bool IsRight {
                get { return false; }
            }

            public override L LeftValue {
                get { return _value; }
            }

            public override void Match(Action<L> onLeft, Action<R> onRight) {
                onLeft(_value);
            }

            public override Res Match<Res>(Func<L, Res> onLeft, Func<R, Res> onRight) {
                return onLeft(_value);
            }

            public override R RightValue {
                get { throw new InvalidOperationException("Tried to retrieve the Right value from a Left."); }
            }
        }

        public sealed class Right : Either<L,R> {
            private readonly R _value;

            public Right(R value) {
                _value = value;
            }

            public override bool IsRight {
                get { return true; }
            }

            public override L LeftValue {
                get { throw new InvalidOperationException("Tried to retrieve the Left value from a Right."); }
            }

            public override void Match(Action<L> onLeft, Action<R> onRight) {
                onRight(_value);
            }

            public override Res Match<Res>(Func<L, Res> onLeft, Func<R, Res> onRight) {
                return onRight(_value);
            }

            public override R RightValue {
                get { return _value; }
            }
        }
    }
}
