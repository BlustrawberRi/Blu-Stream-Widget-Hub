using Godot;
using Godot.Collections;
using SB.Events;
using System;

public partial class CommandWidget : StreamerWidget
{
	[Export] string commandName;
	[Signal] public delegate void CommandTriggeredEventHandler(string commandName, string commandText);
	public override EventType[] StreamerBotEventRequests
	{
		get => new[] { Command.Triggered };
		set { }
	}

	public override void OnEventDataReceived(EventType type, Dictionary data)
	{
		CommandData command = new(data);
		if (commandName == "" || commandName == command.Name)
			EmitSignal(SignalName.CommandTriggered, command.Name, command.Message);
    }

}
