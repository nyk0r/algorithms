using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class SequenceUtils {
        public static Func<T, T, bool> GetComparer<T>(Ordering ordering) where T : IComparable<T> {
            switch (ordering) {
                case Ordering.Gt:
                    return (a, b) => a.CompareTo(b) > 0;
                case Ordering.GtOrEq:
                    return (a, b) => a.CompareTo(b) >= 0;
                case Ordering.Lt:
                    return (a, b) => a.CompareTo(b) < 0;
                case Ordering.LtOrEq:
                    return (a, b) => a.CompareTo(b) <= 0;
                default:
                    throw new ArgumentOutOfRangeException("ordering");
            }
        }

        public static Func<T, T, bool> GetComparer<T>(Sorting sorting) where T : IComparable<T> {
            switch (sorting) {
                case Sorting.Asc:
                    return GetComparer<T>(Ordering.LtOrEq);
                case Sorting.Desc:
                    return GetComparer<T>(Ordering.GtOrEq);
                default:
                    throw new ArgumentOutOfRangeException("sorting");
            }
        }

        public static Func<T, T, bool> GetSwapPrdicate<T>(Sorting dir) where T : IComparable<T> {
            return
                dir == Sorting.Asc
                    ? (Func<T, T, bool>) ((a, b) => a.CompareTo(b) > 0)
                    : (a, b) => a.CompareTo(b) < 0;
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
            var comparer = GetComparer<T>(ordering);
            return CheckNeighbours(seq, (a, b) => comparer(a, b));
        } 
    }
}
