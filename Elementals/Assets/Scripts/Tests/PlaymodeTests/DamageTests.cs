using System.Collections;
using System.Collections.Generic;
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
}