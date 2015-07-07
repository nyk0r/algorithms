using Algorithms;
using NUnit.Framework;

namespace AlgorithmsTests {
    [TestFixture]
    internal class ElementarySortsTest : SortTest{
        [Test]
        public void TestSelectionAsc() {
            TestSortAsc(ElementarySorts.Selection);
        }

        [Test]
        public void TestSelectionDesc() {
            TestSortDesc(ElementarySorts.Selection);
        }

        [Test]
        public void TestInsertionAsc() {
            TestSortAsc(ElementarySorts.Insertion);
        }

        [Test]
        public void TestInsertionDesc() {
            TestSortDesc(ElementarySorts.Insertion);
        }

        [Test]
        public void TestShellAsc() {
            TestSortAsc(ElementarySorts.Shell);
        }

        [Test]
        public void TestShellDesc() {
            TestSortDesc(ElementarySorts.Shell);
        }
    }
}
