Setup your Tilemap Gameobject
-------


Create a new tilemap in your scene
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324082944.png)

Change GameObject Layer
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324083023.png)

Change Sorting Layer
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324083313.png)

Add the Collider to the tilemap that the player will walk on
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324083408.png)

You can tweak the appearance of your tilemap by changing the color property.  Clicking on the box will open the color picker. 
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324084355.png)
You can also use the eyedropper on the very right to pick a color directly from the scene.
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324084519.png)



Paint your Tilemap
----------
1. open the tile pallette window ![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%2020220324084120.png)
2. Select one of the two tilemap painting tools 
	1.![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324085024.png) the Brush tool **(B)** 
	2. ![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324085003.png)the Box Brush Tool **(U)**
		1. *The letter in parens is the shortcut*
1. choose a tile by clicking on in the pallette
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324091714.png)
	1.*you can select a group of tiles by dragging*
	![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324091605.png)
2. select the tilemap you want to paint on
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324091448.png)
3. paint on the tilemap ![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324091856.png)
5. Here are some other helpful tricks for working with tilemaps
6. The picker tool (I) ![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324085210.png]) allows you to copy a large chunk of your tilemap (Click and drag). *Alternative: when painting with the brush or box tool **hold CTRL** and drag the mouse*
7. The eraser tool (D) ![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324085548.png) erases.  I know it's mindblowing.   *Alternative: when painting with the brush or box tool **hold SHIFT** and drag the mouse*
8. You can also rotate and mirror your brush![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%%20image2020220324085743.png)




Add art and gameplay to your Level
========
The Level Kit folder contains other prefabs for building your level
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324090640.png)

Add artistic details to your scene from the prefabs set dressing folder
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324090543.png)

*NOTE: you can use the prefab Basic Ground Tilemap in the Level Kit folder to skip the setup steps at the beginning. But I recommend making it yourself first. Then add that prefab to the scene to see if you set yours up correctly*



Additional Level Setup
---------

Be sure to add a Player Virtual Camera to your level so the camera will follow the player! 
![](https://raw.githubusercontent.com/mharris382/Software-Engineering-Capstone/main/Docs/Images/Pasted%20image%2020220324091204.png)
These are setup to drag and drop into the scene.  The one with the boundary has additional details about how to setup the boundary, but I recommend using the one that doesn't have a boundary
