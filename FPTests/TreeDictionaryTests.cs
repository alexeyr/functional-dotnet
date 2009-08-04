/*
* TreeDictionaryTests.cs is part of functional-dotnet project
* 
* Copyright (c) 2009 Alexey Romanov
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
using FP.Collections.Persistent;
using FP.Core;
using Microsoft.Pex.Framework;

namespace FPTests {
    [PexClass(typeof(TreeDictionary<,,>))]
    public partial class TreeDictionaryTests {
        private TreeDictionary<TKey, TValue, DefaultComparer<TKey>> CreateDictionary<TKey, TValue>(
            TKey[] keys, TValue[] values) where TKey : struct {
            PexAssume.IsNotNull(keys);
            PexAssume.IsNotNull(values);
            PexAssume.AreEqual(keys.Length, values.Length);
            PexAssume.AreDistinctValues(keys);
            return TreeDictionary.Empty<TKey, TValue>().AddAll(keys.Zip(values));
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int), typeof(object) })]
        public void Test_Lookup<TValue>(int[] keys, TValue[] values) {
            var dict = CreateDictionary(keys, values);
            for (int i = 0; i < keys.Length; i++) {
                PexAssert.AreEqual(values[i], dict[keys[i]]);
            }
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int), typeof(object) })]
        public void Test_Enumeration<TValue>(int[] keys, TValue[] values) {
            var dict = CreateDictionary(keys, values);
            Array.Sort(keys, values);
            int i = 0;
            foreach (var pair in dict) {
                PexAssert.AreEqual(pair.Item1, keys[i]);
                PexAssert.AreEqual(pair.Item2, values[i]);
                i++;
            }
        }
    }
}