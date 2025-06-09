using Godot;
using System;
using System.Globalization;

public partial class TodoItemContainer : VBoxContainer
{
	[Export] public CheckBox todoToggle;
	[Export] public Label todoTextLabel;

	public string userName;
	public bool isDone = false;

	public void SetUserName(string userDisplayName) 
	{
		if (todoToggle == null)
			return;

		todoToggle.Text = userDisplayName;
		userName = userDisplayName;
	}

	public void SetTodo(string todoText) 
	{
		if (todoTextLabel == null)
			return;
		
		todoTextLabel.Text = todoText;
	}

	public void SetDone()
	{
		if (todoToggle == null)
			return;

		isDone = true;
		todoToggle.ButtonPressed = true;
	}
}
