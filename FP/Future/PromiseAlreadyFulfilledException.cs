using System;

namespace FP.Future {
    /// <summary>
    /// Thrown on multiple attempts to fulfill a promise.
    /// </summary>
    public class PromiseAlreadyFulfilledException : InvalidOperationException {}
}
