/*
* Cont.cs is part of functional-dotnet project
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

namespace FP.Core.CPS {
    ///<summary>
    /// A continuation.
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public class Cont<T> {
        public readonly Action<T> Ok;
        public readonly Action<Exception> Fail;
        public Cont(Action<T> ok, Action<Exception> fail) {
            Ok = ok;
            Fail = fail;
        }
    }
}
