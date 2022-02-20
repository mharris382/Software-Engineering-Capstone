# Programming Paradigm #
[Powerpoint Presentation On Data Oriented Design](https://docs.google.com/presentation/d/1v4FO5Jq1GCgPMp6PLHjXuxqYhPgrl4XOf5EZaQhquO8/edit#slide=id.gac7dedd9b1_0_91)

**Separate Data and Behaviour**

**Data as contracts (schema) between team members & modules**

[Example Project](https://github.com/polartron/dataorienteddesign/tree/master/Assets/Scripts)

~~Behaviour <- Behaviour~~
**Behavior <- Data -> Behavior**
Behaviours communicate with each other through the data.

One behaviour can write to the data, while another uses that data.  Neither is aware of the other behaviour's existence.  

For example say we want to implement character movement for the player.  To approach this in a Data Oriented way we would have an Input behavior and an input data object.


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

