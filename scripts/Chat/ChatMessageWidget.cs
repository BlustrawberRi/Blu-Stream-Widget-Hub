using Godot;
using Godot.Collections;
using SB.Events;
using System;

public partial class ChatMessageWidget : StreamerWidget
{
    

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StreamerBotEventRequests.Add(Twitch.ChatMessage) ;
		base._Ready();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void OnEventDataReceived(EventType type, Dictionary data)
	{
		
    }

}
