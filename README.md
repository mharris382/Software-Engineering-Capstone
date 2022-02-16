Software Engineering Capstone
=====

Software Installation and unity project setup

Unity
-----

1. [Download Unity Hub](https://unity3d.com/get-unity/download)
2. [Create Unity Account](https://id.unity.com/en/conversations/7b1f1c15-625f-42bc-a038-c8547f9cb809018f) you can sign in with Google
3. Open the Unity Hub app
4. Navigate to the **Installs** tab on the Left 
5. Click on the **Install Editor** button on the top right corner of the screen
6. **Install Unity version 2020.3.28f1 (LTS)** which is the reccomended version
7. On the next screen you can use the default install settings (if you want to build for web install the WebGL build support, but you can easily do this step later)

Source Control
----

 1. make sure you are logged into Github in your browser. If you don't have a github account [create one GitHub Account now](https://github.com/signup?ref_cta=Sign+up&ref_loc=header+logged+out&ref_page=%2F&source=header-home) 
 2. [Create a GitKraken Account](https://app.gitkraken.com/login), you can use your Google account or prefereably your GitHub account to sign i
 3. [Download GitKraken GUI Installer](https://www.gitkraken.com/download/windows64) and run the installer 
 7. Open the GitKraken application
 8. Sign into your GitKraken Account preferably through your GitHub Account (if you did use your GitHub Account, skip to step 13)
 9. If you did not sign into GitKraken using GitHub, you need to link GitKraken to GitHub now.  
 10. In the GitKraken GUI menu bar navigate to File -> Preferences
 11. From the Preferences Screen, Navigate to the Integrations tab.
 12. Ensure is says your GitHub account is connected
 13. In the GitKraken GUI menu bar navigate to File -> Clone Repo
 14. Paste this Repository link directly into the URL box
 15. **Make sure to choose the path where you want the project to be located. *You will need this path later to initialize the Unity Project***
 16. press the **Clone the Repo!** button and wait for the repo to finish downloading, then press **open now**
 
IDE
--------

1. [create or login to your Jetbrains account](https://account.jetbrains.com/login)
2. [Apply for Jetbrains Student License](https://www.jetbrains.com/shop/eform/students) if you were already approved for the GitHub Education Pack, you can use the GitHub tab to quickly get approved for the jetbrains student license
3. [Download the Rider Installer](https://www.jetbrains.com/rider/?_ga=2.6180787.1420589103.1644964244-1235206129.1644705957) and run the installer
4. After downloading and installing rider, simply run it and follow the on-screen prompts to sign in with your JetBrains Account to activate the student license

Project Setup
-----
Complete the previous setups before this one

1. After completing the previous installations, open Unity Hub and navigate to the **projects tab** on the lefthand side of the window
2. From here click on the **Open** button at the top right
3. Navigate to the path where you cloned the repository.  There should be a subfolder here named **Elementals**, open this folder.  You should now see a number of folders one of them named **Assets**.  The assets folder is very important, so remember this path.
4. Press the Open button now, and wait while Unity sets up the project.  This may take a few minutes.
5. Once Unity finishes importing, the Unity Editor will open.  
6. From the Unity Editor navigate to **Edit -> Preferences** from the menu bar at the top of the screen
7. From the Preferences window, navigate to the **External Tools** tab on the left side of the preferences window
8. Next to **External Script Editor** click the dropdown menu and select **Rider** from the options
9. Now you are all ready to go!

Java to C# Guide
====
included pdf cheatsheet in repo 

Collection Types
| Type       | Java               | C#                       |
|------------|--------------------|--------------------------|
| Array      | `int[]`            | `int[]`                  |
| ArrayList  | `ArrayList<int>`   | `List<int>`              |
| LinkedList | `LinkedList<int>`  | `LinkedList<int>`        |
| Dictionary | `Map<string, int>` | `Dictionary<string,int>` |
| Set        | `HashSet<string>`  | `HashMap<string>`        |

Coding Conventions
====
The coding convention for unity projects is to follow [Microsoft's C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).  There are a few Unity specific tweaks to this.  Private fields marked with the attribute `[SerializeField]` should have pascal case without the leading underscore. 

Unity Serialization
----
Unity is a data driven engine. So when programming with unity it is important to understand the difference between Serialized and Non-Serialized.  
The process of serialization is converting data between unity and C#.  This is used to allow the creation instances of a class and modify the class's serialized values from the editor.  This can be used to create controls to customize functionality or tweak values to modify behaviors.  This can also be used to manually assign dependencies from the Unity Editor.  **Note: This can be useful at times, but it can cause to null reference errors if you forget to assign the dependency in the editor.**  For that reason I like to use this technique for assigning optional dependencies which alter functionality, and resolve all mandatory dependencies from code.  The following types can be serialized by unity.

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

a complete list of serializable types can be found here 

By default Unity serialized all public fields and does not serialize private fields.


There are a few things that are not universally agreed on with c#.  One is whether or not to explicitly add the `private` keyword to private methods and fields since it is optional.

For example these c# statements are equivalent
|Member Type| implicit                            | explicit                                    |
|-----------|-------------------------------------|---------------------------------------------|
|field| `float _currentHealth;`             | `private float _currentHealth;`             |
|serialized field| `[SerializeField] float maxHealth;` | `[SerializeField] private float maxHealth;` |
|method| `void Awake()` | `private void Awake()` |
|property| `float CurrentHealth { get; set; }`    | `private float CurrentHealth { get; set; }`    |


Project Organization Structure
====
We will need to devise an organization strategy that works best for us, however there are some standard conventions that we should use.  Additionally it is important to be aware that [unity has special folders](https://docs.unity3d.com/Manual/SpecialFolders.html) which we will make use of.





