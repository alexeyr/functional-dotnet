using System;
using System.Collections;
using System.Collections.Generic;
using FP.Core;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A vector (nearly array-like sequence).
    /// </summary>
    [Serializable]
    public class BitVector<T> : IRandomAccessSequenceRead<T, BitVector<T>> {
        private readonly int _count;
        private readonly int _shift;
        private Maybe<T> _data;
        private BitVector<T>[] _left;
        private BitVector<T>[] _right;

        public IEnumerator<T> GetEnumerator() {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public int Count {
            get { throw new System.NotImplementedException(); }
        }

        public bool IsEmpty {
            get { throw new System.NotImplementedException(); }
        }

        public BitVector<T> Subsequence(int startIndex, int count) {
            throw new System.NotImplementedException();
        }

        public T this[int index] {
            get { throw new System.NotImplementedException(); }
        }
    }
}