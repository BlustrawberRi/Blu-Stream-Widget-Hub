
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class CommandData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Command { get; set; }
    public string Message { get; set; }
	public int Counter { get; set; }
    public ChatUser User = new();

    public CommandData (Dictionary data)
    {
        try 
        {
			Name = data.GetValueOrDefault("name").AsString();
			Command = data.GetValueOrDefault("command").AsString();
			Message = data.GetValueOrDefault("message").AsString();
			Id = data.GetValueOrDefault("id").AsString();
			Counter = data.GetValueOrDefault("counter").AsInt16();

            var userData = data.GetValueOrDefault("user").AsGodotDictionary();
            User.Display = userData.GetValueOrDefault("display").AsString();
            User.Id = userData.GetValueOrDefault("id").AsInt16();
            User.Type = (ChatUser.UserType)userData.GetValueOrDefault("role").AsInt16();
			User.Subscribed = userData.GetValueOrDefault("subscribed").AsString();
        }
        catch (Exception e)
        {
            GD.PrintErr(e);
        }

    }
}

/* =========== COMMAND DATA STRUCTURE ====================
==========================================================
    {
        "data": {
            "command": "!boop",
            "counter": 3,
            "id": "fb7f251a-127f-4cd0-a256-ac234ce1f723",
            "message": "",
            "name": "Boop",
            "user": {
                "display": "visual_sanity",
                "id": "189451873",
                "name": "visual_sanity",
                "role": 3,
                "subscribed": false,
                "type": "twitch"
            },
            "userCounter": 1
        },
        "event": {
            "source": "Command",
            "type": "Triggered"
        },
        "timeStamp": "2025-05-03T19:39:19.7509626+02:00"
    }*/

