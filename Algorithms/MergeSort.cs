using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Algorithms {
    public static class MergeSort {
        private const int SIMPLE_SORT_THRESHOLD = 15;

        internal static void Merge<T>(IList<T> seq, int begin, int middle, int end, IList<T> buffer, Func<T, T, bool> areOutOfOrder) {
            for (var idx = begin; idx < end; idx++) {
                buffer[idx] = seq[idx];
            }

            int left = begin, right = middle;
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

        internal static void InsertionSort<T>(IList<T> source, IList<T> dest, int begin, int end, Func<T,T, bool> areOutOfOrder) {
            for (var i = begin; i < end; i++) {
                dest[i] = source[i];
                for (var j = i; j > begin && areOutOfOrder(dest[j - 1], dest[j]); --j) {
                    var tmp = dest[j];
                    dest[j] = dest[j - 1];
                    dest[j - 1] = tmp;
                } 
            }
        }

        private static void SortTopDown<T>(IList<T> source, IList<T> dest, int begin, int end, Func<T, T, bool> areOutOfOrder) {
            if (end - begin <= 1) {
                return;
            }
            if (end - begin <= SIMPLE_SORT_THRESHOLD) {
                InsertionSort(source, dest, begin, end, areOutOfOrder);
                return;
            }

            var middle = (begin + end)/2;
            SortTopDown(dest, source, begin, middle, areOutOfOrder);
            SortTopDown(dest, source, middle, end, areOutOfOrder);
            if (areOutOfOrder(source[middle - 1], source[middle])) {
                Merge(source, begin, middle, end, dest, areOutOfOrder);
            } else {
                SequenceUtils.Copy(source, dest, begin, end);
            }
        }

        public static void SortTopDown<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            SortTopDown(seq, dir, Comparer<T>.Default);
        }

        public static void SortTopDown<T>(IList<T> seq, Sorting dir, IComparer<T> comparer) {
            var buffer = new T[seq.Count];
            SequenceUtils.Copy(seq, buffer, 0, seq.Count);
            SortTopDown(buffer, seq, 0, seq.Count, SequenceUtils.GetOutOfOrderPrdicate(dir, comparer));
        }

        public static void SortBottomUp<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            SortBottomUp(seq, dir, Comparer<T>.Default);
        }

        public static void SortBottomUp<T>(IList<T> seq, Sorting dir, IComparer<T> comparer) {
            var buffer = new T[seq.Count];
            var areOutOfOrder = SequenceUtils.GetOutOfOrderPrdicate(dir, comparer);
            for (var width = 1; width <= seq.Count; width *= 2) {
                for (var begin = 0; begin < seq.Count - width; begin += 2*width) {
                    int middle = begin + width, end = Math.Min(begin + 2*width, seq.Count);
                    if (areOutOfOrder(seq[middle - 1], seq[middle])) {
                        Merge(seq, begin, middle, end, buffer, areOutOfOrder);
                        SequenceUtils.Copy(buffer, seq, begin, end);
                    }
                }
            }
        }
    }
}
