using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class MergeSort {
        class SourceWrapper<T> {
            private IList<T> _list;
            private int _current, _end;

            public SourceWrapper(IList<T> list, int begin, int end) {
                _list = list;
                _current = begin;
                _end = end;
            }

            public bool HasMore {
                get {
                    return _current < _end;
                }
            }

            public T Peak {
                get { return _list[_current]; }
            }

            public T GetPeak() {
                return _list[_current++];
            }
        }

        internal static void Merge<T>(IList<T> seq, int begin, int end, IList<T> buffer, Sorting dir) where T : IComparable<T> {
            int middle = (begin + end)/2;
            SourceWrapper<T> left = new SourceWrapper<T>(seq, begin, middle), right = new SourceWrapper<T>(seq, middle, end);
            var swapPredicate = SequenceUtils.GetSwapPrdicate<T>(dir);
            buffer.Clear();
            while (left.HasMore || right.HasMore) {
                if (!left.HasMore || right.HasMore && swapPredicate(left.Peak, right.Peak)) {
                    buffer.Add(right.GetPeak());
                } else {
                    buffer.Add(left.GetPeak());
                }
            }

            for (var idx = begin; idx < end; idx++) {
                seq[idx] = buffer[idx - begin];
            }
        }

        private static void SortTopDown<T>(IList<T> seq, int begin, int end, IList<T> buffer, Sorting dir) where T : IComparable<T> {
            if (end - begin <= 1) {
                return;
            }

            if (end - begin == 2) {
                var swapPredicate = SequenceUtils.GetSwapPrdicate<T>(dir);
                if (swapPredicate(seq[begin], seq[end - 1])) {
                    var tmp = seq[begin];
                    seq[begin] = seq[end - 1];
                    seq[end - 1] = tmp;
                }
            }

            var middle = (begin + end)/2;
            SortTopDown(seq, begin, middle, buffer, dir);
            SortTopDown(seq, middle, end, buffer, dir);
            Merge(seq, begin, end, buffer, dir);
        }

        public static void SortTopDown<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            SortTopDown(seq, 0, seq.Count, new List<T>(seq.Count), dir);
        }

        public static void SortBottomUp<T>(IList<T> seq, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            /*if (seq.Count <= 1) {
                return;
            }

            var buffer = new List<T>(seq.Count);
            for (var width = 1; width <= seq.Count; width *= 2) {
                for (var part = 0; part < seq.Count/width; part++) {
                    int begin = part*width, end = (part + 1)*width;
                    Merge(seq, begin, end, buffer, dir);
                }
            }*/
        }
    }
}
