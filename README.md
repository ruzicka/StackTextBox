# StackTextBox
StackTextBox is simple Unity 3D component implementing stack of text messages.
## Installation
Download the project and copy content of the Assets directory to your Unity project's Assets directory.
## How to use
```c#
	//Get StackTextBox instance of the prefab droped in the scene
	StackTextBox msgBox = GameObject.Find("StackTextBox")
		.GetComponent<StackTextBox>();
		
	// add some messages	
	msgBox.AddText("Hello, world!");
	msgBox.AddText("By, world!");

	// AddText method returns StackText instance
	StackText msg = msgBox.AddText("Hello, world!");
	...
	// it can be used later to hide message. Hiding message effectively 
	// removes it from the stack.
	msg.Hide();
```


