/*
The MIT License (MIT)

Copyright (c) 2010 Roger Hill

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files 
(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, 
publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RandomNumbers
{
    public static class CollectionRandomizer
    {
        /// <summary>
        /// Fast randomizer that sorts collection in place. by swapping
        /// each item with a random item that has not been swapped yet.
        /// </summary>
        public static IList<T> ShuffleList<T>(this IList<T> list)
        {
            int n = list.Count;
            T value;

            while (n > 1)
            {
                n--;
                int k = Random.Shared.Next(n + 1);
                value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        /// <summary>
        /// Shuffle Algorithm using crypto RNG for true randomness
        /// </summary>
        public static IList<T> CryptoShuffleList<T>(this IList<T> list)
        {
            int n = list.Count;
            T value;

            while (n > 1)
            {
                var box = new byte[1];

                do RandomNumberGenerator.Fill(box);
                while (!(box[0] < n * (byte.MaxValue / n)));

                int k = (box[0] % n);
                n--;
                value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
    }
}
