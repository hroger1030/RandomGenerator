using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RandomNumbers
{
    public static class CollectionRandomizer
    {
        private static Random _Random = new Random();

        /// <summary>
        /// Fast randomizer that sorts collection in place. by swaping
        /// each item with a randomitem that has not been swapped yet.
        /// </summary>
        public static IList<T> ShuffleList<T>(this IList<T> list)
        {
            int n = list.Count;
            T value;

            while (n > 1)
            {
                n--;
                int k = _Random.Next(n + 1);
                value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        /// <summary>
        /// Shuffle Algorithim using crypto RNG for true randomness
        /// </summary>
        public static IList<T> CryptoShuffleList<T>(this IList<T> list)
        {
            var provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            T value;

            while (n > 1)
            {
                byte[] box = new byte[1];

                do provider.GetBytes(box);
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
