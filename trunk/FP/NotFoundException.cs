using System;

namespace FP {
    internal class NotFoundException : SystemException {
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException) {}

        public NotFoundException(string message) : base(message) {}

        public NotFoundException() {}
    }
}