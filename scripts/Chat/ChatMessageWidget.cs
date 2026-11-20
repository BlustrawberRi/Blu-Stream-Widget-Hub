using Godot;
using Godot.Collections;
using SB.Events;
using System;
using System.Collections.Generic;

public partial class ChatMessageWidget : StreamerWidget
{
	[Signal] public delegate void ChatMessageReceivedEventHandler(string login, string chatMessage);

	//public override List<EventType> StreamerBotEventRequests { get; set; } = new() { Twitch.ChatMessage };


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StreamerBotEventRequests.Add(Twitch.ChatMessage) ;
		base._Ready();
    }


    public override void OnEventDataReceived(EventType type, Dictionary data)
	{
		if (type != Twitch.ChatMessage)
			return;

		TwitchChatMessage chatMessage = new(data);
		EmitSignal(SignalName.ChatMessageReceived, chatMessage.User.Login, chatMessage.Text);
		GD.Print(chatMessage.Text);
    }

}
