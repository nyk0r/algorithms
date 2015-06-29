using System;
using System.Collections.Generic;

namespace Algorithms {
    public static class MergeSort {
        internal static IEnumerable<T> Merge<T>(IEnumerable<T> seq1, IEnumerable<T> seq2, Sorting dir = Sorting.Asc) where T : IComparable<T> {
            throw new NotImplementedException();

            var comparer = SequenceUtils.GetComparer<T>(dir);
            var result = new List<T>();
            IEnumerator<T> enum1 = seq1.GetEnumerator(), enum2 = seq2.GetEnumerator();
            try {
                while (true) {
                    bool moreSeq1 = enum1.MoveNext(), moreSeq2 = enum2.MoveNext();
                    if (moreSeq1 && !moreSeq2) {
                        
                    } else if (!moreSeq1 && moreSeq2) {

                    } else if (moreSeq1 && moreSeq2) {
                        
                    } else {
                        break;
                    }
                }
            } finally {
                enum1.Dispose();
                enum2.Dispose();
            }
        }

        public static void Sort<T>(IList<T> seq) where T : IComparable<T> {
            throw new NotImplementedException();
        }
    }
}
