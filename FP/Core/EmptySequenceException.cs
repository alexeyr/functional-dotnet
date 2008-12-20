/*
* EmptySequenceException.cs is part of functional-dotnet project
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

namespace FP.Core {
    /// <summary>
    /// Thrown by the methods which require their argument to be non-empty
    /// when called on an empty sequence.
    /// </summary>
    public class EmptySequenceException : ArgumentException {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptySequenceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public EmptySequenceException(string message, Exception innerException)
            : base(message, innerException) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptySequenceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public EmptySequenceException(string message) : base(message) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptySequenceException"/> class.
        /// </summary>
        public EmptySequenceException() {}
    }
}