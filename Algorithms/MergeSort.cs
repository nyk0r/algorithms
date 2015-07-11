using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class MergeSort {
        internal static void Merge<T>(IList<T> seq, int begin, int middle, int end, IList<T> buffer, Sorting dir) where T : IComparable<T> {
            for (var idx = begin; idx < end; idx++) {
                buffer[idx] = seq[idx];
            }

            int left = begin, right = middle;
            var areOutOfOrder = SequenceUtils.GetOutOfOrderPrdicate<T>(dir);
            for (var idx = begin; idx < end; idx++) {
                if (left >= middle) {
                    // right part is exhausted take
                    buffer[idx] = seq[right++];
                } else if (right >= end) {
                    // right part is exhausted
                    buffer[idx] = seq[left++];
                } else if (areOutOfOrder(seq[left], seq[right])) {
                    buffer[idx] = seq[right++];
                } else {
                    buffer[idx] = seq[left++];
                }
            }
        }

        private static void SortTopDown<T>(IList<T> seq, int begin, int end, IList<T> buffer, Sorting dir) where T : IComparable<T> {
            if (end - begin <= 1) {
                return;
            }

            var middle = (begin + end)/2;
            var areOutOfOrder = SequenceUtils.GetOutOfOrderPrdicate<T>(dir);
            SortTopDown(seq, begin, middle, buffer, dir);
            SortTopDown(seq, middle, end, buffer, dir);
            if (areOutOfOrder(seq[middle - 1], seq[middle])) {
                Merge(seq, begin, middle, end, buffer, dir);
                for (var idx = begin; idx < end; idx++) {
                    seq[idx] = buffer[idx];
                }
            }
        }

        public static void SortTopDown<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            SortTopDown(seq, 0, seq.Count, new T[seq.Count], dir);
        }

        public static void SortBottomUp<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            var buffer = new T[seq.Count];
            var areOutOfOrder = SequenceUtils.GetOutOfOrderPrdicate<T>(dir);
            for (var width = 1; width <= seq.Count; width *= 2) {
                for (var begin = 0; begin < seq.Count - width; begin += 2*width) {
                    int middle = begin + width, end = Math.Min(begin + 2*width, seq.Count);
                    if (areOutOfOrder(seq[middle - 1], seq[middle])) {
                        Merge(seq, begin, middle, end, buffer, dir);
                        for (var idx = begin; idx < end; idx++) {
                            seq[idx] = buffer[idx];
                        }
                    }
                }
            }
        }
    }
}
