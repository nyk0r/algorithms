using System;
using System.Collections.Generic;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    internal class QuickSortTest : SortTest {
        private sealed class PartitioningResult {
            public int Point { get; private set; }
            public double[] Sequnece { get; private set; }
            public Sorting Sorting { get; private set; }

            public PartitioningResult(int point, double[] sequnece, Sorting sorting) {
                Point = point;
                Sequnece = sequnece;
                Sorting = sorting;
            }
        }

        [Test]
        public void TestPartition2() {
            var fixtures = new Dictionary<double[], PartitioningResult[]> {
                {
                    new [] { 1.0, 1.0 }, 
                    new [] 
                    { 
                        new PartitioningResult(0, new [] { 1.0, 1.0 }, Sorting.Asc),
                        new PartitioningResult(0, new [] { 1.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 1.0, 2.0 }, 
                    new [] {
                        new PartitioningResult(0, new [] { 1.0, 2.0 }, Sorting.Asc),
                        new PartitioningResult(1, new [] { 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 2.0, 1.0 }, 
                    new [] {
                        new PartitioningResult(1, new [] { 1.0, 2.0 }, Sorting.Asc),
                        new PartitioningResult(0, new [] { 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 0.0, 0.0, 0.0 }, 
                    new [] {
                        new PartitioningResult(0, new [] { 0.0, 0.0, 0.0 }, Sorting.Asc),
                        new PartitioningResult(0, new [] { 0.0, 0.0, 0.0 }, Sorting.Desc)
                    }
                },
                { 
                    new [] { 1.0, 2.0, 3.0 }, 
                    new [] {
                        new PartitioningResult(0, new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new PartitioningResult(2, new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    } 
                }, 
                {
                    new [] { 1.0, 3.0, 2.0 }, 
                    new [] {
                        new PartitioningResult(0, new [] { 1.0, 3.0, 2.0 }, Sorting.Asc),
                        new PartitioningResult(2, new [] { 2.0, 3.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 2.0, 1.0, 3.0 }, 
                    new [] {
                        new PartitioningResult(1, new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new PartitioningResult(1, new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 2.0, 1.0, 3.0 }, 
                    new [] {
                        new PartitioningResult(1, new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new PartitioningResult(1, new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 3.0, 2.0, 1.0 }, 
                    new [] {
                        new PartitioningResult(2, new [] { 1.0, 2.0, 3.0 }, Sorting.Asc),
                        new PartitioningResult(0, new [] { 3.0, 2.0, 1.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 3.0, 1.0, 2.0 }, 
                    new[] {
                        new PartitioningResult(2, new [] { 2.0, 1.0, 3.0 }, Sorting.Asc),
                        new PartitioningResult(0, new [] { 3.0, 1.0, 2.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 7.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, 
                    new [] {
                        new PartitioningResult(6, new [] { 0.0, 3.0, 2.0, 1.0, 5.0, 1.0, 7.0, 9.0, 8.0 }, Sorting.Asc),
                        new PartitioningResult(2, new [] { 8.0, 9.0, 7.0, 1.0, 2.0, 3.0, 0.0, 1.0, 5.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, 
                    new [] {
                        new PartitioningResult(0, new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, Sorting.Asc),
                        new PartitioningResult(8, new [] { 5.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 0.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, 
                    new [] {
                        new PartitioningResult(8, new [] { 5.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 9.0 }, Sorting.Asc),
                        new PartitioningResult(0, new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 7.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, 
                    new [] {
                        new PartitioningResult(7, new [] { 1.0, 3.0, 2.0, 1.0, 4.0, 5.0, 0.0, 7.0, 9.0, 8.0 }, Sorting.Asc),
                        new PartitioningResult(2, new [] { 8.0, 9.0, 7.0, 1.0, 2.0, 3.0, 0.0, 1.0, 5.0, 4.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, 
                    new [] {
                        new PartitioningResult(0, new [] { 0.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, Sorting.Asc),
                        new PartitioningResult(9, new [] { 4.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 0.0 }, Sorting.Desc)
                    }
                }, 
                {
                    new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, 
                    new [] {
                        new PartitioningResult(9, new [] { 4.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 9.0 }, Sorting.Asc),
                        new PartitioningResult(0, new [] { 9.0, 3.0, 2.0, 1.0, 8.0, 9.0, 0.0, 1.0, 5.0, 4.0 }, Sorting.Desc)
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
                    Assert.AreEqual(point, result.Point);
                    CollectionAssert.AreEqual(arr, result.Sequnece);
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
    }
}