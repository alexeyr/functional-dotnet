using System;

namespace FP.Future {
    /// <summary>
    /// Thrown if the result of a future is the same future.
    /// </summary>
    public class CyclicFutureException : InvalidOperationException {}
}
