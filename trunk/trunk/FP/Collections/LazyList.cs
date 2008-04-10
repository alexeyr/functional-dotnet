/* (C) Alexey Romanov 2008
 * Code adapted from http://blogs.msdn.com/wesdyer/archive/2007/02/12/why-all-of-the-love-for-lists.aspx
  */

using System.Collections;
using System.Collections.Generic;

public class LazyList<T> : IEnumerable<T> {
    IEnumerable<T> _next;
    readonly T _current;
    IEnumerator<T> _enumerator;

    LazyList(T value, IEnumerator<T> enumerator) {
        _current = value;
        _enumerator = enumerator;
    }

    public static IEnumerable<T> Create(IEnumerable<T> enumerable) {
        return Create(enumerable.GetEnumerator());
    }

    public static IEnumerable<T> Create(IEnumerator<T> enumerator) {
        return enumerator.MoveNext() 
            ? new LazyList<T>(enumerator.Current, enumerator) 
            : null;
    }

    public IEnumerable<T> Next {
        get {
            if (_enumerator != null) {
                _next = Create(_enumerator);
                _enumerator = null;
            }
            return _next;
        }
    }

    public T Current { get { return _current; } }

    ///<summary>
    ///Returns an enumerator that iterates through a collection.
    ///</summary>
    ///
    ///<returns>
    ///An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    ///</returns>
    ///<filterpriority>2</filterpriority>
    IEnumerator IEnumerable.GetEnumerator() {
        return ((IEnumerable<T>) this).GetEnumerator();
    }

    ///<summary>
    ///Returns an enumerator that iterates through the collection.
    ///</summary>
    ///
    ///<returns>
    ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    ///</returns>
    ///<filterpriority>1</filterpriority>
    public IEnumerator<T> GetEnumerator() {
        IEnumerable<T> seq = this;
        while (seq != null) {
            var ll = seq as LazyList<T>;
            if (ll != null) {
                yield return ll._current;
                seq = ll._next;
            }
            else {
                foreach (var t in seq)
                    yield return t;
                seq = null;
            }
        }
    }
}