using System;
using FP.Collections.Immutable;

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
                    _result = Collections.Immutable.Result.Try(_calculation);
                    _hasResult = true;
                    _calculation = null;
                }
                return _result;
            }
        }

        /// <summary>
        /// Gets the status of the future.
        /// </summary>
        /// <value>The status.</value>
        public override Future.Status Status {
            get {
                return _hasResult
                           ? _result.Match(
                                 s => Future.Status.Successful,
                                 f => Future.Status.Failed)
                           : Future.Status.Future;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Future{T}"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return ToStringHelper("Lazy");
        }
    }
}
