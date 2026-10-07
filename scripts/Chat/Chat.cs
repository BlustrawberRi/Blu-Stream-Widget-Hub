using Godot;
using System;
using System.Collections.Generic;


public partial class Chat : VBoxContainer
{
	[Export] public PackedScene ChatBubbleReference;

	private List<ChatBubble> bubbles = new List<ChatBubble>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void CreateNewBubble(String chatterName, String message)
	{
		ChatBubble bubble = ChatBubbleReference.Instantiate<ChatBubble>();
		AddChild(bubble);
		
		bubbles.Add(bubble);
	}
}
