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

using System;
using System.Diagnostics;
using System.Threading;
using FP.Core;

namespace FP.Future {
    /// <summary>
    /// Represents a <see cref="Future{T}"/> for which the result can be set externally.
    /// </summary>
    public class Promise<T> : Future<T> {
        /// <summary>
        /// Returns a new <see cref="Promise{T}"/> which can be fulfilled later.
        /// </summary>
        public Promise() {
            _future = null;
        }

        private Future<T> _future;

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
        /// Is fulfilled
        /// </summary>
        public bool IsFulfilled { get { return _future != null; } } // IsFulfilled

        /// <summary>
        /// Occurs when the promise is fulfilled (possibly by another future).
        /// </summary>
        public event EventHandler<PromiseFulfilledArgs<T>> Fulfilled;

        /// <summary>
        /// Called when the promise is fulfilled with a final result.
        /// </summary>
        protected void OnFulfilled() {
            if (Fulfilled != null)
                Fulfilled(this, new PromiseFulfilledArgs<T>(_future));
            if (!_future.IsCompleted)
                _future.Determined += delegate { OnDetermined(); };
            else
                OnDetermined();
        }

        /// <summary>
        /// Fulfills the promise with the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <exception cref="PromiseAlreadyFulfilledException">if the promise has already
        /// been fulfilled or failed.</exception>
        public void Fulfill(Result<T> result) {
            Fulfill(new Lazy<T>(result));
        }

        /// <summary>
        /// Fulfills the promise with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="PromiseAlreadyFulfilledException">if the promise has already
        /// been fulfilled or failed.</exception>
        public void Fulfill(T value) {
            Fulfill((Result<T>) Core.Result.Success(value));
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
                if (IsFulfilled) throw new PromiseAlreadyFulfilledException();
                var promise = future as Promise<T>;
                if (promise != null && promise.Future == this) throw new CyclicFutureException();
                _future = future;
                OnFulfilled();
            }
        }

        /// <summary>
        /// Fails the promise with the specified exception.
        /// </summary>
        /// <param name="e">The exception.</param>
        /// <exception cref="PromiseAlreadyFulfilledException">if the promise has already
        /// been fulfilled or failed.</exception>
        public void Fail(Exception e) {
            Fulfill(Core.Result.Failure<T>(e));
        }

        /// <summary>
        /// Gets the result of the future, whether it is failed or determined. If the result isn't 
        /// available yet, blocks the calling thread until it is obtained.
        /// </summary>
        /// <value>The result.</value>
        public override Result<T> Result {
            get {
                if (_future == null) {
                    using (var resetEvent = new ManualResetEvent(false)) {
                        EventHandler<PromiseFulfilledArgs<T>> endWait =
                            (sender, e) => resetEvent.Set();
                        Fulfilled += endWait;
                        resetEvent.WaitOne();
                    }
                }
                Debug.Assert(_future != null);
                return _future.Result;
            }
        }

        /// <summary>
        /// Gets the status of the future.
        /// </summary>
        /// <value>The status.</value>
        public override Status Status {
            get {
                return IsFulfilled
                           ? _future.Status
                           : Status.Future;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a result (is successful or failed).
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has a result; otherwise, <c>false</c>.
        /// </value>
        public override bool IsCompleted {
            get { return IsFulfilled && _future.IsCompleted; }
        }

        /// <summary>
        /// Gets a value indicating whether this future is lazy (and not forced yet).
        /// </summary>
        /// <value><c>true</c> if the promise has been fulfilled with a lazy future;
        /// otherwise, <c>false</c>.</value>
        public override bool IsLazy {
            get { return IsFulfilled && _future.IsLazy; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current 
        /// <see cref="Future{T}"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return _future == null
                       ? ToStringHelper("Promise")
                       : string.Format("Promise({0})", _future);
        }
    }
}