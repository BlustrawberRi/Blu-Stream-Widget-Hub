using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class StreamerbotAnswerParser : Node
{
	[Signal] public delegate void CommandReceivedEventHandler(string commandName, string commandParam, string userDisplayName); 
	
	public void OnAnswerReceived(string answer)
	{
		var answerDic = Json.ParseString(answer);
		//GD.PrintRich(answerDic +"\n");

		//Todo handle event status

		if(answerDic.AsGodotDictionary() == null)
			return;

		if (HasChatMessage(answerDic.AsGodotDictionary())) 
		{
        	Command command = ExtractCommand(answerDic.AsGodotDictionary());
			if (command != null)
				EmitSignal(SignalName.CommandReceived, command.name, command.param, command.userDisplayName);
        }
	}

    private Command ExtractCommand(Dictionary answer)
    {

		string message = GetChatMessage(answer);
		if (!IsCommand(message))
			return null;

        string[] commandStructure = message.Split(" ", 2);
		
		return new Command(commandStructure[0],commandStructure.Length < 2? "" : commandStructure[1], GetUserDisplayName(answer));
    }

    private bool HasChatMessage(Dictionary answer)
    {
		string messageType = null;
		try
		{
			messageType = answer["event"].AsGodotDictionary()["type"].ToString();
		} catch (KeyNotFoundException) 
		{
			return false;
		} 

		if (messageType == "ChatMessage")
			return true;

		return false;
    }

	/// <summary>
	/// Gets Chat message from answer, if there is any.
	/// </summary>
	/// <param name="answer">Answer from streamer.bot.</param>
	/// <returns>Chat Message or null if no ChatMessage.</returns>
	private string GetChatMessage(Dictionary answer)
	{
		string chatMessage = null;
		try 
		{
			chatMessage = answer["data"].AsGodotDictionary()["message"].AsGodotDictionary()["message"].ToString();
		} catch (KeyNotFoundException e) 
		{
			GD.Print(e.Message + ": " + e.TargetSite);
		}
		return chatMessage;
	}

	private string GetUserDisplayName(Dictionary answer)
	{
		string userDisplayName = null;
		try 
		{
			userDisplayName = answer["data"].AsGodotDictionary()["message"].AsGodotDictionary()["displayName"].ToString();
		} catch (KeyNotFoundException e) 
		{
			GD.Print(e.Message + ": " + e.TargetSite);
		}
		return userDisplayName;
	}

	private bool IsCommand (string chatMessage) 
	{
		if ( chatMessage[0] == '!' ) {
			GD.Print("Message is a command!");
			return true;
		}
		return false;
	}

}
