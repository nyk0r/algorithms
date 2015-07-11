using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class SequenceUtils {
        public static Func<T, T, bool> GetComparer<T>(Ordering ordering) where T : IComparable<T> {
            return GetComparer(ordering, Comparer<T>.Default);
        }

        public static Func<T, T, bool> GetComparer<T>(Ordering ordering, IComparer<T> comparer) {
            switch (ordering) {
                case Ordering.Gt:
                    return (a, b) => comparer.Compare(a, b) > 0;
                case Ordering.GtOrEq:
                    return (a, b) => comparer.Compare(a, b) >= 0;
                case Ordering.Lt:
                    return (a, b) => comparer.Compare(a, b) < 0;
                case Ordering.LtOrEq:
                    return (a, b) => comparer.Compare(a, b) <= 0;
                default:
                    throw new ArgumentOutOfRangeException("ordering");
            }
        }

        public static Func<T, T, bool> GetComparer<T>(Sorting sorting) where T : IComparable<T> {
            return GetComparer(sorting, Comparer<T>.Default);
        }

        public static Func<T, T, bool> GetComparer<T>(Sorting sorting, IComparer<T> comparer) {
            switch (sorting) {
                case Sorting.Asc:
                    return GetComparer(Ordering.LtOrEq, comparer);
                case Sorting.Desc:
                    return GetComparer(Ordering.GtOrEq, comparer);
                default:
                    throw new ArgumentOutOfRangeException("sorting");
            }
        }

        public static Func<T, T, bool> GetOutOfOrderPrdicate<T>(Sorting dir) where T : IComparable<T> {
            return GetOutOfOrderPrdicate(dir, Comparer<T>.Default);
        }

        public static Func<T, T, bool> GetOutOfOrderPrdicate<T>(Sorting dir, IComparer<T> comparer) {
            return
                dir == Sorting.Asc
                    ? (Func<T, T, bool>)((a, b) => comparer.Compare(a, b) > 0)
                    : (a, b) => comparer.Compare(a, b) < 0;
        }

        public static bool CheckNeighbours<T>(IEnumerable<T> seq, Func<T, T, bool> predicate) {
            var enu = seq.GetEnumerator();
            try {
                if (!enu.MoveNext()) {
                    return true;
                }
                var a = enu.Current;
                if (!enu.MoveNext()) {
                    return true;
                }
                do {
                    var b = enu.Current;
                    if (!predicate(a, b)) {
                        return false;
                    }
                    a = b;
                } while (enu.MoveNext());

                return true;
            }
            finally {
                enu.Dispose();
            }
        }

        public static bool IsOrdered<T>(IEnumerable<T> seq, Ordering ordering) where T : IComparable<T> {
            return IsOrdered(seq, ordering, Comparer<T>.Default);
        }

        public static bool IsOrdered<T>(IEnumerable<T> seq, Ordering ordering, IComparer<T> comparer) {
            var comparison = GetComparer(ordering, comparer);
            return CheckNeighbours(seq, (a, b) => comparison(a, b));
        }
    }
}
