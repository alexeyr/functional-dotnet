using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A static class to help with type inference.
    /// </summary>
    public static class Result {
        public static Result<T>.Success Success<T>(T value) {
            return new Result<T>.Success(value);
        }

        public static Result<T>.Failure Failure<T>(string reason) {
            return new Result<T>.Failure(reason);
        }

        public static Result<T>.Failure Failure<T>(Exception exception) {
            return new Result<T>.Failure(exception);
        }

        /// <summary>
        /// Tries the specified function.
        /// </summary>
        /// <typeparam name="T">The return type of the function.</typeparam>
        /// <param name="function">The function.</param>
        /// <returns><see cref="Result{T}.Failure"/> if <paramref name="function"/> throws exception;
        /// <see cref="Result{T}.Success"/> otherwise.</returns>
        public static Result<T> Try<T>(this Func<T> function) {
            try {
                return Success(function());
            }
            catch (Exception e) {
                return Failure<T>(e);
            }
        }
    }

    /// <summary>
    /// A class which describes a result of a caluculation, which can succeed or fail.
    /// </summary>
    /// <seealso cref="Either{L,R}"/>
    /// <seealso cref="Maybe{T}"/>
    /// <typeparam name="T">The type of the result.</typeparam>
    public abstract class Result<T> {
        private Result() {}
        /// <summary>
        /// Case analysis on results.
        /// </summary>
        /// <param name="onSuccess">The action to do if the result is a success.</param>
        /// <param name="onFailure">The action to do if the result is a failure.</param>
        public abstract void Match(Action<T> onSuccess, Action<string> onFailure);
        /// <summary>
        /// Case analysis on results.
        /// </summary>
        /// <param name="onSuccess">The function to evaluate if the result is a success.</param>
        /// <param name="onFailure">The function to evaluate if the result is a failure.</param>
        public abstract void Match<R>(Func<T, R> onSuccess, Func<string, R> onFailure);
        /// <summary>
        /// Case analysis on results.
        /// </summary>
        /// <param name="onSuccess">The action to do if the result is a success.</param>
        /// <param name="onFailure">The action to do if the result is a failure.</param>
        public abstract void Match(Action<T> onSuccess, Action<Exception> onFailure);
        /// <summary>
        /// Case analysis on results.
        /// </summary>
        /// <param name="onSuccess">The function to evaluate if the result is a success.</param>
        /// <param name="onFailure">The function to evaluate if the result is a failure.</param>
        public abstract void Match<R>(Func<T, R> onSuccess, Func<Exception, R> onFailure);
        /// <summary>
        /// Converts the result to <see cref="Maybe{T}"/>.
        /// </summary>
        /// <returns></returns>
        public abstract Maybe<T> ToMaybe();

        /// <summary>
        /// Implements the operator false.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if the result is <see cref="Failure"/>.</returns>
        public static bool operator false(Result<T> result) {
            return result is Failure;
        }
        /// <summary>
        /// Implements the operator true.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if the result is <see cref="Success"/>.</returns>
        public static bool operator true(Result<T> result) {
            return result is Success;
        }

        /// <summary>
        /// Represents a failure.
        /// </summary>
        public sealed class Failure : Result<T> {
            /// <summary>
            /// Gets the reason.
            /// </summary>
            /// <value>The reason.</value>
            public string Reason { get; private set; }

            /// <summary>
            /// Gets the exception.
            /// </summary>
            /// <value>The exception.</value>
            public Exception Exception { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Result&lt;T&gt;.Failure"/> class.
            /// </summary>
            /// <param name="reason">The reason.</param>
            public Failure(string reason) {
                Reason = reason;
                Exception = new Exception(reason);
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Result&lt;T&gt;.Failure"/> class.
            /// </summary>
            /// <param name="exception">The exception.</param>
            public Failure(Exception exception) {
                Reason = exception.Message;
                Exception = exception;
            }

            /// <summary>
            /// Performs an implicit conversion from <see cref="FP.Collections.Immutable.Result&lt;T&gt;.Failure"/> to <see cref="System.String"/>.
            /// </summary>
            /// <param name="failure">The failure.</param>
            /// <returns>The reason of the failure.</returns>
            public static implicit operator string(Failure failure) {
                return failure.Reason;
            }

            /// <summary>
            /// Performs an implicit conversion from <see cref="FP.Collections.Immutable.Result&lt;T&gt;.Failure"/> to <see cref="System.Exception"/>.
            /// </summary>
            /// <param name="failure">The failure.</param>
            /// <returns>The exception of the failure.</returns>
            public static implicit operator Exception(Failure failure) {
                return failure.Exception;
            }

            public override string ToString() {
                return Reason;
            }

            /// <summary>
            /// Case analysis on results.
            /// </summary>
            /// <param name="onSuccess">The function to evaluate if the result is a success.</param>
            /// <param name="onFailure">The function to evaluate if the result is a failure.</param>
            public override void Match<R>(Func<T, R> onSuccess, Func<string, R> onFailure) {
                onFailure(Reason);
            }

            /// <summary>
            /// Case analysis on results.
            /// </summary>
            /// <param name="onSuccess">The action to do if the result is a success.</param>
            /// <param name="onFailure">The action to do if the result is a failure.</param>
            public override void Match(Action<T> onSuccess, Action<string> onFailure) {
                onFailure(Reason);
            }

            /// <summary>
            /// Case analysis on results.
            /// </summary>
            /// <param name="onSuccess">The function to evaluate if the result is a success.</param>
            /// <param name="onFailure">The function to evaluate if the result is a failure.</param>
            public override void Match<R>(Func<T, R> onSuccess,
                                          Func<Exception, R> onFailure) {
                onFailure(Exception);
            }

            /// <summary>
            /// Case analysis on results.
            /// </summary>
            /// <param name="onSuccess">The action to do if the result is a success.</param>
            /// <param name="onFailure">The action to do if the result is a failure.</param>
            public override void Match(Action<T> onSuccess, Action<Exception> onFailure) {
                onFailure(Exception);
            }

            /// <summary>
            /// Converts the result to <see cref="Maybe{T}"/>.
            /// </summary>
            /// <returns></returns>
            public override Maybe<T> ToMaybe() {
                return Maybe<T>.Nothing;
            }
        }

        /// <summary>
        /// Represents a success.
        /// </summary>
        public sealed class Success : Result<T> {
            /// <summary>
            /// Gets the value.
            /// </summary>
            /// <value>The value.</value>
            public T Value { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Result&lt;T&gt;.Success"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public Success(T value) {
                Value = value;
            }

            /// <summary>
            /// Performs an implicit conversion from <see cref="FP.Collections.Immutable.Result&lt;T&gt;.Success"/> to <see cref="T"/>.
            /// </summary>
            /// <param name="success">The success.</param>
            /// <returns>The value.</returns>
            public static implicit operator T(Success success) {
                return success.Value;
            }

            /// <summary>
            /// Case analysis on results.
            /// </summary>
            /// <param name="onSuccess">The function to evaluate if the result is a success.</param>
            /// <param name="onFailure">The function to evaluate if the result is a failure.</param>
            public override void Match<R>(Func<T, R> onSuccess, Func<string, R> onFailure) {
                onSuccess(Value);
            }

            /// <summary>
            /// Case analysis on results.
            /// </summary>
            /// <param name="onSuccess">The action to do if the result is a success.</param>
            /// <param name="onFailure">The action to do if the result is a failure.</param>
            public override void Match(Action<T> onSuccess, Action<string> onFailure) {
                onSuccess(Value);
            }

            /// <summary>
            /// Case analysis on results.
            /// </summary>
            /// <param name="onSuccess">The function to evaluate if the result is a success.</param>
            /// <param name="onFailure">The function to evaluate if the result is a failure.</param>
            public override void Match<R>(Func<T, R> onSuccess,
                                          Func<Exception, R> onFailure) {
                onSuccess(Value);
            }

            /// <summary>
            /// Case analysis on results.
            /// </summary>
            /// <param name="onSuccess">The action to do if the result is a success.</param>
            /// <param name="onFailure">The action to do if the result is a failure.</param>
            public override void Match(Action<T> onSuccess, Action<Exception> onFailure) {
                onSuccess(Value);
            }

            /// <summary>
            /// Converts the result to <see cref="Maybe{T}"/>.
            /// </summary>
            /// <returns></returns>
            public override Maybe<T> ToMaybe() {
                return Maybe.Just(Value);
            }
        }
    }
}
