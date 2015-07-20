using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class QuickSort {
        private const int SIMPLE_SORT_THRESHOLD = 15;

        internal static int Partition2<T>(IList<T> seq, int begin, int end, Func<T, T, bool> areOutOfOrder) {
            if (begin >= end) {
                throw new ArgumentOutOfRangeException();
            }

            int lo = begin, hi = end;
            T partitioner = seq[begin];
            while (lo < hi) {
                for (; lo < end && !areOutOfOrder(seq[lo], partitioner); ++lo) ;
                for (; hi > begin && !areOutOfOrder(partitioner, seq[hi]); --hi) ;
                if (lo < hi) {
                    SequenceUtils.Swap(seq, lo, hi);
                }
            }
            SequenceUtils.Swap(seq, begin, hi);
            return hi;
        }

        private static void Sort2<T>(IList<T> seq, int begin, int end, Sorting dir, IComparer<T> comparer, Func<T, T, bool> areOutOfOrder) {
            if (end - begin <= SIMPLE_SORT_THRESHOLD) {
                ElementarySorts.Insertion(seq, dir, comparer);
                return;
            }
            var pivot = Partition2(seq, begin, end, areOutOfOrder);
            Sort2(seq, begin, pivot - 1, dir, comparer, areOutOfOrder);
            Sort2(seq, pivot + 1, end, dir, comparer, areOutOfOrder);
        }

        public static void Sort2<T>(IList<T> seq, Sorting dir, IComparer<T> comparer) {
            SequenceUtils.Shuffle(seq);
            Sort2(seq, 0, seq.Count - 1, dir, comparer, SequenceUtils.GetOutOfOrderPrdicate(dir, comparer));
        }

        public static void Sort2<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T:IComparable<T> {
            Sort2(seq, dir, Comparer<T>.Default);
        }
    }
}
