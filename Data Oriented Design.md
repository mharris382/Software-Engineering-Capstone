# Programming Paradigm #
Dependency Inversion Principle
=====
An additional important facet of our software design paradigm is how we will implement the dependency inversion principle using Unity.  Essentially the purpose of the dependency inversion principle is to prevent direct dependencies between unstable systems and instead reroute those dependencies through a stable third party object.  

By stable and unstable we are refering to how likely it is  that the code in that file will be changed.  Stable classes are very unlikely or impossible to change.  Unstable classes are highly volitle and likely to undergo many changes or be removed entirely.  Game development relies on lots of experimentation and fast iterations.  This means that much of the code will be extremely unstable, especially in the early stages of a project.  Building dependencies on unstable code makes a codebase inherintly unscalable. As the codebase grows, it will become harder and harder to debug and maintain.  This problem has to be addressed premptively, and the best way I've found to do it is through dependency inversion.   The solution is to build systems which only rely on stable code, and communicate indirectly through that stable code.  

This is the primary reason we are using the data container objects.  A data container does not have any behavior inherently.  This makes the code signifcantly easier to understand and change, which makes it much more stable than code with lots of behavior.   By separating the data and the behaviour we can invert the dependencies between different behaviors through the shared data objects.  A good example of that can be seen in the character movement system, which has an [input controller][3], [physics controller][5] and an [animation controller][1].  These behaviors are dependent on each other, but the way they work may be changed for any number of reasons.  Since each controller is unstable, we don't want to create any dependencies between them. Instead they all share a dependency to the [character's state][2] which only holds data.  All these systems use this state without knowning about the other's existence. 


***This has the added benefit of making all the system highly reusable.***  The input controller can easily be exchanged for an AI controller which could reference the exact same character state.  None of the other sytems would be effected.

Benefits
----
- reduces unstable dependencies
- makes complex codebase easier to understand
- helps keep systems contained, modular and reusable

Data Oriented Design
=====
the methodology outlined above is heavily influenced by Data Oriented Design paradigm

[Powerpoint Presentation On Data Oriented Design](https://docs.google.com/presentation/d/1v4FO5Jq1GCgPMp6PLHjXuxqYhPgrl4XOf5EZaQhquO8/edit#slide=id.gac7dedd9b1_0_91)

**Separate Data and Behaviour**

**Data as contracts (schema) between team members & modules**


[Example Project](https://github.com/polartron/dataorienteddesign/tree/master/Assets/Scripts)

~~Behaviour <- Behaviour~~
**Behavior <- Data -> Behavior**
Behaviours communicate with each other through the data.

One behaviour can write to the data, while another uses that data.  Neither is aware of the other behaviour's existence.  

For example say we want to implement character movement for the player.  To approach this in a Data Oriented way we would have an Input behavior and an input data object.

Naming Conventions
----
- data container classes or structs will end with the name **State** or **Container**
	- the suffix **State** will be used when the object stores a collection of data pertaining to a specific system
	- the suffix **Container** will be used when the object stores a single item of data and may be accessed  by many other systems.  All containers will inherit from a special unity type called [ScriptableObject](https://docs.unity3d.com/Manual/class-ScriptableObject.html)
- data container which store events (observables) will use the suffix **Event** or **Events** 
- data packets will use the suffix **Info**. These objects may be passed around from various systems in order to process specific complex cross-system behaviors (such as damage) will be implemented as structs rather than classes (structs are *pass by value* rather than *pass by reference*) and 

| suffix | purpose | base type |
|-------|----|-------|
| State | multi item data container | MonoBehaviour or System.Object|
| Container| single item data container | ScriptableObject |
| Event | event data container | System.Object or UnityEvent |
| Info | data packets | struct |

Examples
------

[Movement Example](https://github.com/polartron/dataorienteddesign/blob/master/Assets/Scripts/8/Movement.cs)

```cs
public struct InputState
{
	public Vector2 MoveInput;
	public bool Jump;
}

```

```cs
public class InputBehaviour
{
	InputState data;
	
	void Update()
	{
		var moveX = Input.GetAxis("Horizontal");
		var moveY = Input.GetAxis("Vertical");
		data.MoveInput = new Vector2(moveX, moveY);
		
		data.Jump = Input.GetButton("Jump");
	}
}
```

```cs
public class MoveState
{
	public Vector2 velocity;
	public bool Grounded;	
}
```

```cs

public class MoveBehaviour
{
	InputState inputState;
	MoveState moveState;
	readonly MoveConfig moveConfig;
	
	public void Update()
	{
		var velocity = moveState.velocity;
		velocity.x = inputState.MoveInput.normalized * moveConfig.moveSpeed;
		
		if(moveState.Grounded)
		{
			velocity.y =  0;
		}
		else
		{
			velocity.y = 9.8f;
		}
	}
}

```

```cs
[Serializable]
public class MoveConfig
{
	public float moveSpeed = 10;
	public float jumpForce = 10;
	public float gravity = 9.8f;
}
```




[1]:https://github.com/mharris382/Software-Engineering-Capstone/blob/main/Elementals/Assets/Scripts/Character%20Movement/CharacterAnimator.cs
[2]:https://github.com/mharris382/Software-Engineering-Capstone/blob/main/Elementals/Assets/Scripts/Character%20Movement/CharacterState.cs
[3]:https://github.com/mharris382/Software-Engineering-Capstone/blob/main/Elementals/Assets/Scripts/Character%20Movement/MovementInputHandler.cs
[4]:https://github.com/mharris382/Software-Engineering-Capstone/blob/main/Elementals/Assets/Scripts/Character%20Movement/MovementState.cs
[5]:https://github.com/mharris382/Software-Engineering-Capstone/blob/main/Elementals/Assets/Scripts/Character%20Movement/CharacterMove.cs