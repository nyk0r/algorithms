using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class BinarySearch {
        public static int Find<T>(IList<T> sortedSeq, T elem, Sorting soring = Sorting.Asc) where T : IComparable<T>, IEquatable<T> {
            if (sortedSeq.Count == 0) {
                return -1;
            }

            var comparer = SequenceUtils.GetComparer<T>(soring);

            int low = 0, high = sortedSeq.Count - 1;
            while (low < high) {
                int mid = (low + high)/2;

                if (comparer(elem, sortedSeq[mid])) {
                    high = mid;
                }
                else {
                    low = mid + 1;
                }
            }

            return sortedSeq[high].Equals(elem) ? high : -1;
        }
    }
}
