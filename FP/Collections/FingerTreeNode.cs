/*
* FingerTreeNode.cs is part of functional-dotnet project
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FP.Collections {
    /// <summary>
    /// A node in the middle section of a deep finger tree.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the node.</typeparam>
    /// <typeparam name="V">Type of the weight monoid.</typeparam>
    internal abstract class FTNode<T, V> : IMeasured<V>, IEnumerable<T>, IFoldable<T>
        where T : IMeasured<V> {
        private FTNode() {}

        public abstract A FoldRight<A>(Func<T, A, A> binOp, A initial);
        public abstract A FoldLeft<A>(Func<A, T, A> binOp, A initial);
        public abstract FTNode<T, V> Reverse(Func<T, T> f);
        public V Measure { get; private set; }

        internal abstract T[] ToArray();

        ///<summary>
        ///Returns an enumerator that iterates through the node.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the node.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// A node with two subtrees.
        /// </summary>
        [DebuggerDisplay("Node2({Item1}, {Item2})")]
        internal class Node2 : FTNode<T, V>, IEquatable<Node2> {
            public readonly T Item1;
            public readonly T Item2;

            public Node2(T item1, T item2, Monoid<V> monoid) :
                this(item1, item2, monoid.Plus(item1.Measure, item2.Measure)) {}

            internal Node2(T item1, T item2, V measure) {
                Item1 = item1;
                Item2 = item2;
                Measure = measure;
            }

            public override A FoldRight<A>(Func<T, A, A> binOp, A initial) {
                return binOp(Item1, binOp(Item2, initial));
            }

            public override A FoldLeft<A>(Func<A, T, A> binOp, A initial) {
                return binOp(binOp(initial, Item1), Item2);
            }

            public override FTNode<T, V> Reverse(Func<T, T> f) {
                return new Node2(f(Item2), f(Item1), Measure);
            }

            internal override T[] ToArray() {
                return new[] {Item1, Item2};
            }

            ///<summary>
            ///Returns an enumerator that iterates through the node.
            ///</summary>
            ///
            ///<returns>
            ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the node.
            ///</returns>
            ///<filterpriority>1</filterpriority>
            public override IEnumerator<T> GetEnumerator() {
                yield return Item1;
                yield return Item2;
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <returns>
            /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
            /// </returns>
            /// <param name="other">An object to compare with this object.</param>
            public bool Equals(Node2 other) {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(other.Item1, Item1) && Equals(other.Item2, Item2);
            }

            /// <summary>
            /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
            /// </summary>
            /// <returns>
            /// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
            /// </returns>
            /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
            /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception><filterpriority>2</filterpriority>
            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof (Node2)) return false;
                return Equals((Node2) obj);
            }

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object" />.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            public override int GetHashCode() {
                unchecked {
                    return (Item1.GetHashCode() * 397) ^ Item2.GetHashCode();
                }
            }

            public static bool operator ==(Node2 left, Node2 right) {
                return Equals(left, right);
            }

            public static bool operator !=(Node2 left, Node2 right) {
                return !Equals(left, right);
            }
        }

        /// <summary>
        /// A node with three subtrees.
        /// </summary>
        [DebuggerDisplay("Node3({Item1}, {Item2}, {Item3})")]
        internal class Node3 : FTNode<T, V>, IEquatable<Node3> {
            public readonly T Item1;
            public readonly T Item2;
            public readonly T Item3;

            public Node3(T item1, T item2, T item3, Monoid<V> monoid) :
                this(
                item1, item2, item3,
                monoid.Plus(monoid.Plus(item1.Measure, item2.Measure), item3.Measure)) {}

            internal Node3(T item1, T item2, T item3, V measure) {
                Item1 = item1;
                Item2 = item2;
                Item3 = item3;
                Measure = measure;
            }

            public override A FoldRight<A>(Func<T, A, A> binOp, A initial) {
                return binOp(Item1, binOp(Item2, binOp(Item3, initial)));
            }

            public override A FoldLeft<A>(Func<A, T, A> binOp, A initial) {
                return binOp(binOp(binOp(initial, Item1), Item2), Item3);
            }

            public override FTNode<T, V> Reverse(Func<T, T> f) {
                return new Node3(f(Item3), f(Item2), f(Item1), Measure);
            }

            internal override T[] ToArray() {
                return new[] {Item1, Item2, Item3};
            }

            ///<summary>
            ///Returns an enumerator that iterates through the node.
            ///</summary>
            ///
            ///<returns>
            ///A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the node.
            ///</returns>
            ///<filterpriority>1</filterpriority>
            public override IEnumerator<T> GetEnumerator() {
                yield return Item1;
                yield return Item2;
                yield return Item3;
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <returns>
            /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
            /// </returns>
            /// <param name="other">An object to compare with this object.</param>
            public bool Equals(Node3 other) {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(other.Item1, Item1) && Equals(other.Item2, Item2) &&
                       Equals(other.Item3, Item3);
            }

            /// <summary>
            /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
            /// </summary>
            /// <returns>
            /// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
            /// </returns>
            /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
            /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception><filterpriority>2</filterpriority>
            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof (Node3)) return false;
                return Equals((Node3) obj);
            }

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object" />.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            public override int GetHashCode() {
                unchecked {
                    int result = Item1.GetHashCode();
                    result = (result * 397) ^ Item2.GetHashCode();
                    result = (result * 397) ^ Item3.GetHashCode();
                    return result;
                }
            }

            public static bool operator ==(Node3 left, Node3 right) {
                return Equals(left, right);
            }

            public static bool operator !=(Node3 left, Node3 right) {
                return !Equals(left, right);
            }
        }
        }
}