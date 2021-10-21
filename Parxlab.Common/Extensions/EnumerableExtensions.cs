using System;
using System.Collections.Generic;
using System.Linq;

namespace Parxlab.Common.Extensions
{
    public static class EnumerableExtensions
    {
        private static readonly Random random = new();

        public static T SelectRandom<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException();
            }

            if (!sequence.Any())
            {
                throw new ArgumentException("The sequence is empty.");
            }

            //optimization for ICollection<T>
                return sequence.ElementAt(random.Next(sequence.Count()));
        }
    }
}