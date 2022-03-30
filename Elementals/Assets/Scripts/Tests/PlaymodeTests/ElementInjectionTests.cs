using Damage;
using Elements;
using NUnit.Framework;
using UnityEngine;

public class ElementInjectionTests
{
    [Test]
    public void ElementInjectedIsCorrect()
    {
        var health = new GameObject("Health").AddComponent<HealthState>();
        health.element = Element.Air;
        Assert.AreEqual(Element.Air, health.element);
        
        var dropOnDeath = health.gameObject.AddComponent<DropManaOnDeath>();
        dropOnDeath.Element = Element.Fire;
        Assert.AreEqual(Element.Fire, dropOnDeath.Element);
        
        
        var root = new GameObject("Parent");
        health.transform.SetParent(root.transform);
        
        var injector = root.AddComponent<ElementInjector>();
        var element = Element.Water;
        injector.element = element;
        injector.InjectElementToDependents();
        
        Assert.AreEqual(element, health.element, "Element not injected to HealthState!");
        Assert.AreEqual(element, dropOnDeath.Element, "Element not injected to DropManaOnDeath!");
    }
}