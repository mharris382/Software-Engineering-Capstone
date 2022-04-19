using Elements;
using NUnit.Framework;

namespace Tests.PlaymodeTests
{
    [TestFixture]
    public class ElementSlotTests
    {
        [Test]
        public void TestCanAssignToEmptySlots()
        {
            int expectedEmptySlots = 3;
            IElementSlots slots = new Slots(expectedEmptySlots);
            Assert.AreEqual(expectedEmptySlots, slots.TotalSlots);
            Assert.AreEqual(expectedEmptySlots, slots.EmptySlots);
            Assert.AreEqual(0, slots.AssignedSlots);
            int expectedNextIndex = 0;
            Element addElement = Element.Fire;
            int actualIndex = slots.TryAssignNewElement(addElement);
            Assert.AreEqual(expectedNextIndex, actualIndex);
            bool addedSuccesfully = slots.TryGetElementAtIndex(expectedNextIndex, out var foundElement);
            Assert.IsTrue(addedSuccesfully);
            Assert.AreEqual(addElement, foundElement);
        }

        [Test]
        public void CanAssignMultipleSlots()
        {
            int expectedEmptySlots = 3;
            IElementSlots slots = new Slots(expectedEmptySlots);
            Element addFirstElement = Element.Air;
            Element addSecondElement = Element.Earth;
            Element addThirdElement = Element.Fire;

            slots.TryAssignNewElement(addFirstElement);
            slots.TryAssignNewElement(addSecondElement);
            slots.TryAssignNewElement(addThirdElement);
            
            
            Assert.IsTrue(slots.TryGetElementAtIndex(0, out var actualFirst));
            Assert.IsTrue(slots.TryGetElementAtIndex(1, out var actualSecond));
            Assert.IsTrue(slots.TryGetElementAtIndex(2, out var actualThird));
            Assert.AreEqual(addFirstElement, actualFirst);
            Assert.AreEqual(addSecondElement, actualSecond);
            Assert.AreEqual(addThirdElement, actualThird);
            
        }
    }
}