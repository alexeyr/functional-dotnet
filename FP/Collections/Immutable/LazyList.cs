/* (C) Alexey Romanov 2008
 * Code adapted from http://blogs.msdn.com/wesdyer/archive/2007/02/12/why-all-of-the-love-for-lists.aspx
 */

using System.Collections.Generic;

namespace FP.Collections.Immutable {
    public class LazyList<T> : ImmutableListBase<T> {
        IImmutableList<T> _tail;
        readonly T _head;
        IEnumerator<T> _enumerator;

        LazyList(T value, IEnumerator<T> enumerator) {
            _head = value;
            _enumerator = enumerator;
        }

        public static IImmutableList<T> Create(IEnumerable<T> enumerable) {
            return Create(enumerable.GetEnumerator());
        }

        public static IImmutableList<T> Create(IEnumerator<T> enumerator) {
            return enumerator.MoveNext()
                       ? new LazyList<T>(enumerator.Current, enumerator)
                       : null;
        }

        public override IImmutableList<T> Tail {
            get {
                if (_enumerator != null) {
                    _tail = Create(_enumerator);
                    _enumerator = null;
                }
                return _tail;
            }
        }

        public override bool IsEmpty {
            get { return false; }
        }

        public override T Head { get { return _head; } }
    }
}