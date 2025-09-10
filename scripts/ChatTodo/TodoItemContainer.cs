using Godot;
using System;
using System.Globalization;

public partial class TodoItemContainer : Container
{
	[Export] public CheckBox todoToggle;
	[Export] public RichTextLabel todoTextLabel;
    [Export] public Color todoDisabledTextColor;

    public string userName;
    public string todo;
    public bool isDone = false;

	

	public void UpdateText(string userDisplayName, string todoText)
	{
		if (todoTextLabel == null)
			return;

		userName = userDisplayName;
        todo = todoText;

        todoTextLabel.Text = "[b]"+ userName + ":[/b]  " + todoText; 
	}

	public void SetDone()
	{
		if (todoToggle == null)
			return;

		isDone = true;
		todoToggle.ButtonPressed = true;
        todoTextLabel.AddThemeColorOverride("default_color", todoDisabledTextColor);
    }
}
