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

        internal static Tuple<int, int> Partition3<T>(IList<T> seq, int begin, int end, Func<T, T, bool> areOutOfOrder) {
            if (begin >= end) {
                throw new ArgumentOutOfRangeException();
            }

            int lo = begin, idx = begin, hi = end;
            T partitioner = seq[begin];
            while (idx <= hi) {
                if (areOutOfOrder(partitioner, seq[idx])) {
                    SequenceUtils.Swap(seq, lo, idx);
                    ++lo;
                    ++idx;
                } else if (areOutOfOrder(seq[idx], partitioner)) {
                    SequenceUtils.Swap(seq, hi, idx);
                    --hi;
                } else {
                    ++idx;
                }
            }

            return Tuple.Create(lo, hi);
        }

        private static void Sort3<T>(IList<T> seq, int begin, int end, Sorting dir, IComparer<T> comparer, Func<T, T, bool> areOutOfOrder) {
            if (end - begin <= SIMPLE_SORT_THRESHOLD) {
                ElementarySorts.Insertion(seq, dir, comparer);
                return;
            }
            var pivots = Partition3(seq, begin, end, areOutOfOrder);
            Sort3(seq, begin, pivots.Item1 - 1, dir, comparer, areOutOfOrder);
            Sort3(seq, pivots.Item2 + 1, end, dir, comparer, areOutOfOrder);
        }

        public static void Sort3<T>(IList<T> seq, Sorting dir, IComparer<T> comparer) {
            SequenceUtils.Shuffle(seq);
            Sort3(seq, 0, seq.Count - 1, dir, comparer, SequenceUtils.GetOutOfOrderPrdicate(dir, comparer));
        }

        public static void Sort3<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            Sort3(seq, dir, Comparer<T>.Default);
        }
    }
}
