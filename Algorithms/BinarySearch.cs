using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class BinarySearch {
        public static int Find<T>(IList<T> sortedSeq, T elem) where T : IComparable<T>, IEquatable<T> {
            if (sortedSeq.Count == 0) {
                return -1;
            }

            int low = 0, high = sortedSeq.Count - 1;
            while (low < high) {
                int mid = (low + high)/2;

                if (elem.CompareTo(sortedSeq[mid]) <= 0) {
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
