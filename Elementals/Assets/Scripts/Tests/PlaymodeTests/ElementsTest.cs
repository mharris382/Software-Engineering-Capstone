using System;
using System.Collections;
using Elements;
using Elements.Totem;
using Elements.Totem.UI;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlaymodeTests
{
    
    public class ElementsTest
    {
        [Test]
        public void TestElementConfigExists()
        {
            Assert.IsNotNull(ElementConfig.Instance);
        }


        public IEnumerator TestTotemUIInput()
        {
            var dummyInput = new DummyTotemInput();
            bool wasChanged =false; 
            var start = Element.Fire;
            var next = Element.Water;
            var prev = Element.Air;
            var player = CreateElementContainer(start);
            var ui = CreateElementContainer(start);
            var controller = CreateController(player, ui);
            controller.InputHandler = dummyInput;
            float startTime = Time.time;
            
            
            float timeoutTime = startTime + 10;
            int frameCount = 0;
            
            while (frameCount < 6 )
            {
                switch (frameCount)
                {
                    case 0:
                        dummyInput.TestInputStream.OnNext(1);
                        break;
                    case 1:
                        Assert.AreEqual(next, ui.Element, $"Didn't change element to next: {next} from prev: {start}");
                        //expect element to be next
                        break;
                    case 2:
                        dummyInput.TestInputStream.OnNext(-1);
                        break;    
                    case 3:
                        Assert.AreEqual(start, ui.Element, $"Didn't change element to prev: {start} from prev: {next}");
                        break;
                    case 4:
                        dummyInput.TestInputStream.OnNext(-1);
                        break;
                    case 5:
                        Assert.AreEqual(prev, ui.Element, $"Didn't change element to prev: {prev} from prev: {start}");
                        dummyInput.TestInputStream.OnCompleted();
                        wasChanged = true;
                        break;
                }   
                frameCount++;
                yield return null;
            }
            Assert.IsFalse(Time.time >= timeoutTime,"timeout occured");
        }

        private Element PredictNext(Element e) => (Element) (((int) e) % 5);
        private Element PredictPrevious(Element e) => ((int)e == 0 ) ? ((Element)4) : (Element) ((int) e + 1);

        private ElementContainer CreateElementContainer(Element initialElement)
        {
            var container = ScriptableObject.CreateInstance<ElementContainer>();
            container.Element = initialElement;
            return container;
        }
        
        private UITotemController CreateController(ElementContainer playerContainer, ElementContainer uiContainer)
        {
            var go = new GameObject("UI Controller");
            var controller = go.AddComponent<UITotemController>();
            controller.playerElement = playerContainer;
            controller.uiSelectedElement = uiContainer;
            
            return controller;
        }
        
        public class DummyTotemInput : ITotemInputHandler
        {

            public Subject<int> TestInputStream = new Subject<int>();
            public int testInputValue;
            public IObservable<int> CreateInputAxisCycleElements()
            {
                return TestInputStream;
            }

            public int GetElementSelectionInputAxis()
            {
                if (testInputValue != 0)
                {
                    var input = testInputValue;
                    testInputValue = 0;
                    return input;
                }
                return testInputValue;
            }
        }


        [UnityTest]
        public IEnumerator TestTotemRechargeTimer()
        {
            var config = new TotemConfig() {
                rechargeDuration = 1,
                totemRadius = 1
            };
            var totemState = new TotemStateData(config);

            
            totemState.PlayerInRange = false;
            Assert.AreEqual(TotemStates.ReadyToUse, totemState.CurrentState);                  //totem should start in the ready state
            totemState.ConsumeTotem();                                                                       //consuming the totem should start the recharge timer
            Assert.AreEqual(TotemStates.Charging, totemState.CurrentState);                     //totem should not be ready until recharge duration has passed
            yield return new WaitForSeconds(config.rechargeDuration + 0.1f);                                 //wait the recharge duration (plus a little extra for buffer room)
            Assert.AreEqual(TotemStates.ReadyToUse, totemState.CurrentState);                  //totem should now be ready to use again
        }

        [Test]
        public void TotemStartsFullyCharged()
        {
            var totemState = new TotemStateData(new TotemConfig() {
                rechargeDuration = 1,
                totemRadius = 1
            });
            Assert.IsFalse(totemState.IsCharging);
        }

        [Test]
        public void TotemInUseWhenInRangeAndCharged()
        {
            var totemState = new TotemStateData(new TotemConfig() {
                rechargeDuration = 1,
                totemRadius = 1
            });


            totemState.IsCharging = false;
            totemState.PlayerInRange = true;
            Assert.AreEqual(TotemStates.InUse, totemState.CurrentState);
        }

        [Test]
        public void TotemNotInUseWhenCharging()
        {
            var totemState = new TotemStateData(new TotemConfig() {
                rechargeDuration = 1,
                totemRadius = 1
            });
            
            
            totemState.IsCharging = true;
            totemState.PlayerInRange = true;
            Assert.AreEqual(TotemStates.Charging, totemState.CurrentState);
        }


        [Test]
        public void TotemReadyWhenNotChargingAndNotInRange()
        {
            var totemState = new TotemStateData(new TotemConfig() {
                rechargeDuration = 1,
                totemRadius = 1
            });
            
            totemState.IsCharging = false;
            totemState.PlayerInRange = false;
            Assert.AreEqual(TotemStates.ReadyToUse, totemState.CurrentState);
        }
    }
}