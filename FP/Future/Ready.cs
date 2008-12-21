using FP.Core;

namespace FP.Future {
    /// <summary>
    /// A finished future.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    public class Ready<T> : Future<T> {
        private readonly Result<T> _result;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ready&lt;T&gt;"/> class holding 
        /// <paramref name="result"/>.
        /// </summary>
        /// <param name="result">The result.</param>
        public Ready(Result<T> result) {
            _result = result;
        }

        public override Result<T> Result {
            get { return _result; }
        }

        public override bool HasResult {
            get { return true; }
        }

        public override bool IsLazy {
            get { return false; }
        }
    }
}