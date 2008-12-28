using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FP.Validation {
    /// <summary>
    /// Represents validation of preconditions.
    /// </summary>
    /// <remarks>Cannot be constructed directly; call <see cref="Requires.That"/> to
    /// begin validation and extension methods from <see cref="ValidationExtensions"/> to
    /// check conditions.</remarks>
    /// <seealso cref="ValidationExtensions"/>
    public class Validation {
        private readonly List<Exception> _exceptions;

        internal Exception[] Exceptions {
            get { return _exceptions.ToArray(); }
        }

        internal Validation AddExceptionInternal(Exception ex) {
            lock (_exceptions) {
                _exceptions.Add(ex);
            }

            return this;
        }

        internal Validation() {
            _exceptions = new List<Exception>(1); // optimize for only having 1 exception
        }

        ~Validation() {
            throw new ValidationException("validation.Check() was not called!", Exceptions);
        }

        [DebuggerNonUserCode]
        internal virtual void Throw() {
            Exception exception = _exceptions.Count == 1
                          ? _exceptions[0]
                          : new MultiException(Exceptions);
            throw new ValidationException(exception);
        }
    }
}