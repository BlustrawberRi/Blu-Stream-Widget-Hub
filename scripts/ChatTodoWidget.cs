using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// Displays Todo's received by the !todo command as a list.
/// </summary>
/// <remarks>
/// Data needed:
/// - <b>Command/Triggered</b>: command, message, user.display, user.id, user.type
/// 
/// 
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
/// </remarks>
public partial class ChatTodoWidget : StreamerWidget
{
    public override Enum[] StreamerbotEventRequests { 
            get 
            {   
                return new Enum[]{
                    StreamerbotClient.EventTypes.Command.Triggered};
            } 
        }

    [Signal]
    public delegate void TodoReceivedEventHandler(string todo, ChatUser user, string timestamp);
    [Signal]
    public delegate void TodoDoneEventHandler(ChatUser user, string timestamp);

    public override void OnAnswerReceived(string answer)
    {
        //todo: check for command todo
        var data = Json.ParseString(answer).AsGodotDictionary().GetValueOrDefault("data").AsGodotDictionary();
        string command = data.GetValueOrDefault("command").AsString();

        if (command == "!todo")
        {
            AddNewTodo(data);
        }
        else if (command == "!done")
            MarkTodoDone(data);

    }

    private void AddNewTodo ( Dictionary data)
    {
        var todo = data.GetValueOrDefault("message").AsString();
        if (todo == "") return;

        var user = GetUserFromCommandData(data);

        AddNewTodo(todo, user);
    }
    public void AddNewTodo(String todo, ChatUser user)
    {
        GD.PrintRich("[wave]New Todo from [b]"+user.display+"[/b]: "+todo+"[/wave]");
        EmitSignal(SignalName.TodoReceived, todo, user, Time.GetDatetimeStringFromSystem());
    }

    private void MarkTodoDone(Dictionary data)
    {
        var user = GetUserFromCommandData(data);
        EmitSignal(SignalName.TodoDone, user);
        GD.PrintRich("[rainbow]TODO DONE[/rainbow]");
    }

    private ChatUser GetUserFromCommandData(Dictionary data) {

        ChatUser user = new();
        try
        {
            var userData = data.GetValueOrDefault("user").AsGodotDictionary();
            user.display = userData.GetValueOrDefault("display").AsString();
            user.id = userData.GetValueOrDefault("id").AsInt16();
            user.type = (ChatUser.UserType)userData.GetValueOrDefault("role").AsInt16();
        } catch (Exception) {
            if (user.display != "") return user;
            return null;
        }
        return user;
    }
}
