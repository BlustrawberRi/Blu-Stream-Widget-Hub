using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class TwitchRaid
{
    public int fromBroadcasterUserId;
    public string fromBroadcasterUserLogin;
    public string fromBroadcasterUserName;

    public int toBroadcasterUserId;
    public string toBroadcasterUserLogin;
    public string toBroadcasterUserName;
    public int viewers;
    public bool isTest;

    public enum RewardStatus { unfulfilled, fullfilled }


    public TwitchRaid(Dictionary data)
    {
        try
        {
            fromBroadcasterUserId = data.GetValueOrDefault("from_broadcaster_user_id").AsInt16();
            fromBroadcasterUserLogin = data.GetValueOrDefault("from_broadcaster_user_login").AsString();
            fromBroadcasterUserName = data.GetValueOrDefault("from_broadcaster_user_name").AsString();

            toBroadcasterUserId = data.GetValueOrDefault("to_broadcaster_user_id").AsInt16();
            toBroadcasterUserLogin = data.GetValueOrDefault("to_broadcaster_user_login").AsString();
            toBroadcasterUserName = data.GetValueOrDefault("to_broadcaster_user_name").AsString();

            viewers = data.GetValueOrDefault("viewers").AsInt16();
            isTest = data.GetValueOrDefault("is_test").AsBool();

        }
        catch (Exception e)
        {
            GD.PrintErr(e);
            //todo throw my own error
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

