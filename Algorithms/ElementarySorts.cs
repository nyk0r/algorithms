using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class ElementarySorts {
        private static void Swap<T>(IList<T> seq, int a, int b) {
            var tmp = seq[a];
            seq[a] = seq[b];
            seq[b] = tmp;
        }

        public static void Selection<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            Selection(seq, dir, Comparer<T>.Default);
        }

        public static void Selection<T>(IList<T> seq, Sorting dir, IComparer<T> comparer) {
            var length = seq.Count;
            var areOutOfOrder = SequenceUtils.GetOutOfOrderPrdicate(dir, comparer);
            for (var i = 0; i < length - 1; i++) {
                var swapIdx = i;
                for (var j = i + 1; j < length; j++) {
                    if (areOutOfOrder(seq[swapIdx], seq[j])) {
                        swapIdx = j;
                    }
                }
                if (swapIdx != i) {
                    Swap(seq, i, swapIdx);
                }
            }
        }

        private static void PutSentinel<T>(IList<T> seq, Func<T, T, bool> areOutOfOrder) {
            for (var i = seq.Count - 1; i > 0; i--) {
                if (areOutOfOrder(seq[i - 1], seq[i])) {
                    Swap(seq, i - 1, i);
                }
            }
        }

        public static void Insertion<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            Insertion(seq, dir, Comparer<T>.Default);
        }

        public static void Insertion<T>(IList<T> seq, Sorting dir, IComparer<T> comparer) {
            var length = seq.Count;
            var movePredicate = SequenceUtils.GetOutOfOrderPrdicate(dir, comparer);

            // Sentinel is an optimization made to remove a (j > 0) check in inner loop.
            PutSentinel(seq, movePredicate);
            for (var i = 1; i < length; i++) {
                var j = i;
                var current = seq[i];
                while (movePredicate(seq[j - 1], current)) {
                    seq[j] = seq[j - 1];
                    --j;
                }
                seq[j] = current;
            }
        }

        private static IEnumerable<int> GetShellGaps(int n) {
            var tmp = new List<int>(10);
            for (int k = 0; ; k++) {
                var gap = (int)(Math.Pow(3, k) - 1)/2;
                if (gap > Math.Ceiling((double) n/3)) {
                    break;
                }
                tmp.Add(gap);
            }
            var result = new List<int>(tmp.Count);
            for (var i = tmp.Count - 1; i >= 0; i--) {
                result.Add(tmp[i]);
            }
            return result;
        } 

        public static void Shell<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            Shell(seq, dir, Comparer<T>.Default);
        }

        public static void Shell<T>(IList<T> seq, Sorting dir, IComparer<T> comparer) {
            var length = seq.Count;
            var movePredicate = SequenceUtils.GetOutOfOrderPrdicate(dir, comparer);

            foreach (var gap in GetShellGaps(length)) {
                for (var i = gap; i < length; i++) {
                    var j = i;
                    var current = seq[i];
                    while (j >= gap && movePredicate(seq[j - gap], current)) {
                        seq[j] = seq[j - gap];
                        j -= gap;
                    }
                    seq[j] = current;
                }
            }
        }
    }
}
