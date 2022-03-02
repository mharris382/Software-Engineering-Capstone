Project Management
------
[Codecks](https://elementals.codecks.io/decks)

[Discord](https://discord.gg/TrZ2W2FU)


Software Engineering Capstone
=====

[Software Installation and unity project setup](https://github.com/mharris382/Software-Engineering-Capstone/blob/main/Docs/Installation%20and%20Setup.md)




Coding Conventions
====
The coding convention for unity projects is to follow [Microsoft's C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).  There are a few Unity specific tweaks to this.  Private fields marked with the attribute `[SerializeField]` should have pascal case without the leading underscore. 

Unity Serialization
----
Unity is a data driven engine. So when programming with unity it is important to understand the difference between Serialized and Non-Serialized.  
The process of serialization is converting data between unity and C#.  This is used to allow the creation instances of a class and modify the class's serialized values from the editor.  This can be used to create controls to customize functionality or tweak values to modify behaviors.  This can also be used to manually assign dependencies from the Unity Editor.  **Note: This can be useful at times, but it can cause to null reference errors if you forget to assign the dependency in the editor.**  For that reason I like to use this technique for assigning optional dependencies which alter functionality, and resolve all mandatory dependencies from code.  The following types can be serialized by unity.

All serialized fields on Unity types will show up in the Inspector inside the Unity Editor.  


|Type |Example                | Default Value|
|-----|-----------------------|-----|
|bool    |`public bool canSprint = true;` |`false`|
|int     |`public int maximumMana = 100;`  | `0`|
|float   |`public float moveSpeed = 10;`| `0.0f`|
|string  | `public string spellID = "Water Bolt";`| `""`|
|enum types | `public Element startElement = Element.Water;`| First Declared Enum |
|Unity Types| `public GameObject playerPrefab;` | `null`|
|Array of any types above| `public string[] spells;`| `new string[0]`|
|List of any types above| `public List<GameObject> enemyPrefabs;`|`new List<GameObject>()`|

[a complete guide to Unity's serializable types can be found here](https://docs.unity3d.com/Manual/script-Serialization.html)

By default Unity serialized all public fields and does not serialize private fields.


There are a few things that are not universally agreed on with c#.  One is whether or not to explicitly add the `private` keyword to private methods and fields since it is optional.

For example these c# statements are equivalent

| Member Type | implicit | explicit    |
|-----------|-----------|----------------|
| field | `float _currentHealth;`             | `private float _currentHealth;`             |
| serialized field | `[SerializeField] float maxHealth;` | `[SerializeField] private float maxHealth;` |
| method | `void Awake()` | `private void Awake()` |
| property | `float CurrentHealth { get; set; }`    | `private float CurrentHealth { get; set; }`    |


Project Organization Structure
====
We will need to devise an organization strategy that works best for us, however there are some standard conventions that we should use.  Additionally it is important to be aware that [unity has special folders](https://docs.unity3d.com/Manual/SpecialFolders.html) which we will make use of.

```
Assets
|
└───Art
|	
└───Art

|	|
|	|
```
Software Package Diagram
----
![Package Diagram](https://github.com/mharris382/Software-Engineering-Capstone/blob/main/Docs/Images/Package%20Diagram.png)

[Java to C# Guide](https://github.com/mharris382/Software-Engineering-Capstone/blob/main/Docs/CSharp%20for%20Java%20Developers%20-%20Cheat%20Sheet.pdf)
-----

Java to C# Collection Types

| Type       | Java               | C#                       |
|------------|--------------------|--------------------------|
| Array      | `int[]`            | `int[]`                  |
| ArrayList  | `ArrayList<int>`   | `List<int>`              |
| LinkedList | `LinkedList<int>`  | `LinkedList<int>`        |
| Dictionary | `Map<string, int>` | `Dictionary<string,int>` |
| Set        | `HashSet<string>`  | `HashMap<string>`        |