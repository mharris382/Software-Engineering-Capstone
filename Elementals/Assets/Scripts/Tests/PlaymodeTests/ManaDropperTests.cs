using System.Collections;
using ManaSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ManaDropperTests
{
    [UnityTest]
    public IEnumerator TestManaDropperNeverNull()
    {
        var dropper = new GameObject("ManaDropper", typeof(ManaDropper));
        yield return null;
        Assert.IsNotNull(ManaDropper.Instance);
        GameObject.Destroy(dropper);
        yield return null;
        Assert.IsNotNull(ManaDropper.Instance);
    }

    [Test]
    public void TestHasDropParent()
    {
        Assert.NotNull(ManaDropper.Instance);
        Assert.NotNull(ManaDropper.Instance.DropParent);
    }
    [UnityTest]
    public IEnumerator ManaDropperDropsCorrectElementAndAmount()
    {
        var dropper = ManaDropper.Instance;
        yield return null;
        var parent = dropper.DropParent;
        Assert.AreEqual(0, parent.childCount, "WTF?!");
         
        int expectedDropAmount = 10;
        Element expectElement = Element.Earth;
         
        var dropInfo = new ManaDropInfo() {DropAmount = expectedDropAmount};
        ManaDropper.DropMana(expectElement, Vector2.zero, dropInfo);
        yield return null;
        Assert.AreEqual(expectedDropAmount, parent.childCount, "Parent has incorrect number of children!");
        var mana = parent.GetComponentsInChildren<Mana>();
        Assert.AreEqual(expectedDropAmount, mana.Length, "Found incorrect number of mana objects!");
        foreach (var mana1 in mana) Assert.AreEqual(expectElement, mana1.element, "Mana object has incorrect element!");
    }
}