using System;
using System.Collections.Generic;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    internal class QuickSortTest : SortTest {
        private class PartitioningResult {
            public double[] Sequnece { get; private set; }
            public Sorting Sorting { get; private set; }

            public PartitioningResult(double[] sequnece, Sorting sorting) {
                Sequnece = sequnece;
                Sorting = sorting;
            }
        }

        private sealed class Partitioning2Result : PartitioningResult {
            public int Point { get; private set; }

            public Partitioning2Result(int point, double[] sequnece, Sorting sorting)
                : base(sequnece, sorting) {
                Point = point;
            }
        }

        private sealed class Partitioning3Result : PartitioningResult {
            public Tuple<int, int> Points { get; private set; }

            public Partitioning3Result(Tuple<int, int> point, double[] sequnece, Sorting sorting)
                : base(sequnece, sorting) {
                Points = point;
            }
        }

        [Test]
        public void TestPartition2() {
            var fixtures = new Dictionary<double[], Partitioning2Result[]> {
                {
                    new [] { 1.0, 1.0 }, 
                    new [] 
                    { 
                        new Partitioning2Result(0, new [] { 1.0, 1.0 }, Sorting.Asc),
                        new Partitioning2Result(0, new [] { 1.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 1.0, 2.0 }, 
                    new [] {
                        new Partitioning2Result(0, new [] { 1.0, 2.0 }, Sorting.Asc),
                        new Partitioning2Result(1, new [] { 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 2.0, 1.0 }, 
                    new [] {
                        new Partitioning2Result(1, new [] { 1.0, 2.0 }, Sorting.Asc),
                        new Partitioning2Result(0, new [] { 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 0.0, 0.0, 0.0 }, 
                    new [] {
                        new Partitioning2Result(0, new [] { 0.0, 0.0, 0.0 }, Sorting.Asc),
                        new Partitioning2Result(0, new [] { 0.0, 0.0, 0.0 }, Sorting.Desc)
                    }
                },
                { 
                    new [] { 1.0, 2.0, 3.0 }, 
                    new [] {
                        new Partitioning2Result(0, new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new Partitioning2Result(2, new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    } 
                }, 
                {
                    new [] { 1.0, 3.0, 2.0 }, 
                    new [] {
                        new Partitioning2Result(0, new [] { 1.0, 3.0, 2.0 }, Sorting.Asc),
                        new Partitioning2Result(2, new [] { 2.0, 3.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 2.0, 1.0, 3.0 }, 
                    new [] {
                        new Partitioning2Result(1, new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new Partitioning2Result(1, new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    }
                },  
                {
                    new [] { 3.0, 2.0, 1.0 }, 
                    new [] {
                        new Partitioning2Result(2, new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new Partitioning2Result(0, new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 3.0, 1.0, 2.0 }, 
                    new[] {
                        new Partitioning2Result(2, new [] { 2.0, 1.0, 3.0 }, Sorting.Asc),
                        new Partitioning2Result(0, new [] { 3.0, 1.0, 2.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 7.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, 
                    new [] {
                        new Partitioning2Result(6, new [] { 0.0, 3.0, 2.0, 1.0, 5.0, 1.0, 7.0, 9.0, 8.0 }, Sorting.Asc),
                        new Partitioning2Result(2, new [] { 8.0, 9.0, 7.0, 1.0, 2.0, 3.0, 0.0, 1.0, 5.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, 
                    new [] {
                        new Partitioning2Result(0, new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, Sorting.Asc),
                        new Partitioning2Result(8, new [] { 5.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 0.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, 
                    new [] {
                        new Partitioning2Result(8, new [] { 5.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 9.0 }, Sorting.Asc),
                        new Partitioning2Result(0, new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 7.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, 
                    new [] {
                        new Partitioning2Result(7, new [] { 1.0, 3.0, 2.0, 1.0, 4.0, 5.0, 0.0, 7.0, 9.0, 8.0 }, Sorting.Asc),
                        new Partitioning2Result(2, new [] { 8.0, 9.0, 7.0, 1.0, 2.0, 3.0, 0.0, 1.0, 5.0, 4.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, 
                    new [] {
                        new Partitioning2Result(0, new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, Sorting.Asc),
                        new Partitioning2Result(9, new [] { 4.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 0.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, 
                    new [] {
                        new Partitioning2Result(9, new [] { 4.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 9.0 }, Sorting.Asc),
                        new Partitioning2Result(0, new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, Sorting.Desc)
                    }
                },
            };

            Assert.Throws<ArgumentOutOfRangeException>(
                () => QuickSort.Partition2(new double[] {}, 0,  -1, SequenceUtils.GetOutOfOrderPrdicate<double>(Sorting.Asc)));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => QuickSort.Partition2(new double[] { 0 }, 0, 0, SequenceUtils.GetOutOfOrderPrdicate<double>(Sorting.Asc)));

            foreach (var fixture in fixtures) {
                foreach (var result in fixture.Value) {
                    var arr = (double[])fixture.Key.Clone();
                    var point = QuickSort.Partition2(arr, 0, arr.Length - 1, SequenceUtils.GetOutOfOrderPrdicate<double>(result.Sorting));
                    Assert.AreEqual(result.Point, point);
                    CollectionAssert.AreEqual(result.Sequnece, arr);
                }
            }
        }

        [Test]
        public void TestSort2Asc() {
            TestSortAsc(QuickSort.Sort2);
        }

        [Test]
        public void TestSort2Desc() {
            TestSortDesc(QuickSort.Sort2);
        }

        [Test]
        public void TestPartition3() {
            var fixtures = new Dictionary<double[], Partitioning3Result[]> {
                {
                    new [] { 1.0, 1.0 }, 
                    new [] 
                    { 
                        new Partitioning3Result(Tuple.Create(0, 1), new [] { 1.0, 1.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(0, 1), new [] { 1.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 1.0, 2.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(0, 0), new [] { 1.0, 2.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(1, 1), new [] { 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 2.0, 1.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(1, 1), new [] { 1.0, 2.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(0, 0), new [] { 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 0.0, 0.0, 0.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(0, 2), new [] { 0.0, 0.0, 0.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(0, 2), new [] { 0.0, 0.0, 0.0 }, Sorting.Desc)
                    }
                },
                { 
                    new [] { 1.0, 2.0, 3.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(0, 0), new [] { 1.0, 3.0, 2.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(2, 2), new [] { 2.0, 3.0, 1.0 }, Sorting.Desc)
                    } 
                }, 
                {
                    new [] { 1.0, 3.0, 2.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(0, 0), new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(2, 2), new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 2.0, 1.0, 3.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(1, 1), new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(1, 1), new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 3.0, 2.0, 1.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(2, 2), new [] { 2.0, 1.0, 3.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(0, 0), new [] { 3.0, 1.0, 2.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 3.0, 1.0, 2.0 }, 
                    new[] {
                        new Partitioning3Result(Tuple.Create(2, 2), new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(0, 0), new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 7.0, 3.0, 2.0, 7.0, 8.0, 9.0, 0.0, 9.0, 5.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(4, 5), new [] { 3.0, 2.0, 5.0, 0.0, 7.0, 7.0, 9.0, 9.0, 8.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(3, 4), new [] { 9.0, 9.0, 8.0, 7.0, 7.0, 0.0, 2.0, 5.0, 3.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(0, 1), new [] { 0.0, 0.0, 1.0, 8.0, 9.0, 2.0, 1.0, 5.0, 3.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(7, 8), new [] { 3.0, 2.0, 1.0, 8.0, 9.0, 1.0, 5.0, 0.0, 0.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(7, 8), new [] { 3.0, 2.0, 1.0, 8.0, 0.0, 1.0, 5.0, 9.0, 9.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(0, 1), new [] { 9.0, 9.0, 1.0, 8.0, 2.0, 0.0, 1.0, 5.0, 3.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 7.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 7.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(6, 7), new [] { 3.0, 2.0, 1.0, 5.0, 0.0, 1.0, 7.0, 7.0, 9.0, 8.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(2, 3), new [] { 9.0, 8.0, 7.0, 7.0, 1.0, 0.0, 1.0, 5.0, 2.0, 3.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 0.0, 9.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(0, 2), new [] {  0.0, 0.0, 0.0, 8.0, 9.0, 1.0, 1.0, 2.0, 9.0, 3.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(7, 9), new [] { 3.0, 2.0, 1.0, 8.0, 9.0, 1.0, 9.0, 0.0, 0.0, 0.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, 
                    new [] {
                        new Partitioning3Result(Tuple.Create(8, 9), new [] { 3.0, 2.0, 1.0, 8.0, 0.0, 1.0, 5.0, 4.0, 9.0, 9.0 }, Sorting.Asc),
                        new Partitioning3Result(Tuple.Create(0, 1), new [] { 9.0, 9.0, 1.0, 8.0, 2.0, 0.0, 1.0, 5.0, 4.0, 3.0 }, Sorting.Desc)
                    }
                },
            };

            Assert.Throws<ArgumentOutOfRangeException>(
                () => QuickSort.Partition3(new double[] { }, 0, -1, SequenceUtils.GetOutOfOrderPrdicate<double>(Sorting.Asc)));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => QuickSort.Partition3(new double[] { 0 }, 0, 0, SequenceUtils.GetOutOfOrderPrdicate<double>(Sorting.Asc)));

            foreach (var fixture in fixtures) {
                foreach (var result in fixture.Value) {
                    var arr = (double[]) fixture.Key.Clone();
                    var points = QuickSort.Partition3(arr, 0, arr.Length - 1,
                        SequenceUtils.GetOutOfOrderPrdicate<double>(result.Sorting));
                    Assert.AreEqual(result.Points, points);
                    CollectionAssert.AreEqual(result.Sequnece, arr);
                }
            }
        }

        [Test]
        public void TestSort3Asc() {
            TestSortAsc(QuickSort.Sort3);
        }

        [Test]
        public void TestSort3Desc() {
            TestSortDesc(QuickSort.Sort3);
        }
    }
}