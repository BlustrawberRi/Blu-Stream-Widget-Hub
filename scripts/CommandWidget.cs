using Godot;
using Godot.Collections;
using SB.Events;
using System;
using System.Collections.Generic;

public partial class CommandWidget : StreamerWidget
{
	[Export] string commandName;
	[Signal] public delegate void CommandTriggeredEventHandler(string commandName, string commandText);
	public new List<EventType> StreamerBotEventRequests = new() { Command.Triggered };

	public override void OnEventDataReceived(EventType type, Dictionary data)
	{
		CommandData command = new(data);
		if (commandName == "" || commandName == command.Name)
			EmitSignal(SignalName.CommandTriggered, command.Name, command.Message);
    }

}
