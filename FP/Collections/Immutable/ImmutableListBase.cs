#region License
/*
* ImmutableListBase.cs is part of functional-dotnet project
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
#endregion

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FP.Core;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A base class for immutable lists. It's not necessary for other implementations to inherit from it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ImmutableListBase<T> : IImmutableList<T> {
        /// <summary>
        /// Gets the "head" (first element) of the list.
        /// </summary>
        /// <value>The head of the list.</value>
        /// <exception cref="EmptySequenceException">is the current list <see cref="IsEmpty"/>.</exception>
        public abstract T Head { get; }
        /// <summary>
        /// Gets the "tail" (all elements but the first) of the list.
        /// </summary>
        /// <value>The tail of the list.</value>
        /// <exception cref="EmptySequenceException">is the current list <see cref="IsEmpty"/>.</exception>
        public abstract IImmutableList<T> Tail { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public abstract bool IsEmpty { get; }
        /// <summary>
        /// Prepends a new head.
        /// </summary>
        /// <param name="newHead">The new head.</param>
        /// <returns>
        /// The list with <paramref name="newHead"/> as <see cref="Head"/>
        /// and the current list as <see cref="Tail"/>.
        /// </returns>
        public abstract IImmutableList<T> Prepend(T newHead);

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
            IImmutableList<T> list = this;
            while (list != null && !list.IsEmpty) {
                yield return list.Head;
                list = list.Tail;
            }
        } // GetEnumerator()

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <value></value>
        public T this[int index] {
            get { return this.ElementAt(index); }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ImmutableListBase<T> list1, ImmutableListBase<T> list2) {
            return list1 == null
                ? list2 == null
                : list2 != null && list1.SequenceEqual(list2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ImmutableListBase<T> list1, ImmutableListBase<T> list2) {
            return !(list1 == list2);
        }

        protected bool Equals(ImmutableListBase<T> list) {
            return list != null && this.SequenceEqual(list);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
        public override bool Equals(object obj) {
            return ReferenceEquals(this, obj) || Equals(obj as ImmutableListBase<T>);
        }

        /// <summary>
        /// Serves as a hash function for this type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode() {
            return IsEmpty 
                ? typeof(T).GetHashCode()
                : Head.GetHashCode() + 29*Tail.GetHashCode();
        }
    } // class ImmutableListBase
} // namespace FP.Collections.Immutable
