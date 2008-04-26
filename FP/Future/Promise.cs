using System;
using System.Threading;
using FP.Collections.Immutable;

namespace FP.Future {
    /// <summary>
    /// Represents a <see cref="Future{T}"/> for which the result can be set externally.
    /// </summary>
    public class Promise<T> : Future<T> {
        private Result<T> _result;
        private Future<T> _future;
        private bool _isFulfilled;

        public Future<T> Future {
            get {
                lock (this) {
                    return _future ?? this;
                }
            }
        }
        /// <summary>
        /// Fulfills the promise with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="PromiseAlreadyFulfilledException">if the promise has already
        /// been fulfilled or failed.</exception>
        public void Fulfill(T value) {
            lock (this) {
                if (_isFulfilled) throw new PromiseAlreadyFulfilledException();
                _result = Result.Success(value);
                _isFulfilled = true;
            }
        }

        /// <summary>
        /// Fulfills the promise with the specified future.
        /// </summary>
        /// <param name="future">The future.</param>
        /// <exception cref="PromiseAlreadyFulfilledException">if the promise has already
        /// been fulfilled or failed.</exception>
        /// <exception cref="CyclicFutureException">if the future is this promise.</exception>
        public void Fulfill(Future<T> future) {
            lock (this) {
                if (_isFulfilled) throw new PromiseAlreadyFulfilledException();
                if (this == future) throw new CyclicFutureException();
                _future = future;
                _isFulfilled = true;
            }
        }

        /// <summary>
        /// Fulfills the promise with the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <exception cref="PromiseAlreadyFulfilledException">if the promise has already
        /// been fulfilled or failed.</exception>
        public void Fulfill(Result<T> result) {
            lock (this) {
                if (_isFulfilled) throw new PromiseAlreadyFulfilledException();
                _result = result;
                _isFulfilled = true;
            }
        }

        /// <summary>
        /// Fails the promise with the specified exception.
        /// </summary>
        /// <param name="e">The exception.</param>
        /// <exception cref="PromiseAlreadyFulfilledException">if the promise has already
        /// been fulfilled or failed.</exception>
        public void Fail(Exception e) {
            lock (this) {
                if (_isFulfilled) throw new PromiseAlreadyFulfilledException();
                _result = Result.Failure<T>(e);
                _isFulfilled = true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this future is lazy.
        /// </summary>
        /// <value><c>false</c>.</value>
        public override bool IsLazy {
            get { return false; }
        }

        /// <summary>
        /// Requests the result of the future. If the result isn't yet available, blocks the thread
        /// until it is obtained.
        /// </summary>
        /// <returns></returns>
        public override T Request() {
            while (!_isFulfilled)
                Thread.Sleep(100);
            return _result != null ? _result.Value : _future.Request();
        }

        /// <summary>
        /// Gets the status of the future.
        /// </summary>
        /// <value>The status.</value>
        public override Future.Status Status {
            get { return _isFulfilled ? FP.Future.Future.Status.Determined : FP.Future.Future.Status.Future; }
        }
    }
}
