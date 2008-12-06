#region License
/*
* Promise.cs is part of functional-dotnet project
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
#endregion

using System;
using System.Threading;
using FP.Core;

namespace FP.Future {
    /// <summary>
    /// Represents a <see cref="Future{T}"/> for which the result can be set externally.
    /// </summary>
    public class Promise<T> : Future<T> {
        private Result<T> _result;
        private Future<T> _future;
        private bool _isFulfilled;

        ///<summary>
        ///A future which has the same result as the promise.
        ///</summary>
        public Future<T> Future {
            get {
                lock (this) {
                    return _future ?? this;
                }
            }
        }

        /// <summary>
        /// Occurs when the promise is fulfilled (possibly by another future).
        /// </summary>
        public event EventHandler<PromiseFulfilledArgs<T>> Fulfilled;

        /// <summary>
        /// Called when the promise is fulfilled with a final result.
        /// </summary>
        protected void OnCompletelyFulfilled() {
            _isFulfilled = true;
            if (Fulfilled != null)
                Fulfilled(this, new PromiseFulfilledArgs<T>(true, _result, null));
            OnDetermined(_result);
        }

        /// <summary>
        /// Called when the promise is fulfilled with another future.
        /// </summary>
        protected void OnFutureFulfilled() {
            if (!_future.HasResult) {
                _isFulfilled = true;
                if (Fulfilled != null)
                    Fulfilled(this, new PromiseFulfilledArgs<T>(false, null, _future));
            }
            else {
                _result = _future.Result;
                _future = null;
                OnCompletelyFulfilled();
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
                _result = Core.Result.Success(value);
                OnCompletelyFulfilled();
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
                if (future == this) throw new CyclicFutureException();
                var promise = future as Promise<T>;
                if (promise != null && promise.Future == this) throw new CyclicFutureException();
                if (future.HasResult) {
                    _result = future.Result;
                    OnCompletelyFulfilled();
                }
                else {
                    _future = future;
                    _future.Determined += (sender, args) => {
                                              _result = args.Result;
                                              _future = null;
                                          };
                    OnFutureFulfilled();
                }
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
                OnCompletelyFulfilled();
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
                _result = Core.Result.Failure<T>(e);
                OnCompletelyFulfilled();
            }
        }

        /// <summary>
        /// Gets the result of the future, whether it is failed or determined. If the result isn't 
        /// available yet, blocks the thread until it is obtained.
        /// </summary>
        /// <value>The result.</value>
        public override Result<T> Result {
            get {
                if (!_isFulfilled)
                    using (var resetEvent = new ManualResetEvent(false)) {
                        EventHandler<PromiseFulfilledArgs<T>> endWait = (sender, e) => resetEvent.Set();
                        Fulfilled += endWait;
                        resetEvent.WaitOne();
                    }
                if (_result == null) {
                    _result = _future.Result;
                    _future = null;
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
                return _isFulfilled
                           ? (_result != null
                                  ? _result.Match(
                                        s => FP.Future.Future.Status.Successful,
                                        f => FP.Future.Future.Status.Failed)
                                  : _future.Status)
                           : FP.Future.Future.Status.Future;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this future is lazy (and not forced yet).
        /// </summary>
        /// <value><c>true</c> if the promise has been fulfilled with a lazy future; otherwise, <c>false</c>.</value>
        public override bool IsLazy {
            get { return (_future != null && _future.IsLazy); }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="Future{T}"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return _future != null
                       ? string.Format("Promise({0})", _future)
                       : ToStringHelper("Promise");
        }
    }

}