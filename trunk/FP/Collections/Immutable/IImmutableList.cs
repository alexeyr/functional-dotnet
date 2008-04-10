using System.Collections.Generic;

namespace FP.Collections.Immutable {
    public interface IImmutableList<T> : IEnumerable<T> {
        T Head { get; }
        IImmutableList<T> Tail { get; }
        bool IsEmpty { get; }
    }
}
