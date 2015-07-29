using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Algorithms {
    public enum DHeapType { Max, Min }

    public class DHeap<T> : IEnumerable<T> {
        private Func<T, T, bool> _comparer;
        private IList<T> _content;
        private int _rank;

        public DHeap(IEnumerable<T> source, IComparer<T> comparer, DHeapType type, int rank = 2) {
            switch (type) {
                case DHeapType.Max:
                    _comparer = (a, b) => comparer.Compare(a, b) >= 0;
                    break;
                case DHeapType.Min:
                    _comparer = (a, b) => comparer.Compare(a, b) <= 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            _content = new List<T>(source);
            _rank = rank;
            for (var node = _content.Count/_rank; node >= 0; --node) {
                Sink(node);
            }
        }

        public DHeap(IEnumerable<T> source, DHeapType type, int rank = 2) : this(source, Comparer<T>.Default, type) { }

        public DHeap(DHeapType type, int rank = 2) : this(new T[] { }, type) { }

        public void Add(T item) {
            _content.Add(item);
            Swim(_content.Count - 1);
        }

        public T GetPeak() {
            if (_content.Count == 0) {
                throw new InvalidOperationException("The heap is empty.");
            }

            var result = _content[0];
            _content[0] = _content[_content.Count - 1];
            _content.RemoveAt(_content.Count - 1);
            Sink(0);
            return result;
        }

        public T Peak { get { return _content[0]; } }

        public int Count { get { return _content.Count; }}

        private void Swim(int node) {
            for (var parent = (node - 1)/_rank;
                parent >= 0 && !_comparer(_content[parent], _content[node]);
                node = parent, parent = (node - 1)/_rank) {
                Swap(parent, node);
            }
        }

        private void Sink(int node) {
            for (var maxChild = MaxChild(node);
                maxChild != -1 && !_comparer(_content[node], _content[maxChild]);
                node = maxChild, maxChild = MaxChild(node)) {
                Swap(node, maxChild);
            }
        }

        private int MaxChild(int node) {
            var result = -1;
            for (var child = node*_rank + 1; child < _content.Count && child <= node*_rank + _rank; ++child) {
                if (result == -1) {
                    result = child;
                } else if (!_comparer(_content[result], _content[child])) {
                    result = child;
                }
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Swap(int node1, int node2) {
            var tmp = _content[node1];
            _content[node1] = _content[node2];
            _content[node2] = tmp;
        }

        public IEnumerator<T> GetEnumerator() {
            return _content.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
