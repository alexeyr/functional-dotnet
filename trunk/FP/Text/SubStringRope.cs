/*
* SubStringRope.cs is part of functional-dotnet project
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
using System.Collections.Generic;
using System.IO;
using FP.Validation;

namespace FP.Text {
    /// <summary>
    /// A rope which is a substring of another rope.
    /// </summary>
    [Serializable]
    public sealed class SubStringRope : FlatRope {
        private readonly FlatRope _sourceRope;
        private readonly int _offset;
        private readonly int _length;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubStringRope"/> class.
        /// </summary>
        /// <param name="sourceRope">The char sequence.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        public SubStringRope(FlatRope sourceRope, int offset, int length) {
            Requires.That.
                IsIndexAndCountInRange(sourceRope, offset, length, "offset", "length").Check();

            _sourceRope = sourceRope;
            _offset = offset;
            _length = length;
        }

        public override IEnumerator<char> GetEnumerator(int startIndex) {
            Requires.That.IsIndexInRange(this, startIndex, "startIndex").Check();

            return _sourceRope.GetEnumerator(_offset + startIndex);
        }

        public override int Count {
            get { return _length; }
        }

        public override char this[int index] {
            get {
                Requires.That.IsIndexInRange(this, index, "startIndex").Check();

                return _sourceRope[_offset + index];
            }
        }

        public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) {
            Requires.That.IsIndexAndCountInRange(this, sourceIndex, count, "sourceIndex", "count").Check();

            _sourceRope.CopyTo(_offset + sourceIndex, destination, destinationIndex, count);
        }

        public override void WriteOut(TextWriter writer, int startIndex, int count) {
            _sourceRope.WriteOut(writer, _offset + startIndex, count);
        }

        public override Rope SubString(int startIndex, int count) {
            if (startIndex == 0 && count == Count)
                return this;

            Requires.That.
                IsIndexAndCountInRange(this, startIndex, count, "startIndex", "count").Check();

//            if (count <= MAX_SHORT_SIZE) {
//                var array = new char[count];
//                _charSequence.CopyTo(startIndex, array, 0, count);
//                return array.ToRope();
//            }
            return new SubStringRope(_sourceRope, _offset + startIndex, count);
        }
    }
}