using Godot;
using System;

 [Tool]
public partial class ChatBubble : MarginContainer
{
	[Export] public String ChatterName
    {
		set
		{
			_chatterName = value;
			SetChatterName(value);
		}
		get => _chatterName;
    }
	[Export] public String ChatterMessage
	{
		set
		{
			_chatterMessage = value;
			SetMessageText(value);
		}
		get => _chatterMessage;
	}

	[Export] RichTextLabel ChatterNameLabel;
	[Export] RichTextLabel ChatterMessageLabel;

	private string _chatterName;
	private string _chatterMessage;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetBubble(ChatterName, ChatterMessage);
	}

	public void SetBubble(String name, String message)
	{
		SetMessageText(message);
		SetChatterName(name);
	}
	public void SetMessageText(String message)
	{
		if (ChatterMessageLabel == null) return;

		ChatterMessageLabel.Text = message;
	}

	public void SetChatterName(String name)
	{
		if (ChatterNameLabel == null) return;

		ChatterNameLabel.Text = name;
	}
}
