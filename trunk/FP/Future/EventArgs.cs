using System;
using FP.Collections.Immutable;

namespace FP.Future {
    public class PromiseFulfilledArgs<T> : EventArgs {
        public Result<T> Result { get; private set; }
        public Future<T> Future { get; private set; }
        public bool IsComplete { get; private set; }

        public PromiseFulfilledArgs(bool isComplete, Result<T> result, Future<T> future) {
            Future = future;
            IsComplete = isComplete;
            Result = result;
        }
    }

    public class FutureDeterminedArgs<T> : EventArgs {
        public Result<T> Result { get; private set; }

        public FutureDeterminedArgs(Result<T> result) {
            Result = result;
        }
    }
}
