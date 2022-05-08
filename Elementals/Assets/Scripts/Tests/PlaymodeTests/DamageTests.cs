using System.Collections;
using System.Collections.Generic;
using Damage;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DamageTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void HealthIsAliveWhenItHasHealth()
    {
        // Use the Assert class to test conditions
        var health = CreateTestComponent<HealthState>("");

        health.CurrentValue = 1;
        Assert.IsTrue(health.isAlive);
    }
    
    [Test]
    public void HealthIsDeadWhenItDoesntHaveHealth()
    {
        // Use the Assert class to test conditions
        var health = CreateTestComponent<HealthState>("");

        health.CurrentValue = 0;
        Assert.IsFalse(health.isAlive);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DamageTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
    
    T CreateTestComponent<T>(string name) where T:Component{
        var go = new GameObject(name);
        return go.AddComponent<T>();
    }

    private const float ineffectiveMultiplier = 0;
    private const float effectiveMultiplier = 2;
    private const float selfMultiplier = 0.5f;


    public void TestDamage(Element attackerElement, Element defenderElement, float expectedMultiplier)
    {
        var multiplier = ElementMatrix.getDamageModifier(defenderElement, attackerElement);
        Assert.AreEqual(expectedMultiplier, multiplier);
    }

    [Test]
    public void DamageIsHalvedAgainstSelf()
    {
        TestDamage(Element.Air, Element.Air, selfMultiplier);
        TestDamage(Element.Earth, Element.Earth, selfMultiplier);
        TestDamage(Element.Fire, Element.Fire, selfMultiplier);
        TestDamage(Element.Water, Element.Water, selfMultiplier);
        TestDamage(Element.Thunder, Element.Thunder, selfMultiplier);
    }
    [Test]
    public void WaterIsEffectiveAgainstFire()
    {
        TestDamage(Element.Water, Element.Fire, effectiveMultiplier);
    }
    [Test]
    public void FireInIneffectiveAgainstWater()
    {
        TestDamage(Element.Fire, Element.Water, ineffectiveMultiplier);
    }
    [Test]
    public void FireIsEffectiveAgainstAir()
    {
        TestDamage(Element.Fire, Element.Air, effectiveMultiplier);
    }
    [Test]
    public void AirIsIneffectiveAgainstFire()
    {
        TestDamage(Element.Air, Element.Fire, ineffectiveMultiplier);
    }
    [Test]
    public void AirIsEffectiveAgainstEarth()
    {
        TestDamage(Element.Air, Element.Earth, effectiveMultiplier);
    }
    [Test]
    public void EarthIsIneffectiveAgainstAir()
    {
        TestDamage(Element.Earth, Element.Air, ineffectiveMultiplier);
    }
    [Test]
    public void ThunderIsIneffectiveAgainstEarth()
    {
        TestDamage(Element.Thunder, Element.Earth, ineffectiveMultiplier);
    }
    [Test]
    public void EarthIsEffectiveAgainstThunder()
    {
        TestDamage(Element.Earth, Element.Thunder, effectiveMultiplier);
    }
    [Test]
    public void ThunderIsEffectiveAgainstWater()
    {
        TestDamage(Element.Thunder, Element.Water, effectiveMultiplier);
    }
    [Test]
    public void WaterIsIneffectiveAgainstThunder()
    {
        TestDamage(Element.Water, Element.Thunder, ineffectiveMultiplier);
    }
    
    
}