using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class BinarySearch {
        public static int Find<T>(IList<T> sortedSeq, T elem, Sorting soring = Sorting.Asc) where T : IComparable<T>, IEquatable<T> {
            return Find(sortedSeq, elem, soring, Comparer<T>.Default);
        }

        public static int Find<T>(IList<T> sortedSeq, T elem, Sorting soring, IComparer<T> comparer) {
            if (sortedSeq.Count == 0) {
                return -1;
            }

            var comparison = SequenceUtils.GetComparer(soring, comparer);

            int low = 0, high = sortedSeq.Count - 1;
            while (low < high) {
                var mid = low + (high - low)/2;

                if (comparison(elem, sortedSeq[mid])) {
                    high = mid;
                } else {
                    low = mid + 1;
                }
            }

            return sortedSeq[high].Equals(elem) ? high : -1;
        }
    }
}
